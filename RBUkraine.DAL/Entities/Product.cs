using RBUkraine.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBUkraine.DAL.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        public ProductImage Image { get; set; }
    }
}
