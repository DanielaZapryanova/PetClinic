using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PetClinic.Contracts;
using PetClinic.Controllers;
using PetClinic.Models;
using System;
using System.Data;
using Xunit.Sdk;

namespace PetClinic.Tests
{
    public class OwnerControllerTests
    {
        [Fact]
        public void AddOwner_Returns_AddOwner_View()
        {
            // Arrange
            IOwnerService ownerServiceMock = new Mock<IOwnerService>().Object;
            var controller = new OwnerController(ownerServiceMock);

            // Act
            var result = controller.AddOwner() as ViewResult;

            // Assert
            Assert.Equal("AddOwner", result?.ViewName);
            Assert.True(result?.Model is AddOwnerViewModel);
        }

        [Fact]
        public async Task AddOwner_AddOwner_Post_Calls_OwnerService()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.AddOwner(It.IsAny<AddOwnerViewModel>())).Returns(Task.FromResult(true));

            var controller = new OwnerController(mockedOwnerService.Object);

            AddOwnerViewModel ownerToAdd = new AddOwnerViewModel()
            {
                FullName = "Test Owner Full Name",
                Address = "Test Owner Address",
                Age = 34,
                Telephone = "+359888444765",
            };

            // Act
            var result = await controller.AddOwner(ownerToAdd) as RedirectToActionResult;

            // Assert
            mockedOwnerService.Verify(ownerService => ownerService.AddOwner(ownerToAdd));
        }

        [Fact]
        public async Task AddOwner_AddOwner_Post_Calls_OwnerService_Add_Added_Successfully()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.AddOwner(It.IsAny<AddOwnerViewModel>())).Returns(Task.FromResult(true));

            var controller = new OwnerController(mockedOwnerService.Object);

            AddOwnerViewModel ownerToAdd = new AddOwnerViewModel()
            {
                FullName = "Test Owner Full Name",
                Address = "Test Owner Address",
                Age = 34,
                Telephone = "+359888444765",
            };

            // Act
            var result = await controller.AddOwner(ownerToAdd) as RedirectToActionResult;

            // Assert
            Assert.Equal("All", result?.ActionName);
        }

        [Fact]
        public async Task AddOwner_AddOwner_Post_Calls_OwnerService_Add_Added_Unsuccessfully()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.AddOwner(It.IsAny<AddOwnerViewModel>())).Returns(Task.FromResult(false));

            var controller = new OwnerController(mockedOwnerService.Object);

            AddOwnerViewModel ownerToAdd = new AddOwnerViewModel()
            {
                FullName = "Test Owner Full Name",
                Address = "Test Owner Address",
                Age = 34,
                Telephone = "+359888444765",
            };

            // Act
            var result = await controller.AddOwner(ownerToAdd) as ViewResult;

            // Assert
            Assert.Equal("Error", result?.ViewName);
        }

        [Fact]
        public async Task AddOwner_AddOwner_Post_Invalid_Age()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedOwnerService = repository.Create<IOwnerService>();

            var controller = new OwnerController(mockedOwnerService.Object);

            AddOwnerViewModel ownerToAdd = new AddOwnerViewModel()
            {
                FullName = "Test Owner Full Name",
                Address = "Test Owner Address",
                Age = 9,
                Telephone = "+359888444765",
            };

            // Act
            var result = await controller.AddOwner(ownerToAdd) as ViewResult;

            // Assert
            Assert.Equal("AddOwner", result?.ViewName);
            Assert.True(result?.Model is AddOwnerViewModel);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}
