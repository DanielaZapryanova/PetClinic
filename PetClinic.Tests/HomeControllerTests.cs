using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Moq;
using PetClinic.Controllers;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PetClinic.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_Returns_Index_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act 
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void ForUs_Returns_ForUs_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act
            var result = controller.ForUs() as ViewResult;

            // Assert
            Assert.Equal("ForUs", result?.ViewName);
        }

        [Fact]
        public void Contacts_Returns_Contacts_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act
            var result = controller.Contacts() as ViewResult;

            // Assert
            Assert.Equal("Contacts", result?.ViewName);
        }

        [Fact]
        public void ImportantInformation_Returns_ImportantInformation_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act
            var result = controller.ImportantInformation() as ViewResult;

            // Assert
            Assert.Equal("ImportantInformation", result?.ViewName);
        }

        [Fact]
        public void HomeVisits_Returns_HomeVisits_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act
            var result = controller.HomeVisits() as ViewResult;

            // Assert
            Assert.Equal("HomeVisits", result?.ViewName);
        }

        [Fact]
        public void Privacy_Returns_Privacy_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act
            var result = controller.Privacy() as ViewResult;

            // Assert
            Assert.Equal("Privacy", result?.ViewName);
        }

        [Fact]
        public void PriceList_Returns_PriceList_View()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> loggerMockObject = loggerMock.Object;
            var controller = new HomeController(loggerMockObject);

            // Act
            var result = controller.PriceList() as ViewResult;

            // Assert
            Assert.Equal("PriceList", result?.ViewName);
        }
    }
}