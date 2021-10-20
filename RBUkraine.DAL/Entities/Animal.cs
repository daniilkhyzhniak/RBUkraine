using RBUkraine.DAL.Entities.Base;
using System.Collections.Generic;

namespace RBUkraine.DAL.Entities
{
    public class Animal : BaseEntity
    {
        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public string Species { get; set; }
        
        public int Population { get; set; }

        public int CharitableOrganizationId { get; set; }

        public CharitableOrganization CharitableOrganization { get; set; }

        public ICollection<AnimalImage> AnimalImages { get; set; }

        public ICollection<AnimalTranslate> AnimalTranslates { get; set; }
    }
}