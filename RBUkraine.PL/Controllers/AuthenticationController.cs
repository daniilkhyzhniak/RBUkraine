using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Contracts;
using RBUkraine.BLL.Models.User;
using RBUkraine.PL.PasswordGenerators;
using RBUkraine.PL.ViewModels.Authentication;

namespace RBUkraine.PL.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet("~/registration")]
        public IActionResult Registration()
        {
            return View();
        }
        
        [HttpPost("~/registration"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(
            [FromForm] RegisterViewModel model)
        {
            await _userService.CreateUserAsync(_mapper.Map<UserCreationModel>(model));
            var claims = await _userService.AuthenticateAsync(_mapper.Map<AuthModel>(model));
            
            if (!claims.Any())
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }

            await SignInAsync(claims);
            
            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }
        
        [HttpGet("~/login")]
        public IActionResult Login(
            [FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("~/login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            [FromForm] LoginViewModel model,
            [FromQuery] string returnUrl)
        {
            var claims = await _userService.AuthenticateAsync(_mapper.Map<AuthModel>(model));

            if (!claims.Any())
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }

            await SignInAsync(claims);

            return string.IsNullOrWhiteSpace(returnUrl)
                ? RedirectToAction("GetAnimals", "CharitableOrganizations")
                : Redirect(returnUrl);
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> LoginGoogle(
            [FromQuery] string token)
        {
            var claims = await _userService.LoginWithGoogle(token);
            await SignInAsync(claims);
            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }

        [HttpPost("~/logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }

        [HttpPost("/remind-password")]
        public async Task<IActionResult> RemindPassword(RemindPasswordViewModel model)
        {
            var password = RandomPasswordGenerator.RandomPassword();
            await _userService.SetNewPasswordAsync(model.Email, password);
            return Ok();
        }

        [HttpGet("~/profile"), Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var user = await _userService.GetUserByEmailAsync(email);
            return View(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost("~/profile"), Authorize]
        public IActionResult Profile(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "Id").Value);
            _userService.Update(userId, _mapper.Map<UserEditorModel>(model));

            return RedirectToAction("Profile");
        }

        private async Task SignInAsync(IEnumerable<Claim> claims)
        {
            var identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}