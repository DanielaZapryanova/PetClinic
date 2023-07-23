using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Data.Models
{
    public class Examination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VisitId { get; set; }

        [ForeignKey(nameof(VisitId))]
        public Visit? Visit { get; set; }

        [Required]
        public string? Conclusion { get; set; }
    }
}
