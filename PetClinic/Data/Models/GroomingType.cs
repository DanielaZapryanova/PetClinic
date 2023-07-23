using System.ComponentModel.DataAnnotations;

namespace PetClinic.Data.Models
{
    public class GroomingType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        [Required]
        [MaxLength(20)]
        public string Breed { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
