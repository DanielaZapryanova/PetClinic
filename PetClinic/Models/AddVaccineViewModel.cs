using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AddVaccineViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime DateOfExpiry { get; set; }
    }
}
