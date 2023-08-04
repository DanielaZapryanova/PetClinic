using Microsoft.EntityFrameworkCore;
using PetClinic.Contracts;
using PetClinic.Data;
using PetClinic.Data.Models;
using PetClinic.Models;

namespace PetClinic.Services
{
    public class VisitsService : IVisitsService
    {
        private ApplicationDbContext dbContext;

        public VisitsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddVisit(AddVisitViewModel addVisitViewModel)
        {
            Visit visit = new Visit();
            visit.Date = addVisitViewModel.Date;
            visit.PetId = addVisitViewModel.PetId;
            visit.VetId = addVisitViewModel.VetId;
            visit.Price = addVisitViewModel.Price;
            visit.ReasonForVisit = addVisitViewModel.ReasonForVisit;
            try
            {
                await dbContext.Visits.AddAsync(visit);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IList<ReasonViewModel> GetPossibleReasons()
        {
            IList<ReasonViewModel> reasons = new List<ReasonViewModel>();
            foreach(Reason reason in Enum.GetValues(typeof(Reason)))
            {
                reasons.Add(new ReasonViewModel() { FriendlyName = Enum.GetName(typeof(Reason), reason), Reason = reason });
            }
            return reasons;
        }
    }
}
