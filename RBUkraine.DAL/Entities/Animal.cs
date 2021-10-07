using RBUkraine.DAL.Entities.Base;
using System.Collections.Generic;

namespace RBUkraine.DAL.Entities
{
    public class Animal : BaseEntity
    {
        public string Name { get; set; }

        public AnimalDetails AnimalDetails { get; set; }

        public ICollection<AnimalImage> AnimalImages { get; set; }
    }
}