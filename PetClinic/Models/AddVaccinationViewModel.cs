using PetClinic.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AddVaccinationViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PetId { get; set; }

        public IList<PetViewModel>? PossiblePets { get; set; }

        [Required]
        public int VetId { get; set; }

        public IList<VeterinarianViewModel>? PossibleVets { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int VaccineId{ get; set; }

        public IList<VaccineViewModel>? PossibleVaccines { get; set; }
    }
}
