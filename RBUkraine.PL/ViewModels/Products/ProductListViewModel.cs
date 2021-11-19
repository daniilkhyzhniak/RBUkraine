using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBUkraine.BLL.Models.Product;

namespace RBUkraine.PL.ViewModels.Products
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public ProductFilterModel Filter { get; set; }

        public IEnumerable<SelectListItem> CategoriesSelectList { get; set; }
    }
}
