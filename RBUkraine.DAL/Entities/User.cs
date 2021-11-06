using System.Collections.Generic;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public bool IncludeInRating { get; set; }

        public ICollection<CharityEventPurchase> CharityEventPurchase { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}