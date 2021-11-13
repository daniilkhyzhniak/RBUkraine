namespace RBUkraine.BLL.Models.Donation
{
    public class DonationModel
    {
        public int UserId { get; set; }

        public int? AnimalId { get; set; }

        public int? CharitableOrganizationId { get; set; }

        public decimal Amount { get; set; }
    }
}
