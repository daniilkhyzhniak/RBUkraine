using System.ComponentModel.DataAnnotations.Schema;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharityEventPurchase : BaseEntity
    {
        public int UserId { get; set; }

        public int CharityEventId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        public User User { get; set; }

        public CharityEvent CharityEvent { get; set; }
    }
}
