using RBUkraine.DAL.Entities.Enums;

namespace RBUkraine.BLL.Enums
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

        internal static Language ConvertToLanguage(string culture)
        {
            return culture == Ukrainian
                ? Language.Ukrainian
                : Language.English;
        }
    }
}