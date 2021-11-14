using System.Collections.Generic;
using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.Product
{
    public class ProductFilterModel
    {
        public ProductSearchOptions SearchOptions { get; set; } = ProductSearchOptions.ByName;
        public string Search { get; set; }
        
        public ProductSortOptions SortOptions { get; set; }

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;

        public IList<string> Categories { get; set; } = new List<string>();
    }
}
