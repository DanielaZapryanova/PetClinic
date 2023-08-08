using PetClinic.Data.Models;
using PetClinic.Data;
using PetClinic.Models;
using PetClinic.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
            return false;
            Pet pet = new Pet();
            pet.Name = addPetViewModel.Name;
            pet.DateOfBirth = addPetViewModel.DateOfBirth;
            pet.Breed = addPetViewModel.Breed;
            pet.Gender = addPetViewModel.Gender;
            pet.Color = addPetViewModel.Color;
            pet.Weight = addPetViewModel.Weight;
            pet.OwnerId = addPetViewModel.OwnerId;
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
            return false;
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
            pet.OwnerId = editPetViewModel.OwnerId;
            // Save changes in db
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePet(int id)
        {
            return false;
            // Find pet by id
            var pet = await dbContext.Pets.FirstOrDefaultAsync(pet => pet.Id == id);

            if (pet != null)
            {
                var petVisits = await dbContext.Visits.Where(visit => visit.PetId == pet.Id).ToListAsync();

                // We need to remove all visits also - both simple visit and vaccination.
                foreach(var visit in petVisits)
                {
                    var vaccination = await dbContext.Vaccinations.Where(vaccination => vaccination.VisitId == visit.Id).FirstOrDefaultAsync();
                    if (vaccination != null)
                    {
                        dbContext.Vaccinations.Remove(vaccination);
                    }

                    dbContext.Visits.Remove(visit);
                }
                dbContext.Pets.Remove(pet);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IList<PetViewModel>> GetAllPets()
        {
            var pets = await dbContext.Pets.Select(pet => new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                DateOfBirth = pet.DateOfBirth,
                Breed = pet.Breed,
                Gender = pet.Gender,
                Color = pet.Color,
                Weight = pet.Weight,
                OwnerId = pet.OwnerId,
            }).ToListAsync();
            foreach (var pet in pets)
            {
                var owner = await dbContext.Owners.FirstOrDefaultAsync(owner => owner.Id == pet.OwnerId);
                pet.Owner = owner;
            }
            return pets;
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
                    OwnerId = pet.OwnerId,
                })
                .FirstOrDefaultAsync();
        }
    }
}
