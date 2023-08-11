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
using Microsoft.AspNetCore.Mvc;
using Moq;
using PetClinic.Contracts;
using PetClinic.Controllers;

namespace PetClinic.Tests
{
    public class PetControllerTests
    {
        [Fact]
        public async Task AddPet_Returns_AddPet_View()
        {
            // Arrange
            IOwnerService ownerServiceMock = new Mock<IOwnerService>().Object;
            IPetService petServiceMock = new Mock<IPetService>().Object;
            var controller = new PetController(petServiceMock, ownerServiceMock);

            // Act
            var result = await controller.AddPet() as ViewResult;

            // Assert
            Assert.True(result?.Model is AddPetViewModel);
        }

        [Fact]
        public async Task All_Returns_All()
        {
            // Arrange
            IList<PetViewModel> petsFromService = new List<PetViewModel>();
            petsFromService.Add(new PetViewModel()
            {
                Id = 1,
                Name = "Test pet 1",
                DateOfBirth = DateTime.Now,
                Breed = "Puppy",
                Color = "Green",
                Gender = "Female",
                Weight = 11,
                OwnerId = 1,
            });
            petsFromService.Add(new PetViewModel()
            {
                Id = 2,
                Name = "Test pet 2",
                DateOfBirth = DateTime.Now,
                Breed = "Puppy",
                Color = "Green",
                Gender = "Female",
                Weight = 11,
                OwnerId = 2,
            });
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedPetService = repository.Create<IPetService>();
            mockedPetService.Setup(f => f.GetAllPets()).Returns(Task.FromResult(petsFromService));
            IOwnerService ownerServiceMock = new Mock<IOwnerService>().Object;

            var controller = new PetController(mockedPetService.Object,ownerServiceMock);

            // Act 
            var result = await controller.All() as ViewResult;

            // Assert
            Assert.True(result?.Model is IList<PetViewModel>);
            Assert.Equal("Test pet 1", ((IList<PetViewModel>)result?.Model).First().Name);
            Assert.Equal("Test pet 2", ((IList<PetViewModel>)result?.Model).Last().Name);
        }
    }
}
