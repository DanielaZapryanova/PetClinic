using Microsoft.EntityFrameworkCore;
using PetClinic.Data.Models;
using PetClinic.Data;
using PetClinic.Models;
using PetClinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Tests
{
    public class VetServiceTests
    {
        [Fact]
        public async Task AddVetAddsVet()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to add
                var service = new VetService(db);
                AddVeterinarianViewModel addVetViewModel = new AddVeterinarianViewModel()
                {
                    FullName = "Test 2",
                    Telephone = "+359888445533",
                    Specialization = "Test 2",
                };

                // Act
                var result = await service.AddVet(addVetViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the added pet has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var vet = db.Set<Vet>().Where(vet => vet.FullName.Equals("Test 2", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (vet == null)
                {
                    throw new Exception("Vet not saved in db successfully");
                }
                Assert.Equal("Test 2", vet.FullName);
                Assert.Equal("+359888445533", vet.Telephone);
                Assert.Equal("Test 2", vet.Specialization);
            }
        }

        [Fact]
        public async Task EditVetEditsVet()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VetService(db);
                EditVeterinarianViewModel editVetViewModel = new EditVeterinarianViewModel()
                {
                    Id = 1,
                    FullName = "Test 2",
                    Telephone = "+359888445533",
                    Specialization = "Test 2",
                };

                // Act
                var result = await service.EditVet(editVetViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the edited pet has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var vet = db.Set<Vet>().Where(vet => vet.FullName.Equals("Test 2", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (vet == null)
                {
                    throw new Exception("Vet not saved in db successfully");
                }
                Assert.Equal("Test 2", vet.FullName);
                Assert.Equal("+359888445533", vet.Telephone);
                Assert.Equal("Test 2", vet.Specialization);
            }
        }

        [Fact]
        public async Task GetAllVetsGetsAllVets()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 2",
                    Telephone = "+359888445533",
                    Specialization = "Test 2",
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VetService(db);

                // Act
                var result = await service.GetAllVets();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task GetAllIsActiveVetsGetsAllIsActiveVets()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                    IsActive = true,
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 2",
                    Telephone = "+359888445533",
                    Specialization = "Test 2",
                    IsActive = false,
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VetService(db);

                // Act
                var result = await service.GetAllActiveVets();

                // Assert
                Assert.Equal(1, result.Count);
            }
        }

        [Fact]
        public async Task MakeVeterinarianInactive()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                    IsActive = true,
                });

                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VetService(db);

                // Act
                var result = await service.MakeVeterinarianInactive(1);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async Task MakeVeterinarianActive()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                    IsActive = true,
                });

                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VetService(db);

                // Act
                var result = await service.MakeVeterinarianActive(1);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async Task GetVetsGetsVets()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vet>().Add(new Vet()
                {
                    Id = 1,
                    FullName = "Test 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VetService(db);

                // Act
                var result = await service.GetVet(1);

                // Assert
                Assert.Equal(1,result.Id);
            }
        }
    }
}
