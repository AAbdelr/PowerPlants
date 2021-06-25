using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlants.Models
{
    public class ResponseModel
    {
        public ResponseModel(string name, double power)
        {
            Name = name;
            Power = power;
        }

        public string Name { get; set; }
        public double Power { get; set; }
    }
}
