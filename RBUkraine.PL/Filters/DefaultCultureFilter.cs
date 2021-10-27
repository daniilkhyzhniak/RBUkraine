using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using RBUkraine.BLL.Enums;

namespace RBUkraine.PL.Filters
{
    public class DefaultCultureFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cultureCookie = context.HttpContext.Request.Cookies
                .FirstOrDefault(c => c.Key == CookieRequestCultureProvider.DefaultCookieName);

            if (string.IsNullOrWhiteSpace(cultureCookie.Key))
            {
                context.HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Culture.Ukrainian))
                );

                var url = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
                context.Result = new RedirectResult(url);
                return;
            }

            await next();
        }
    }
}
