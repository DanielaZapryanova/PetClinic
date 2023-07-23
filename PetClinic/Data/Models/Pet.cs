using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Data.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? Breed { get;set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Color { get; set; }

        public string? Image { get;set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owner? Owner { get; set; }

    }
}
