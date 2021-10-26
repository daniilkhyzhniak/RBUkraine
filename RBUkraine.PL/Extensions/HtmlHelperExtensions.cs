using System.Linq;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RBUkraine.PL.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string IsNavTabActive(this IHtmlHelper htmlHelper, params string[] paths)
        {
            var queryString = htmlHelper.ViewContext.HttpContext.Request.Path.Value;
            
            return paths.FirstOrDefault(r => r == queryString) is not null
                ? "button_menu_active" 
                : "button_menu";
        }

        public static string IsCultureSelected(this IHtmlHelper htmlHelper, IRequestCultureFeature requestCultureFeature, string culture)
        {
            return requestCultureFeature.RequestCulture.UICulture.Name == culture 
                ? "background-color: transparent; border-color: transparent;" 
                : "-webkit-filter: grayscale(100%); filter: grayscale(100%);";
        }
    }
}
