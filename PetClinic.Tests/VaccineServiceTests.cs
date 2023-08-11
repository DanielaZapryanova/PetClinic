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
    public class VaccineServiceTests
    {
        [Fact]
        public async Task AddVaccineAddsVaccine()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name="test name",
                    NumberInStock=1,
                    ExpirationTime = DateTime.Now,
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to add
                var service = new VaccineService(db);
                AddVaccineViewModel addVaccineViewModel = new AddVaccineViewModel()
                {
                    Name = "test name 2",
                    NumberInStock = 2,
                    DateOfExpiry = DateTime.Now,
                };

                // Act
                var result = await service.AddVaccine(addVaccineViewModel);

                // Assert
                Assert.True(result);
            }

        }

        [Fact]
        public async Task GetAllVaccinesGetsAllVaccines()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name = "test name",
                    NumberInStock = 1,
                    ExpirationTime = DateTime.Now,
                });
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name = "test name 2",
                    NumberInStock = 2,
                    ExpirationTime = DateTime.Now,
                });
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VaccineService(db);

                // Act
                var result = await service.GetAllVaccines();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }
    }
}
