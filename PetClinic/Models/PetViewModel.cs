using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class PetViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Breed { get; set; }

        public string? Gender { get; set; }

        public string? Color { get; set; }

        public decimal Weight { get; set; }

        public int OwnerId { get; set; }
    }
}
