using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RBUkraine.BLL;
using RBUkraine.PL.Filters;
using RBUkraine.PL.Middleware;

namespace RBUkraine.PL
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBusinessLogicLayer(_configuration.GetConnectionString("RBUkraineDb"));
            services.AddAutoMapper(typeof(Mapper));
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.AccessDeniedPath = new PathString("/error/accessDenied");
                    options.ReturnUrlParameter = "returnUrl";
                });
            services.AddAuthorization();
            
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
            services.AddControllersWithViews();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/errors");
            }
            
            app.UseHttpsRedirection();
            app.UseDefaultRouteRedirect("/animals");

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "");
            });
        }
    }
}
