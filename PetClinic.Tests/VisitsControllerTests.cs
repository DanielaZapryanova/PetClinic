using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using PetClinic.Contracts;
using PetClinic.Controllers;
using PetClinic.Data.Models;
using PetClinic.Models;
using PetClinic.Services;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PetClinic.Tests
{
    public class VisitsControllerTests
    {
        [Fact]
        public async Task AllVisits_Returns_AllVisits()
        {
            // Arrange
            IList<VisitViewModel> visitsResultFromService = new List<VisitViewModel>();
            visitsResultFromService.Add(new VisitViewModel()
            {
                Id = 1,
                Date = DateTime.Now,
                Price = 10.5m,
                Veterinarian = "Test vet 1",
                Pet = "Test pet 1",
                ReasonForVisit = Reason.BloodTest,
            }); 
            visitsResultFromService.Add(new VisitViewModel()
            {
                Id = 2,
                Date = DateTime.Now,
                Price = 12.5m,
                Veterinarian = "Test vet 2",
                Pet = "Test pet 3",
                ReasonForVisit = Reason.BloodTest,
            });
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedVisitsService = repository.Create<IVisitsService>();
            mockedVisitsService.Setup(f => f.AllVisit()).Returns(Task.FromResult(visitsResultFromService));
            var mockedPetService = repository.Create<IPetService>();
            var mockedVetService = repository.Create<IVetService>();

            var controller = new VisitsController(mockedVisitsService.Object, mockedPetService.Object, mockedVetService.Object);

            // Act 
            var result = await controller.AllVisit() as ViewResult;

            // Assert
            Assert.Equal("AllVisit", result?.ViewName);
            Assert.True(result?.Model is IList<VisitViewModel>);
            Assert.Equal("Test pet 1", ((IList<VisitViewModel>)result?.Model).First().Pet);
            Assert.Equal("Test pet 3", ((IList<VisitViewModel>)result?.Model).Last().Pet);
        }

        [Fact]
        public async Task Visits_Returns_Visits()
        {
            // Arrange
            IList<VisitViewModel> visitsResultFromService = new List<VisitViewModel>();
            visitsResultFromService.Add(new VisitViewModel()
            {
                Id = 1,
                Date = DateTime.Now,
                Price = 10.5m,
                Veterinarian = "Test vet 1",
                Pet = "Test pet 1",
                ReasonForVisit = Reason.BloodTest,
            });
            visitsResultFromService.Add(new VisitViewModel()
            {
                Id = 2,
                Date = DateTime.Now,
                Price = 12.5m,
                Veterinarian = "Test vet 2",
                Pet = "Test pet 1",
                ReasonForVisit = Reason.BloodTest,
            });
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedVisitsService = repository.Create<IVisitsService>();
            mockedVisitsService.Setup(f => f.Visits(It.IsAny<int>())).Returns(Task.FromResult(visitsResultFromService));
            var mockedPetService = repository.Create<IPetService>();
            var mockedVetService = repository.Create<IVetService>();

            var controller = new VisitsController(mockedVisitsService.Object, mockedPetService.Object, mockedVetService.Object);

            // Act 
            var result = await controller.Visits(2) as ViewResult;

            // Assert
            Assert.Equal("Visits", result?.ViewName);
            Assert.True(result?.Model is IList<VisitViewModel>);
            Assert.Equal("Test pet 1", ((IList<VisitViewModel>)result?.Model).First().Pet);
            Assert.Equal("Test pet 1", ((IList<VisitViewModel>)result?.Model).Last().Pet);
        }

        [Fact]
        public async Task AddVisit_Returns_AddVisit_View()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            var mockedVisitsService = repository.Create<IVisitsService>();
            mockedVisitsService.Setup(f => f.GetPossibleReasons()).Returns(new List<ReasonViewModel>());
            IList<PetViewModel> allPetsFromService = new List<PetViewModel>();
            var mockedPetService = repository.Create<IPetService>();
            mockedPetService.Setup(f => f.GetAllPets()).Returns(Task.FromResult(allPetsFromService));
            IList<VeterinarianViewModel> allVetsFromService = new List<VeterinarianViewModel>();
            var mockedVetService = repository.Create<IVetService>();
            mockedVetService.Setup(f => f.GetAllActiveVets()).Returns(Task.FromResult(allVetsFromService));

            var controller = new VisitsController(mockedVisitsService.Object, mockedPetService.Object, mockedVetService.Object);

            // Act 
            var result = await controller.AddVisit() as ViewResult;

            // Assert
            Assert.Equal("AddVisit", result?.ViewName);
            Assert.True(result?.Model is AddVisitViewModel);
        }

        [Fact]
        public async Task AddVisit_Returns_AddVaccination_View()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            IList<VaccineViewModel> allVaccinesFromService = new List<VaccineViewModel>();
            var mockedVisitsService = repository.Create<IVisitsService>();
            mockedVisitsService.Setup(f => f.GetPossibleVaccines()).Returns(Task.FromResult(allVaccinesFromService));
            IList<PetViewModel> allPetsFromService = new List<PetViewModel>();
            var mockedPetService = repository.Create<IPetService>();
            mockedPetService.Setup(f => f.GetAllPets()).Returns(Task.FromResult(allPetsFromService));
            IList<VeterinarianViewModel> allVetsFromService = new List<VeterinarianViewModel>();
            var mockedVetService = repository.Create<IVetService>();
            mockedVetService.Setup(f => f.GetAllActiveVets()).Returns(Task.FromResult(allVetsFromService));

            var controller = new VisitsController(mockedVisitsService.Object, mockedPetService.Object, mockedVetService.Object);

            // Act 
            var result = await controller.AddVaccination() as ViewResult;

            // Assert
            Assert.Equal("AddVaccination", result?.ViewName);
            Assert.True(result?.Model is AddVaccinationViewModel);
        }

        [Fact]
        public async Task AddVisit_Adds_Visit()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            IList<VaccineViewModel> allVaccinesFromService = new List<VaccineViewModel>();
            var mockedVisitsService = repository.Create<IVisitsService>();
            mockedVisitsService.Setup(f => f.AddVisit(It.IsAny<AddVisitViewModel>())).Returns(Task.FromResult(true));
            var mockedPetService = repository.Create<IPetService>();
            var mockedVetService = repository.Create<IVetService>();

            var controller = new VisitsController(mockedVisitsService.Object, mockedPetService.Object, mockedVetService.Object);

            AddVisitViewModel addVisitViewModel = new AddVisitViewModel()
            {
                Date = DateTime.Now,
                PetId = 1,
                VetId = 1,
                Price = 10,
                ReasonForVisit = Reason.Examinations,
            };
            // Act 
            var result = await controller.AddVisit(addVisitViewModel) as ViewResult;

            // Assert
            mockedVisitsService.Verify(visitsService => visitsService.AddVisit(addVisitViewModel));
        }

        [Fact]
        public async Task AddVaccination_Adds_Vaccination()
        {
            // Arrange
            var repository = new MockRepository(MockBehavior.Strict);
            IList<VaccineViewModel> allVaccinesFromService = new List<VaccineViewModel>();
            var mockedVisitsService = repository.Create<IVisitsService>();
            mockedVisitsService.Setup(f => f.AddVaccination(It.IsAny<AddVaccinationViewModel>())).Returns(Task.FromResult(true));
            var mockedPetService = repository.Create<IPetService>();
            var mockedVetService = repository.Create<IVetService>();

            var controller = new VisitsController(mockedVisitsService.Object, mockedPetService.Object, mockedVetService.Object);

            AddVaccinationViewModel addVaccinationViewModel = new AddVaccinationViewModel()
            {
                Date = DateTime.Now,
                PetId = 1,
                VetId = 1,
                Price = 10,
            };
            // Act 
            var result = await controller.AddVaccination(addVaccinationViewModel) as ViewResult;

            // Assert
            mockedVisitsService.Verify(visitsService => visitsService.AddVaccination(addVaccinationViewModel));
        }
    }
}