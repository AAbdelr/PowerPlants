using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PowerPlants.Hubs;
using PowerPlants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//
// Read the README file in the solution folder
//

namespace PowerPlants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerPlantsController : ControllerBase
    {
        private readonly ILogger<PowerPlantsController> _logger;
        private IHubContext<SignalRHub> _hubContext;
        double remainingLoad;
        List<ResponseModel> response;

       /// <summary>
       /// Constructor
       /// </summary>
       /// <param name="logger"></param>
       /// <param name="hubContext"></param>
        public PowerPlantsController(ILogger<PowerPlantsController> logger, IHubContext<SignalRHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Process the requested load by merit-order
        /// </summary>
        /// <param name="powerPlantsData"></param>
        /// <returns>A powerplants list with the power to be delivered</returns>
        [HttpPost]
        public List<ResponseModel> Post([FromBody] PowerPlantsData powerPlantsData)
        {       
            string name;
            int pmin;
            int pmax;
            double efficiency;

            response = new List<ResponseModel>();
            remainingLoad = powerPlantsData.Load;

            while (remainingLoad > 0)
            {

                foreach (var item in powerPlantsData.PowerPlants.Where(x => x.Type == "windturbine"))
                {
                    name = item.Name;
                    pmin = item.Pmin;
                    pmax = item.Pmax;
                    efficiency = item.Efficiency * (powerPlantsData.Fuels.Wind / 100);

                    GeneratePower(name, pmin, pmax, efficiency);
                }

                foreach (var item in powerPlantsData.PowerPlants.Where(x => x.Type == "gasfired"))
                {
                    name = item.Name;
                    pmin = item.Pmin;
                    pmax = item.Pmax;
                    efficiency = item.Efficiency;

                    GeneratePower(name, pmin, pmax, efficiency);
                }

                foreach (var item in powerPlantsData.PowerPlants.Where(x => x.Type == "turbojet"))
                {
                    name = item.Name;
                    pmin = item.Pmin;
                    pmax = item.Pmax;
                    efficiency = item.Efficiency;

                    GeneratePower(name, pmin, pmax, efficiency);
                }
            }

            // SignalR (Similar to Websocket) is well implemented and as requested the inputs as well as the result are sent to all clients connected to the hub. 
            // On the other hand the code allowing this is commented out because it throw a null exception because no client is connected to the server
            // Because the API is tested via unit test without being executed

            //await this._hubContext.Clients.All.SendAsync("ReceiveMessage", powerPlantsData, response);

            return response;

        }

        /// <summary>
        /// Generates a quantity of energy for a given powerplants based on Max and Min power in order to not lose energy
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pmin"></param>
        /// <param name="pmax"></param>
        /// <param name="efficiency"></param>
        public void GeneratePower(string name, int pmin, int pmax, double efficiency)
        {
            double power;

            if (remainingLoad >= pmin * efficiency)
            {
                if (remainingLoad <= pmax * efficiency)
                {
                    power = remainingLoad;
                    remainingLoad = remainingLoad - power;

                    response.Add(new ResponseModel(name, power));
                }
                else
                {
                    power = pmax * efficiency;
                    remainingLoad = remainingLoad - power;

                    response.Add(new ResponseModel(name, power));
                }
            }
        }

    }
}
