using System.ComponentModel.DataAnnotations;

namespace GymPalApi.ViewModels
{
    public class AddUserToRole
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}
