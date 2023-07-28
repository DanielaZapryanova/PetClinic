using PetClinic.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AddOwnerViewModel
    {
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
