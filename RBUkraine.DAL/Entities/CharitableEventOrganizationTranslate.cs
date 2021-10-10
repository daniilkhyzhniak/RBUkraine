using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class CharitableOrganizationTranslate : BaseTranslate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CharitableOrganizationId { get; set; }

        public CharitableOrganization CharitableOrganization { get; set; }
    }
}
