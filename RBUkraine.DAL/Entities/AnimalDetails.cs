using System.ComponentModel.DataAnnotations;

namespace RBUkraine.DAL.Entities
{
    public class AnimalDetails
    {
        [Key]
        public int AnimalId { get; set; }

        public string Description { get; set; }
    }
}