using System.Collections.Generic;
using Moq;
using BioBank.Shared.Database;
using BioBank.Shared.Models;
using BioBank.Server;
using BioBank.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BioBankTesting
{
    public class SampleTest
    {
        [Fact]
        public async Task AddNewSampleAsync()
        {
            // Arrange
            var newsample = new Mock<IDbService>();
            var TestSample = new Samples { BioBankId = 1, Sample_Id = 1, Donor_Count = 1, Material_Type = "TestSample", Last_updated = DateTime.Now };
            newsample.Setup(x => x.AddNewSample(TestSample)).Returns(true);

            var controller = new SamplesController(newsample.Object);

            // Act
            var actionResult = await controller.Post(TestSample);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.Equal("Data saved successfully!", okResult?.Value);

        }
        [Fact]
        public async Task GetSample()
        {
            var newsample = new Mock<IDbService>();
            var TestSample = new List<Samples> { new Samples { BioBankId = 1, Sample_Id = 1, Donor_Count = 1, Material_Type = "TestSample", Last_updated = DateTime.Now } };
            newsample.Setup(x => x.GetSamples(1)).Returns(TestSample);

            var controller = new SamplesController(newsample.Object);

            // Act
            var actionResult = await controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.Equal(200, okResult?.StatusCode);

        }
    }
}