using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class AnimalTranslate : BaseTranslate
    {
        public int AnimalId { get; set; }

        public string Species { get; set; }

        public string ConservationStatus { get; set; }

        public string Kingdom { get; set; }

        public string Phylum { get; set; }

        public string Class { get; set; }

        public string Order { get; set; }

        public string Family { get; set; }

        public string Genus { get; set; }

        public string Description { get; set; }

        public Animal Animal { get; set; }
    }
}
