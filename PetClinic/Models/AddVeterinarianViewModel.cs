using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AddVeterinarianViewModel
    {
        [Required]
        [MaxLength(20)]
        public string? FullName { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? Specialization { get; set; }

        public List<SpecializationViewModel> Specializations { get; set; }
    }
}
