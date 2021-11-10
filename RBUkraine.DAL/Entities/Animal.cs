using RBUkraine.DAL.Entities.Base;
using System.Collections.Generic;

namespace RBUkraine.DAL.Entities
{
    public class Animal : BaseEntity
    {
        public string Species { get; set; }

        public string LatinSpecies { get; set; }

        public string ConservationStatus { get; set; }

        public string Kingdom { get; set; }

        public string Phylum { get; set; }

        public string Class { get; set; }
        
        public string Order { get; set; }

        public string Family { get; set; }

        public string Genus { get; set; }

        public int Population { get; set; }

        public string Description { get; set; }

        public int CharitableOrganizationId { get; set; }

        public CharitableOrganization CharitableOrganization { get; set; }

        public ICollection<AnimalImage> AnimalImages { get; set; }

        public ICollection<AnimalTranslate> AnimalTranslates { get; set; }

        public ICollection<News> News { get; set; }
    }
}