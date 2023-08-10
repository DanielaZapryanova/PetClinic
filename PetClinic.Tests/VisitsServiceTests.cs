using Microsoft.EntityFrameworkCore;
using Moq;
using PetClinic.Contracts;
using PetClinic.Data.Models;
using PetClinic.Data;
using PetClinic.Models;
using PetClinic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PetClinic.Tests
{
    public class VisitsServiceTests
    {
        [Fact]
        public async Task AddVisitsAddsVisits()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Visit>().Add(new Visit()
                {
                    Date = DateTime.Now,
                    PetId = 1,
                    VetId = 1,
                    Price = 25,
                    ReasonForVisit = Reason.Examinations,
                });
                await db.SaveChangesAsync();
            }
            DateTime dataVisit = DateTime.Now;
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to add
                var service = new VisitsService(db);
                AddVisitViewModel addVisitViewModel = new AddVisitViewModel()
                {
                    Date = dataVisit,
                    PetId = 2,
                    VetId = 2,
                    Price = 30,
                    ReasonForVisit = Reason.Vaccination,
                };

                // Act
                var result = await service.AddVisit(addVisitViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the added pet has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var visit = db.Set<Visit>().Where(visit => visit.PetId == 2).FirstOrDefault();
                if (visit == null)
                {
                    throw new Exception("Visit not saved in db successfully");
                }
                Assert.Equal(dataVisit, visit.Date);
                Assert.Equal(2, visit.PetId);
                Assert.Equal(2, visit.VetId);
                Assert.Equal(30, visit.Price);
                Assert.Equal(Reason.Vaccination, visit.ReasonForVisit);
            }
        }

        [Fact]
        public async Task GetVisitsGetsVisits()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            DateTime dataVisit = DateTime.Now;

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Visit>().Add(new Visit()
                {
                    Date = dataVisit,
                    PetId = 2,
                    VetId = 2,
                    Price = 30,
                    ReasonForVisit = Reason.Vaccination,
                });

                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VisitsService(db);

                // Act
                var result = await service.Visits(1);

                // Assert
                var visit = db.Set<Visit>().FirstOrDefault();

                Assert.Equal(dataVisit, visit.Date);
                Assert.Equal(2, visit.PetId);
                Assert.Equal(2, visit.VetId);
                Assert.Equal(30, visit.Price);
                Assert.Equal(Reason.Vaccination, visit.ReasonForVisit);
            }
        }

        [Fact]
        public void GetPossibleReasons()
        {
            ReasonViewModel examinationReason = new ReasonViewModel()
            {
                Reason = Reason.Examinations,
                FriendlyName = "Преглед"
            };
            ReasonViewModel groomingReason = new ReasonViewModel()
            {
                Reason = Reason.Grooming,
                FriendlyName = "Груминг"
            };
            ReasonViewModel bloodTestReason = new ReasonViewModel()
            {
                Reason = Reason.BloodTest,
                FriendlyName = "Кръвни тестове"
            };

            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VisitsService(db);

                // Act
                IList<ReasonViewModel> result = service.GetPossibleReasons();

                // Assert
                Assert.Equal(3, result.Count);
                result.Contains(examinationReason);
                result.Contains(groomingReason);
                result.Contains(bloodTestReason);
            }
        }

        [Fact]
        public async Task GetPossibleVaccines()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            DateTime firstVaccineExpirationTime = DateTime.Now.AddDays(100);

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name = "Test 1",
                    NumberInStock = 100,
                    ExpirationTime = firstVaccineExpirationTime
                });
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name = "Test 2",
                    NumberInStock = 0,
                    ExpirationTime = DateTime.Now.AddDays(100)
                });
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name = "Test 3",
                    NumberInStock = 100,
                    ExpirationTime = DateTime.Now.AddDays(-1)
                });

                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VisitsService(db);

                // Act
                var result = await service.GetPossibleVaccines();

                // Assert
                Assert.Equal(1, result.Count);
                Assert.Equal("Test 1", result.First().Name);
                Assert.Equal(100, result.First().NumberInStock);
                Assert.Equal(firstVaccineExpirationTime, result.First().DateOfExpiry);
            }
        }


        [Fact]
        public async Task AllVisitReturnsVisits()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            DateTime firstVisitDateTime = DateTime.Now;
            DateTime secondVisitDateTime = DateTime.Now;

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
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet 2",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 0,
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test vet 1",
                    Telephone = "+359888777666",
                    Specialization = "Grooming"
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test vet 2",
                    Telephone = "+359888777666",
                    Specialization = "Grooming"
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test vet 3",
                    Telephone = "+359888777666",
                    Specialization = "Grooming"
                });
                db.Set<Visit>().Add(new Visit()
                {
                    Date = firstVisitDateTime,
                    PetId = 1,
                    VetId = 1,
                    Price = 10.5m,
                    ReasonForVisit = Reason.Grooming
                });
                db.Set<Visit>().Add(new Visit()
                {
                    Date = secondVisitDateTime,
                    PetId = 2,
                    VetId = 3,
                    Price = 12.5m,
                    ReasonForVisit = Reason.Examinations
                });

                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new VisitsService(db);

                // Act
                var result = await service.AllVisit();

                // Assert
                Assert.Equal(2, result.Count);
                Assert.Equal(firstVisitDateTime, result.First().Date);
                Assert.Equal("Test pet", result.First().Pet);
                Assert.Equal("Test vet 1", result.First().Veterinarian);
                Assert.Equal(10.5m, result.First().Price);
                Assert.Equal(Reason.Grooming, result.First().ReasonForVisit);
                Assert.Equal(secondVisitDateTime, result.Last().Date);
                Assert.Equal("Test pet 2", result.Last().Pet);
                Assert.Equal("Test vet 3", result.Last().Veterinarian);
                Assert.Equal(12.5m, result.Last().Price);
                Assert.Equal(Reason.Examinations, result.Last().ReasonForVisit);
            }
        }

        [Fact]
        public async Task AddVaccinationAddsVaccination()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            // Arrange db and fill it up
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // fix up some data
                db.Set<Owner>().Add(new Owner()
                {
                    FullName = "Owner 1",
                    Address = "Owner 1 address",
                    Age = 33,
                    Telephone = "+359876543211",
                });
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet 1",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 0,
                });
                db.Set<Pet>().Add(new Pet()
                {
                    Name = "Test pet 2",
                    DateOfBirth = DateTime.Now,
                    Breed = "Puppy",
                    Color = "Green",
                    Gender = "Female",
                    Weight = 11,
                    OwnerId = 0,
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test vet 1",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                });
                db.Set<Vet>().Add(new Vet()
                {
                    FullName = "Test vet 2",
                    Telephone = "+359888445522",
                    Specialization = "Test 1",
                });
                db.Set<Vaccine>().Add(new Vaccine()
                {
                    Name = "Pfizer",
                    NumberInStock = 100,
                    ExpirationTime = DateTime.Now.AddDays(100),
                });
                await db.SaveChangesAsync();
            }
            DateTime dataVisit = DateTime.Now;
            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to add
                var service = new VisitsService(db);
                AddVaccinationViewModel addVaccinationViewModel = new AddVaccinationViewModel()
                {
                    Date = dataVisit,
                    PetId = 2,
                    VetId = 2,
                    Price = 30,
                    VaccineId = 1
                };

                // Act
                var result = await service.AddVaccination(addVaccinationViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the added pet has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var visit = db.Set<Visit>().Where(visit => visit.PetId == 2).FirstOrDefault();
                if (visit == null)
                {
                    throw new Exception("Visit not saved in db successfully");
                }
                Assert.Equal(dataVisit, visit.Date);
                Assert.Equal(2, visit.PetId);
                Assert.Equal(2, visit.VetId);
                Assert.Equal(30, visit.Price);
                Assert.Equal(Reason.Vaccination, visit.ReasonForVisit);
            }
        }
    }
}
