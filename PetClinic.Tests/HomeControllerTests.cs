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
    }
}