using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double TruckIncreasetConsumption = 1.6;
        public Truck(double fuelQuantity, double consumtion, double tankCapacity) 
            : base(fuelQuantity, consumtion, tankCapacity, TruckIncreasetConsumption)
        {
        }

        public override void Refuel(double litres)
        {
            if (TankCapacity < FuelQuantity + litres)
            {
                throw new ArgumentException($"Cannot fit {litres} fuel in the tank");
            }
            base.Refuel(litres * 0.95);
        }   
    }
}
