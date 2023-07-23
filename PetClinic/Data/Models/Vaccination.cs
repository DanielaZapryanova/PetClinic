using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Data.Models
{
    public class Vaccination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VisitId { get; set; }

        [ForeignKey(nameof(VisitId))]
        public Visit? Visit { get; set; }

        [Required]
        public int VaccineId { get; set; }

        [ForeignKey(nameof(VaccineId))]
        public Vaccine? Vaccine { get; set; }
    }
}

