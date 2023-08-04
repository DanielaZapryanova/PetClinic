using PetClinic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Data.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PetId { get; set; }

        [ForeignKey(nameof(PetId))]
        public Pet? Pet { get; set; }

        [Required]
        public int VetId { get; set; }

        [ForeignKey(nameof(VetId))]
        public Vet? Vet { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Reason ReasonForVisit { get; set; }
    }
    
    public enum Reason
    {
        Examinations,
        BloodTest,
        Vaccination,
        Grooming
    };
}
