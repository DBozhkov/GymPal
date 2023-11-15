namespace GymPalApi.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSupplementInputModel
    {
        [Required]
        [MinLength(2)]
        public string SupplementName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, 1000)]
        public double Price { get; set; }
    }
}
