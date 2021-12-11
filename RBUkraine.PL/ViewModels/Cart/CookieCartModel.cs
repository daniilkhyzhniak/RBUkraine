using System.Collections.Generic;

namespace RBUkraine.PL.ViewModels.Cart
{
    public class CookieCartModel
    {
        public IList<CookieCartItemModel> Items { get; set; } = new List<CookieCartItemModel>();
    }

    public class CookieCartItemModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
    }
}
