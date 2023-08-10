using PetClinic.Data.Models;
using PetClinic.Data;
using PetClinic.Models;
using PetClinic.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PetClinic.Services
{
    public class OwnerService : IOwnerService
    {
        private ApplicationDbContext dbContext;
        private readonly IPetService petService;

        public OwnerService(ApplicationDbContext dbContext, IPetService petService)
        {
            this.dbContext = dbContext;
            this.petService = petService;
        }

        public async Task<bool> AddOwner(AddOwnerViewModel addOwnerViewModel)
        {
            Owner owner = new Owner();
            owner.FullName = addOwnerViewModel.FullName;
            owner.Address = addOwnerViewModel.Address;
            owner.Age = addOwnerViewModel.Age;
            owner.Telephone = addOwnerViewModel.Telephone;

            try
            {
                await dbContext.Owners.AddAsync(owner);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EditOwner(EditOwnerViewModel editOwnerViewModel)
        {
            // Find owner by id
            var owner = await dbContext.Owners.FirstOrDefaultAsync(owner => owner.Id == editOwnerViewModel.Id);

            if (owner == null)
            {
                throw new InvalidOperationException($"Owner with id: {editOwnerViewModel.Id} cannot be found.");
            }

            // Update owner properties
            owner.FullName = editOwnerViewModel.FullName;
            owner.Address = editOwnerViewModel.Address;
            owner.Age = editOwnerViewModel.Age;
            owner.Telephone = editOwnerViewModel.Telephone;

            // Save changes in db
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<OwnerViewModel>> GetAllOwners()
        {
            return await dbContext.Owners.Select(owner => new OwnerViewModel
            {
                Id = owner.Id,
                FullName = owner.FullName,
                Address = owner.Address,
                Age = owner.Age,
                Telephone = owner.Telephone,
            }).ToListAsync();
        }
        public async Task<EditOwnerViewModel?> GetOwner(int ownerId)
        {
            return await dbContext.Owners
                .Where(owner => owner.Id == ownerId)
                .Select(owner => new EditOwnerViewModel
                {
                    Id = owner.Id,
                    FullName = owner.FullName,
                    Address = owner.Address,
                    Age = owner.Age,
                    Telephone = owner.Telephone,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteOwner(int id)
        {
            // Find owner by id
            var ownerFound = await dbContext.Owners.FirstOrDefaultAsync(owner => owner.Id == id);

            if (ownerFound != null)
            {
                var petsOfOwner = await dbContext.Pets.Where(pet => pet.OwnerId == id).ToListAsync();

                // Remove owner of animal
                foreach (var pet in petsOfOwner)
                {
                    try
                    {
                        await this.petService.DeletePet(pet.Id);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                try
                {
                    dbContext.Owners.Remove(ownerFound);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
