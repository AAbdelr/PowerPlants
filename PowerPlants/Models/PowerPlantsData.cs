using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlants.Models
{
    public class PowerPlantsData
    {
        public int Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<PowerPlantsDetails> PowerPlants { get; set; }
    }
}
