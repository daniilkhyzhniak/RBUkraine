using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharitableOrganization : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}