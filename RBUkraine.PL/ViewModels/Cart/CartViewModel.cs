using System.Collections.Generic;

namespace RBUkraine.PL.ViewModels.Cart
{
    public class CartViewModel
    {
        public IEnumerable<ProductCartViewModel> Products { get; set; }
        public decimal Sum { get; set; }
    }
}
