using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharityEvent : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string Organizer { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<CharityEventTranslate> CharityEventTranslates { get; set; }

        public ICollection<CharityEventPurchase> CharityEventPurchase { get; set; }
    }
}