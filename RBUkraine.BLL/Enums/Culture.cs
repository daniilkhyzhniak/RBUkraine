using System.Linq;
using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.BLL.Enums
{
    public static class Culture
    {
        public const string Ukrainian = "uk";

        public const string English = "en";

        public const string Russian = "ru";

        public static bool Exist(string culture)
        {
            return All.Contains(culture);
        }

        public static string[] All => new[] { Ukrainian, English, Russian };

        internal static Language ConvertToLanguage(string culture)
        {
            return culture switch
            {
                Ukrainian => Language.Ukrainian,
                English => Language.English,
                Russian => Language.Russian,
                _ => Language.Ukrainian
            };
        }
    }
}