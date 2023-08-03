using Microsoft.EntityFrameworkCore;
using PetClinic.Contracts;
using PetClinic.Data;
using PetClinic.Data.Models;
using PetClinic.Models;

namespace PetClinic.Services
{
    public class VaccineService : IVaccineService
    {
        private ApplicationDbContext dbContext;
        public VaccineService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddVaccine(AddVaccineViewModel addVaccineViewModel)
        {
            Vaccine vaccine = new Vaccine();
            vaccine.Name = addVaccineViewModel.Name;
            vaccine.ExpirationTime = addVaccineViewModel.DateOfExpiry;
            vaccine.NumberInStock = addVaccineViewModel.NumberInStock;
            try
            {
                await dbContext.Vaccines.AddAsync(vaccine);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<VaccineViewModel>> GetAllVaccines()
        {
            return await dbContext.Vaccines.Select(vaccine => new VaccineViewModel
            {
                Name = vaccine.Name,
                NumberInStock = vaccine.NumberInStock,
                DateOfExpiry = vaccine.ExpirationTime
            }).ToListAsync();
        }
    }
}
