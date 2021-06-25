using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using PowerPlants.Controllers;
using PowerPlants.Hubs;
using PowerPlants.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerPlantsTests
{
    [Route("api/test/[controller]")]
    [ApiController]
    public class PowerPlantsControllerTests : ControllerBase
    {
        /// <summary>
        /// Test method for the PowerPlantsController
        ///  - Right click on the method and click Run to test
        /// </summary>
        [Test]
        public void PostTest()
        {
            var mock = new Mock<ILogger<PowerPlantsController>>();
            var mock2 = new Mock<IHubContext<SignalRHub>>();
            var controller = new PowerPlantsController(mock.Object, mock2.Object);

            string json = System.IO.File.ReadAllText(@"Mock/payload1.json");

            PowerPlantsData data = JsonConvert.DeserializeObject<PowerPlantsData>(json);

            var result = controller.Post(data);

            Assert.IsInstanceOf<List<ResponseModel>>(result);
        }
    }
}
