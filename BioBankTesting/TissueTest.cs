using BioBank.Server.Controllers;
using BioBank.Shared.Database;
using BioBank.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BioBankTesting
{
    public class TissueTest
    {
        [Fact]
        public async Task AddNewTissueAsync()
        {
            // Arrange
            var newtissue = new Mock<IDbService>();
            var TestSample = new Tissue { Id = 1, DieaseTerm="Test",Title="Testtitle" };
            newtissue.Setup(x => x.AddNewCollection(TestSample)).Returns(true);

            var controller = new BioBankController(newtissue.Object);

            // Act
            var actionResult = await controller.Post(TestSample);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.Equal("Data saved successfully!", okResult?.Value);

        }
        [Fact]
        public async Task GetTissue()
        {
            // Arrange
            var newtissue = new Mock<IDbService>();
            var TestTissue = new List<Tissue> { new Tissue { Id = 1, DieaseTerm = "Test", Title = "Testtitle" } };
            newtissue.Setup(x => x.GetBioBankCollections()).Returns(TestTissue);

            var controller = new BioBankController(newtissue.Object);

            // Act
            var actionResult = await controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.Equal(200, okResult?.StatusCode);

        }
    }
}