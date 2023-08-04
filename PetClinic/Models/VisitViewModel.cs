using PetClinic.Data.Models;

namespace PetClinic.Models
{
    public class VisitViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public Reason ReasonForVisit { get; set; }

        public IList<ReasonViewModel>? PossibleReasons { get; set; }
    }
}
