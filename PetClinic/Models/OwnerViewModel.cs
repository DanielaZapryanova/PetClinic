using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class OwnerViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? Telephone { get; set; }
    }
}
