using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RBUkraine.PL.Enums
{
    public static class Culture
    {
        public const string Ukrainian = "uk";

        public const string English = "en";

        public static bool Exist(string culture)
        {
            return culture is (Ukrainian or English);
        }

        public static string[] All => new[] {Ukrainian, English};

        public static SelectList SelectList => new SelectList(new List<SelectListItem> 
        {
            new ("Укр", Ukrainian),
            new ("Eng", English),
        }, "Value", "Text");
    }
}