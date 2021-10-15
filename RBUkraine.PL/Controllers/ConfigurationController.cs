using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using RBUkraine.BLL.Enums;

namespace RBUkraine.PL.Controllers
{
    public class ConfigurationController : Controller
    {
        [HttpPost("~/culture")]
        public IActionResult SetCulture(string culture)
        {
            if (Culture.Exist(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}