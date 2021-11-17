using System.Collections.Generic;
using RBUkraine.PL.ViewModels.Products;

namespace RBUkraine.PL.ViewModels.Cart
{
    public class MakeOrderViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        
        public string City { get; set; }

        public string PostalCode { get; set; }
    }
}
