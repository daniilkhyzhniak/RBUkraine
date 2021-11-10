using System;
using System.Collections.Generic;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }
        
        public DateTimeOffset PublishDate { get; set; }

        public int? CharitableOrganizationId { get; set; }

        public int? AnimalId { get; set; }

        public CharitableOrganization CharitableOrganization { get; set; }

        public Animal Animal { get; set; }

        public ICollection<NewsTranslate> NewsTranslates { get; set; }
    }
}
