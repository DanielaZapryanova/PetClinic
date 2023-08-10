using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PetClinic.Contracts;
using PetClinic.Controllers;
using PetClinic.Data.Models;
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

        [Fact]
        public async Task EditOwner_Returns_EditOwner_Page()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            EditOwnerViewModel editOwnerViewModelFromService = new EditOwnerViewModel()
            {
                Id = 1,
                FullName = "Test owner 1",
                Address = "Test address 1",
                Age = 24,
                Telephone = "+359888777444",
            };
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.GetOwner(It.IsAny<int>())).Returns(Task.FromResult(editOwnerViewModelFromService));

            var controller = new OwnerController(mockedOwnerService.Object);

            // Act
            var result = await controller.EditOwner(2) as ViewResult;

            // Assert
            Assert.Equal("EditOwner", result?.ViewName);
            Assert.True(result?.Model is EditOwnerViewModel);
            Assert.Equal("Test owner 1", ((EditOwnerViewModel)result?.Model).FullName);
        }

        [Fact]
        public async Task EditOwner_Edits_Owner()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            EditOwnerViewModel editOwnerViewModel = new EditOwnerViewModel()
            {
                Id = 1,
                FullName = "Test owner 1",
                Address = "Test address 1",
                Age = 24,
                Telephone = "+359888777444",
            };
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.EditOwner(It.IsAny<EditOwnerViewModel>())).Returns(Task.FromResult(true));

            var controller = new OwnerController(mockedOwnerService.Object);

            // Act
            var result = await controller.EditOwner(editOwnerViewModel) as RedirectToActionResult;

            // Assert
            mockedOwnerService.Verify(ownerService => ownerService.EditOwner(editOwnerViewModel));
            Assert.Equal("All", result?.ActionName);
        }

        [Fact]
        public async Task DeleteOwner_Deletes_Owner()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.DeleteOwner(It.IsAny<int>())).Returns(Task.FromResult(true));

            var controller = new OwnerController(mockedOwnerService.Object);

            // Act
            var result = await controller.DeleteOwner(3) as RedirectToActionResult;

            // Assert
            mockedOwnerService.Verify(ownerService => ownerService.DeleteOwner(3));
            Assert.Equal("All", result?.ActionName);
        }


        [Fact]
        public async Task All_Returns_All()
        {
            // Arrange
            IList<OwnerViewModel> ownersFromService = new List<OwnerViewModel>();
            ownersFromService.Add(new OwnerViewModel()
            {
                Id = 1,
                FullName = "Full name 1",
                Address = "Test address 1",
                Age = 33,
                Telephone = "+359888777666"
            });
            ownersFromService.Add(new OwnerViewModel()
            {
                Id = 2,
                FullName = "Full name 2",
                Address = "Test address 2",
                Age = 33,
                Telephone = "+359888777555"
            });
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedOwnerService = repository.Create<IOwnerService>();
            mockedOwnerService.Setup(f => f.GetAllOwners()).Returns(Task.FromResult(ownersFromService));

            var controller = new OwnerController(mockedOwnerService.Object);

            // Act 
            var result = await controller.All() as ViewResult;

            // Assert
            Assert.Equal("All", result?.ViewName);
            Assert.True(result?.Model is IList<OwnerViewModel>);
            Assert.Equal("Full name 1", ((IList<OwnerViewModel>)result?.Model).First().FullName);
            Assert.Equal("Full name 2", ((IList<OwnerViewModel>)result?.Model).Last().FullName);
        }
    }
}
