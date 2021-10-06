using System.ComponentModel.DataAnnotations;

namespace RBUkraine.PL.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}