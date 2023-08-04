using PetClinic.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class AddVisitViewModel
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
        public Reason ReasonForVisit { get; set; }

        public IList<ReasonViewModel>? PossibleReasons { get; set; }
    }
}
