using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Data.Models
{
    public class Grooming
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VisitId { get; set; }

        [ForeignKey(nameof(VisitId))]
        public Visit? Visit { get; set; }

        [Required]
        public int GroomingTypeId { get; set; }

        [ForeignKey(nameof(GroomingTypeId))]
        public GroomingType? GroomingType { get; set; }
    }
}
