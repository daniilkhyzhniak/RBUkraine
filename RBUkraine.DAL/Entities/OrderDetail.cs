using RBUkraine.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBUkraine.DAL.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
