using PetClinic.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class EditPetViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? Breed { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Color { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owner? Owner { get; set; }

        public IList<OwnerViewModel>? PossibleOwners { get; set; }
    }
}
