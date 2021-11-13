using System;
using System.Collections.Generic;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharitableOrganization : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Founders { get; set; }

        public string Stockholders { get; set; }
        
        public DateTimeOffset FoundationDate { get; set; }

        public ICollection<CharitableOrganizationTranslate> CharitableOrganizationTranslates { get; set; }

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public CharitableOrganizationImage Image { get; set; }

        public ICollection<News> News { get; set; } = new List<News>();

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}