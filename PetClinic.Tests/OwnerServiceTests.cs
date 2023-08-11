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
using PetClinic.Contracts;
using Moq;
using PetClinic.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace PetClinic.Tests
{
    public class OwnerServiceTests
    {
        [Fact]
        public async Task AddOwnerAddsOwner()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");

            IPetService petServiceMock = new Mock<IPetService>().Object;

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
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to add
                var service = new OwnerService(db, petServiceMock);
                AddOwnerViewModel addOwnerViewModel = new AddOwnerViewModel()
                {
                    FullName = "Owner 2",
                    Address = "Owner 2 address",
                    Age = 33,
                    Telephone = "+359876543212"
                };

                // Act
                var result = await service.AddOwner(addOwnerViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the added owner has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var owner = db.Set<Owner>().Where(owner => owner.FullName.Equals("Owner 2", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (owner == null)
                {
                    throw new Exception("owner not saved in db successfully");
                }
                Assert.Equal("Owner 2", owner.FullName);
                Assert.Equal("Owner 2 address", owner.Address);
                Assert.Equal(33, owner.Age);
                Assert.Equal("+359876543212", owner.Telephone);
            }
        }

        [Fact]
        public async Task EditOwnerEditsOwner()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            IPetService petServiceMock = new Mock<IPetService>().Object;

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
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the owner to edit
                var service = new OwnerService(db, petServiceMock);
                EditOwnerViewModel editOwnerViewModel = new EditOwnerViewModel()
                {
                    Id = 1,
                    FullName = "Owner 2",
                    Address = "Owner 2 address",
                    Age = 33,
                    Telephone = "+359876543212"

                };

                // Act
                var result = await service.EditOwner(editOwnerViewModel);

                // Assert
                Assert.True(result);
            }

            // Assert the edited owner has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var owner = db.Set<Owner>().FirstOrDefault();
                if (owner == null)
                {
                    throw new Exception("Owner not saved in db successfully");
                }
                Assert.Equal("Owner 2", owner.FullName);
                Assert.Equal("Owner 2 address", owner.Address);
                Assert.Equal(33, owner.Age);
                Assert.Equal("+359876543212", owner.Telephone);
            }
        }

        [Fact]
        public async Task DeleteOwnerDeletesOwner()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            IPetService petServiceMock = new Mock<IPetService>().Object;

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
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the pet to edit
                var service = new OwnerService(db,petServiceMock);

                // Act
                var result = await service.DeleteOwner(1);

                // Assert
                Assert.True(result);
            }

            // Assert the edited owner has correct properties

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                var ownerCount = db.Set<Owner>().Count();

                Assert.Equal(0, ownerCount);
            }
        }

        [Fact]
        public async Task GetAllOwnersGetsAllOwners()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            IPetService petServiceMock = new Mock<IPetService>().Object;

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
                db.Set<Owner>().Add(new Owner()
                {
                    FullName = "Owner 2",
                    Address = "Owner 2 address",
                    Age = 33,
                    Telephone = "+359876543212"
                });
                
                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the owner to edit
                var service = new OwnerService(db, petServiceMock);

                // Act
                var result = await service.GetAllOwners();

                // Assert
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task GetOwnersGetsOwners()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<PetClinic.Data.ApplicationDbContext>().UseInMemoryDatabase(databaseName: "petClinicDatabase");
            IPetService petServiceMock = new Mock<IPetService>().Object;

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

                await db.SaveChangesAsync();
            }

            using (var db = new ApplicationDbContext(dbOptionsBuilder.Options))
            {
                // Create the service and the owner to edit
                var service = new OwnerService(db, petServiceMock);

                // Act
                var result = await service.GetOwner(1);

                // Assert
                var owner = db.Set<Owner>().FirstOrDefault();

                Assert.Equal("Owner 1", owner.FullName);
                Assert.Equal("Owner 1 address", owner.Address);
                Assert.Equal(33, owner.Age);
                Assert.Equal("+359876543211", owner.Telephone);
            }
        }
    }
}
