using System.ComponentModel.DataAnnotations;

namespace RBUkraine.PL.ViewModels.Authentication
{
    public class UserViewModel
    {
        public string Email { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public bool IncludeInRating { get; set; }

        public decimal TotalDonationAmount { get; set; }
    }
}