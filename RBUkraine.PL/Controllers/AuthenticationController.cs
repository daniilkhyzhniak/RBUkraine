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
using RBUkraine.BLL.Models;
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
            
            return Redirect("login");
        }
        
        // TODO: Delete default route
        [Route("")]
        [HttpGet("~/login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost("~/login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            [FromForm] LoginViewModel model)
        {
            var claims = await _userService.AuthenticateAsync(_mapper.Map<AuthModel>(model));

            if (!claims.Any())
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }

            await SignInAsync(claims);
            
            return Redirect("login");
        }
        
        [HttpPost("~/logout"), Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("login");
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