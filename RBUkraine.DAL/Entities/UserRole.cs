using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        
        public int RoleId { get; set; }
        
        public User User { get; set; }
        
        public Role Role { get; set; }
    }
}