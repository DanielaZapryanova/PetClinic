using Microsoft.AspNetCore.Mvc;
using Moq;
using PetClinic.Contracts;
using PetClinic.Controllers;
using PetClinic.Data.Models;
using PetClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Tests
{
    public class VaccineControllerTests
    {
        [Fact]
        public async Task All_Returns_All()
        {
            // Arrange
            IList<VaccineViewModel> vaccineFromService = new List<VaccineViewModel>();
            vaccineFromService.Add(new VaccineViewModel()
            {
                Id = 1,
                Name = "test name 2",
                NumberInStock = 2,
                DateOfExpiry = DateTime.Now,
            });
            vaccineFromService.Add(new VaccineViewModel()
            {
                Id = 2,
                Name = "test name",
                NumberInStock = 1,
                DateOfExpiry = DateTime.Now,
            });
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedVaccineService = repository.Create<IVaccineService>();
            mockedVaccineService.Setup(f => f.GetAllVaccines()).Returns(Task.FromResult(vaccineFromService));

            var controller = new VaccinesController(mockedVaccineService.Object);

            // Act 
            var result = await controller.All() as ViewResult;

            // Assert
            Assert.True(result?.Model is IList<VaccineViewModel>);
            Assert.Equal("test name 2", ((IList<VaccineViewModel>)result?.Model).First().Name);
            Assert.Equal("test name", ((IList<VaccineViewModel>)result?.Model).Last().Name);
        }

        [Fact]
        public async Task AddVaccine_Returns_AddVaccine_View()
        {
            // Arrange
            IVaccineService vaccineServiceMock = new Mock<IVaccineService>().Object;
            var controller = new VaccinesController(vaccineServiceMock);

            AddVaccineViewModel addVaccineViewModel = new AddVaccineViewModel()
            {
                Name = "test name",
                NumberInStock = 1,
                DateOfExpiry = DateTime.Now,
            };
            // Act
            var result = await controller.Add(addVaccineViewModel) as ViewResult;

            // Assert
            Assert.True(result?.Model is AddVaccineViewModel);
        }

    }
}
