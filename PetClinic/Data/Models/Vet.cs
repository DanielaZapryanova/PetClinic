using System.ComponentModel.DataAnnotations;

namespace PetClinic.Data.Models
{
    public class Vet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? FullName { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? Specalization { get; set; }
    }
}
