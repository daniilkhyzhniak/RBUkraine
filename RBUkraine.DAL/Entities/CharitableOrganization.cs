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

        public ICollection<CharitableOrganizationTranslate> CharitableOrganizationTranslates { get; set; }
    }
}