namespace RBUkraine.BLL.Models.VolunteerApplication
{
    public class VolunteerApplicationModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public string Reason { get; set; }
        public string WorkingConditions { get; set; }
        public bool? Confirmed { get; set; }
    }
}
