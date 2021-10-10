using RBUkraine.DAL.Entities.Base;
using System.Collections.Generic;

namespace RBUkraine.DAL.Entities
{
    public class Animal : BaseEntity
    {
        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public int Population { get; set; }

        public ICollection<AnimalImage> AnimalImages { get; set; }
    }
}