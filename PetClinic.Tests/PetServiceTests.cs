using Microsoft.EntityFrameworkCore;
using PetClinic.Data;
using PetClinic.Data.Models;
using PetClinic.Models;
using PetClinic.Services;

namespace PetClinic.Tests
{
    public class PetServiceTests
    {
        [Fact]
        public async Task AddPetAddsPet()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 0,
                });
                await db.SaveChangesAsync();
            }

            DateTime petDateOfBirth = DateTime.Now;

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to add
                var service = new PetService(db);
                AddPetViewModel addPetViewModel = new AddPetViewModel()
                {
                    Name = "test name 2",
                    DateOfBirth = petDateOfBirth,
                    Breed = "Shepherd",
                    Gender = "Male",
                    Color = "Blue",
                    Weight = 34,
                    OwnerId = 1,
                };

                // Act
                var result = await service.AddPet(addPetViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the added pet has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var pet = db.Set<Pet>().Where(pet => pet.Name.Equals("test name 2", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (pet == null)
                {
                    throw new Exception("pet not saved in db successfully");
                }
                Assert.Equal("test name 2", pet.Name);
                Assert.Equal(petDateOfBirth, pet.DateOfBirth);
                Assert.Equal("Shepherd", pet.Breed);
                Assert.Equal("Male", pet.Gender);
                Assert.Equal("Blue", pet.Color);
                Assert.Equal(34, pet.Weight);
                Assert.Equal(1, pet.OwnerId);
            }
        }

        [Fact]
        public async Task EditPetEditsPet()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 0,
                });
                await db.SaveChangesAsync();
            }

            DateTime petDateOfBirth = DateTime.Now;

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new PetService(db);
                EditPetViewModel editPetViewModel = new EditPetViewModel()
                {
                    Id = 1,
                    Name = "Test pet edited",
                    DateOfBirth = petDateOfBirth,
                    Breed = "Doggo",
                    Color = "Red",
                    Gender = "Female",
                    Weight = 13,
                    OwnerId = 16
                };

                // Act
                var result = await service.EditPet(editPetViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the edited pet has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var pet = db.Set<Pet>().FirstOrDefault();
                if (pet == null)
                {
                    throw new Exception("pet not saved in db successfully");
                }
                Assert.Equal("Test pet edited", pet.Name);
                Assert.Equal(petDateOfBirth, pet.DateOfBirth);
                Assert.Equal("Doggo", pet.Breed);
                Assert.Equal("Female", pet.Gender);
                Assert.Equal("Red", pet.Color);
                Assert.Equal(13, pet.Weight);
                Assert.Equal(16, pet.OwnerId);
            }
        }


        [Fact]
        public async Task GetAllPetsGetsAllPets()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 1,
                });
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 2,
                });
                db.Set<Owner>().Add(new Owner()
                {
                    FullName = "Owner 1",
                    Address = "Owner 1 address",
                    Age = 33,
                    Telephone = "+359876543211",
                });
                db.Set<Owner>().Add(new Owner()
                {
                    FullName = "Owner 2",
                    Address = "Owner 2 address",
                    Age = 33,
                    Telephone = "+359876543212",
                });
                await db.SaveChangesAsync();
            }

            DateTime petDateOfBirth = DateTime.Now;

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new PetService(db);

                // Act
                var result = await service.GetAllPets();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }
    }
}
