using System.ComponentModel.DataAnnotations;

namespace GymPalApi.ViewModels
{
    public class CreateUserInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email cannot be empty"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(5)]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password cannot be empty")]
        [MinLength(5)]
        [MaxLength(25)]
        public string ConfirmPassword { get; set; }
    }
}