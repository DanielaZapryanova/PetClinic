using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class EditOwnerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FullName { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Telephone { get; set; }
    }
}
