using System;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharityEvent : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string Organizer { get; set; }

        public decimal Price { get; set; }
    }
}