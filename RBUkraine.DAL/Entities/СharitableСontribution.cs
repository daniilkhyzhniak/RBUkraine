using RBUkraine.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBUkraine.DAL.Entities
{
    public class СharitableСontribution : BaseEntity
    {
        public int? AnimalId { get; set; }

        public int? CharitableOrganizationId { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public int UserId { get; set; }

        public Animal Animal { get; set; }

        public CharitableOrganization CharitableOrganization { get; set; }

        public User User { get; set; }
    }
}
