using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private const double BusIncreasedConsumption = 1.4;
        public Bus(double fuelQuantity, double consumtion, double tankCapacity) 
            : base(fuelQuantity, consumtion, tankCapacity, BusIncreasedConsumption)
        {
        }
    }
}
