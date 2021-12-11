using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Entities
{
    public class VolunteerApplication : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool AppropriateAge { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public string Reason { get; set; }
        public string WorkingConditions { get; set; }
        public bool? Confirmed { get; set; }
    }
}
