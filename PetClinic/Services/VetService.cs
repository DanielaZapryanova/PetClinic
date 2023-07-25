using PetClinic.Contracts;
using PetClinic.Data;
using PetClinic.Data.Models;
using PetClinic.Models;

namespace PetClinic.Services
{
    public class VetService : IVetService
    {
        private ApplicationDbContext dbContext;
        public VetService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddVet(AddVeterinarianViewModel addVetViewModel)
        {
            Vet vet = new Vet();
            vet.FullName = addVetViewModel.FullName;
            vet.Telephone = addVetViewModel.Telephone;
            vet.Specialization = addVetViewModel.Specialization;
            try
            {
                await dbContext.Vets.AddAsync(vet);
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
