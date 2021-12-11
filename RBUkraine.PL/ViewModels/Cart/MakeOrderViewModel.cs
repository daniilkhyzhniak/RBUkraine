using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBUkraine.PL.ViewModels.Cart
{
    public class MakeOrderViewModel
    {
        public IEnumerable<ProductCartViewModel> Products { get; set; }
        public decimal Sum { get; set; }

        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int PostOfficeNumber { get; set; }
        public string Comment { get; set; }
    }
}
