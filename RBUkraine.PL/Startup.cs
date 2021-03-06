using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RBUkraine.BLL;
using RBUkraine.BLL.Enums;
using RBUkraine.PL.EmailSender;
using RBUkraine.PL.Filters;
using RBUkraine.PL.Services;
using Stripe;

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
            services.AddTransient<IEmailSender, EmailSender.EmailSender>();
            services.AddTransient<IBonusService, BonusService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.AccessDeniedPath = new PathString("/access-denied");
                    options.ReturnUrlParameter = "returnUrl";
                })
                .AddGoogle(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = _configuration["GoogleAuth:ClientId"];
                    options.ClientSecret = _configuration["GoogleAuth:ClientSecret"];
                });
            services.AddAuthorization();
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            
            services.AddMvc(options =>
            {
                options.Filters.Add<DefaultCultureFilter>();
            })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddViewLocalization();
            
            services.Configure<RequestLocalizationOptions>(options=>
            {
                var cultures = Culture.All.Select(c => new CultureInfo(c)).ToList();
                options.DefaultRequestCulture = new RequestCulture(Culture.Ukrainian);
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
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
            //app.UseDefaultRouteRedirect("/organizations");

            app.UseRequestLocalization(
                app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var apiKey = string.Join("", _configuration["Stripe:ApiKey"].Split("///"));
            StripeConfiguration.ApiKey = apiKey;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "");
            });
        }
    }
}
