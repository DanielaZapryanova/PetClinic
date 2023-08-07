using Microsoft.EntityFrameworkCore;
using PetClinic.Contracts;
using PetClinic.Data;
using PetClinic.Data.Models;
using PetClinic.Models;
using System.Drawing;

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
            foreach (Reason reason in Enum.GetValues(typeof(Reason)))
            {
                if (reason != Reason.Vaccination)
                {
                    reasons.Add(new ReasonViewModel() { FriendlyName = reason.ToFriendlyString(), Reason = reason });
                }
            }
            return reasons;
        }

        public async Task<IList<VaccineViewModel>> GetPossibleVaccines()
        {
            var vaccines = await dbContext.Vaccines
            .Where(vaccine => vaccine.ExpirationTime > DateTime.UtcNow)
            .Where(vaccine => vaccine.NumberInStock > 0)
            .Select(vaccine => new VaccineViewModel
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                DateOfExpiry = vaccine.ExpirationTime,
                NumberInStock = vaccine.NumberInStock,
            }).ToListAsync();
            return vaccines;
        }

        public async Task<IList<VisitViewModel>> AllVisit()
        {
            var visits = await dbContext.Visits.Select(visit => new VisitViewModel
            {
                Id = visit.Id,
                Date = visit.Date,
                Price = visit.Price,
                ReasonForVisit = visit.ReasonForVisit,
            }).ToListAsync();
            return visits;
        }

        public async Task<IList<VisitViewModel>> Visits(int id)
        {
            var visits = await dbContext.Visits
            .Where(x => x.PetId == id)
            .Select(visit => new VisitViewModel
            {
                Id = visit.Id,
                Date = visit.Date,
                Price = visit.Price,
                ReasonForVisit = visit.ReasonForVisit,
            }).ToListAsync();
            return visits;
        }

        public async Task<bool> AddVaccination(AddVaccinationViewModel addVaccinationViewModel)
        {
            Visit visit = new Visit();
            visit.Date = addVaccinationViewModel.Date;
            visit.PetId = addVaccinationViewModel.PetId;
            visit.VetId = addVaccinationViewModel.VetId;
            visit.Price = addVaccinationViewModel.Price;
            visit.ReasonForVisit = Reason.Vaccination;
            try
            {
                await dbContext.Visits.AddAsync(visit);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            Vaccination vaccination = new Vaccination();
            vaccination.VisitId = visit.Id;
            vaccination.VaccineId = addVaccinationViewModel.VaccineId;
            try
            {
                var vaccine = await dbContext.Vaccines.Where(vaccine => vaccine.Id == vaccination.VaccineId).FirstOrDefaultAsync();
                vaccine.NumberInStock--;
                await dbContext.Vaccinations.AddAsync(vaccination);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
