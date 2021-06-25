using Microsoft.AspNetCore.SignalR;
using PowerPlants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlants.Hubs
{
    public class SignalRHub : Hub
    {
        public async Task SendMessage(PowerPlantsData powerPlantsData, List<ResponseModel> response)
        {
            await Clients.All.SendAsync("ReceiveMessage", powerPlantsData, response);
        }
    }
}
