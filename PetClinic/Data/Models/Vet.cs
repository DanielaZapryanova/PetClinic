using System.ComponentModel.DataAnnotations;

namespace PetClinic.Data.Models
{
    public class Vet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? FullName { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? Specialization { get; set; }
    }
}
