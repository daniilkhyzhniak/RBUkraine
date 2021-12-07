namespace RBUkraine.BLL.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public bool IncludeInRating { get; set; }

        public decimal TotalDonationAmount { get; set; }

        public bool IsBonusReceived { get; set; }
    }
}