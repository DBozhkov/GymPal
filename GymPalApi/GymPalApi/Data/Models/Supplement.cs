using System.ComponentModel.DataAnnotations;

namespace GymPalApi.Data
{
    public class Supplement
    {
        public Supplement()
        {
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string SupplementName { get; set; }

        [Required]
        [Range(1700, 2023)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(1700, 2023)]
        public double Price { get; set; }
    }
}
