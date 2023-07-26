using PetClinic.Data.Models;
using PetClinic.Data;
using PetClinic.Models;
using PetClinic.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PetClinic.Services
{
    public class PetService : IPetService
    {
        private ApplicationDbContext dbContext;
        public PetService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddPet(AddPetViewModel addPetViewModel)
        {
            Pet pet = new Pet();
            pet.Name = addPetViewModel.Name;
            pet.DateOfBirth = addPetViewModel.DateOfBirth;
            pet.Breed = addPetViewModel.Breed;
            pet.Gender = addPetViewModel.Gender;
            pet.Color = addPetViewModel.Color;
            pet.Weight = addPetViewModel.Weight;
            try
            {
                await dbContext.Pets.AddAsync(pet);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> EditPet(EditPetViewModel editPetViewModel)
        {
            // Find pet by id
            var pet = await dbContext.Pets.FirstOrDefaultAsync(pet => pet.Id == editPetViewModel.Id);

            if (pet == null)
            {
                throw new InvalidOperationException($"Pet with id: {editPetViewModel.Id} cannot be found.");
            }

            // Update pet properties
            pet.Name = editPetViewModel.Name;
            pet.DateOfBirth = editPetViewModel.DateOfBirth;
            pet.Breed = editPetViewModel.Breed;
            pet.Gender = editPetViewModel.Gender;
            pet.Color = editPetViewModel.Color;
            pet.Weight = editPetViewModel.Weight;

            // Save changes in db
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<PetViewModel>> GetAllPets()
        {
            return await dbContext.Pets.Select(pet => new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                DateOfBirth = pet.DateOfBirth,
                Breed = pet.Breed,
                Gender = pet.Gender,
                Color = pet.Color,
                Weight = pet.Weight,
            }).ToListAsync();
        }
        public async Task<EditPetViewModel?> GetPet(int petId)
        {
            return await dbContext.Pets
                .Where(pet => pet.Id == petId)
                .Select(pet => new EditPetViewModel
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    DateOfBirth = pet.DateOfBirth,
                    Breed = pet.Breed,
                    Gender = pet.Gender,
                    Color = pet.Color,
                    Weight = pet.Weight,
                })
                .FirstOrDefaultAsync();
        }
    }
}
