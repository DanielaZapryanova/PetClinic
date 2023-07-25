using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class VeterinarianViewModel
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Specialization { get; set; }
    }
}
