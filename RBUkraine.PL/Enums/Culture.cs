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

        public static SelectList SelectList => new SelectList(new List<CultureDisplay> 
        {
            new CultureDisplay
            {
                Text = "Укр",
                Value = Ukrainian
            },
            new CultureDisplay
            {
                Text = "Eng",
                Value = English
            },
        }, "Value", "Text");

        private class CultureDisplay
        {
            public string Text { get; set; }

            public string Value { get; set; }
        }
    }
}