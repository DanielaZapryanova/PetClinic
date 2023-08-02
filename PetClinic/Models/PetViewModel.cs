using PetClinic.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

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

        [ForeignKey(nameof(OwnerId))]
        public Owner? Owner { get; set; }
        public IList<OwnerViewModel>? PossibleOwners { get; set; }
    }
}
