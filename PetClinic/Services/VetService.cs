using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> EditVet(EditVeterinarianViewModel editVetViewModel)
        {
            // Find vet by id
            var vet = await dbContext.Vets.FirstOrDefaultAsync(vet => vet.Id == editVetViewModel.Id);

            if (vet == null)
            {
                throw new InvalidOperationException($"Book with id: {editVetViewModel.Id} cannot be found.");
            }

            // Update vet properties
            vet.FullName = editVetViewModel.FullName;
            vet.Telephone = editVetViewModel.Telephone;
            vet.Specialization = editVetViewModel.Specialization;

            // Save changes in db
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<EditVeterinarianViewModel?> GetVet(int vetId)
        {
            return await dbContext.Vets
                .Where(vet => vet.Id == vetId)
                .Select(vet => new EditVeterinarianViewModel
                {
                    Id = vet.Id,
                    FullName = vet.FullName,
                    Telephone = vet.Telephone,
                    Specialization = vet.Specialization
                })
                .FirstOrDefaultAsync();
        }
    }
}
