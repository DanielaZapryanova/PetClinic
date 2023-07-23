using System.ComponentModel.DataAnnotations;

namespace PetClinic.Data.Models
{
    public class Vaccine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime ExpirationTime { get; set; }
    }
}
