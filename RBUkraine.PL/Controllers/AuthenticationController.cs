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
using RBUkraine.BLL.Enums;
using RBUkraine.BLL.Models.User;
using RBUkraine.PL.EmailSender;
using RBUkraine.PL.EmailSender.Models;
using RBUkraine.PL.PasswordGenerators;
using RBUkraine.PL.ViewModels.Authentication;

namespace RBUkraine.PL.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDonationService _donationService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public AuthenticationController(
            IUserService userService,
            IDonationService donationService,
            IMapper mapper,
            IEmailSender emailSender)
        {
            _userService = userService;
            _donationService = donationService;
            _mapper = mapper;
            _emailSender = emailSender;
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
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

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
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            var claims = await _userService.AuthenticateAsync(_mapper.Map<AuthModel>(model));

            if (!claims.Any())
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }

            await SignInAsync(claims);

            if (User.IsInRole(Roles.Admin))
            {
                return RedirectToAction("GetAll", "Animals");
            }

            return RedirectToAction("Profile", "Authentication");
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> LoginGoogle(
            [FromQuery] string token)
        {
            var claims = await _userService.LoginWithGoogle(token);
            await SignInAsync(claims);
            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }

        [HttpGet("~/logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("GetAnimals", "CharitableOrganizations");
        }

        [HttpPost("/remind-password")]
        public async Task<IActionResult> RemindPassword(RemindPasswordViewModel model)
        {
            var password = RandomPasswordGenerator.RandomPassword();

            await _emailSender.SendEmailAsync(new EmailModel
            {
                Email = model.Email,
                Message = $"Ваш новий пароль - {password}",
                Subject = "RBUkraine. Новий пароль"
            });

            await _userService.SetNewPasswordAsync(model.Email, password);
            return Ok();
        }

        [HttpGet("~/profile"), Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var user = await _userService.GetUserByEmailAsync(email);
            user.TotalDonationAmount = await _donationService.GetTotalAmount(user.Id);
            return View(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost("~/profile"), Authorize]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "Id").Value);
            await _userService.Update(userId, _mapper.Map<UserEditorModel>(model));

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