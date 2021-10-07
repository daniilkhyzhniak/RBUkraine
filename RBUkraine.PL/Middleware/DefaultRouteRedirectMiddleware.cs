using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RBUkraine.PL.Middleware
{
    public class DefaultRouteRedirectMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _redirectPath;

        public DefaultRouteRedirectMiddleware(RequestDelegate next, string redirectPath)
        {
            _next = next;
            _redirectPath = redirectPath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect(_redirectPath, true);
                return;
            }

            await _next(context);
        }
    }

    public static class DefaultRouteRedirectMiddlewareExtensions
    {
        public static void UseDefaultRouteRedirect(this IApplicationBuilder app, string redirectPath)
        {
            app.UseMiddleware<DefaultRouteRedirectMiddleware>(redirectPath);
        }
    }
}
