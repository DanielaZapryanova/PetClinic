namespace PetClinic.Models
{
    public class VaccineViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? NumberInStock { get; set; }

        public DateTime DateOfExpiry { get; set; }
    }
}
