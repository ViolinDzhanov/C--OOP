using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public abstract class Vehicle : Ivehicle
    {
        private double increasetConsumption;
        private double fuelQuantity;
       

        protected Vehicle(double fuelQuantity, double consumtion,double tankCapacity, double increasetConsumption)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
            Consumption = consumtion;
            this.increasetConsumption = increasetConsumption;
        }

        public double FuelQuantity 
        {
            get => fuelQuantity;
            private set
            {
                if (TankCapacity < value)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }

        public double Consumption { get; private set; }
        public double TankCapacity { get; private set; }
        
        public string Drive(double distance, bool isConsumptionIncreased = true)
        {
            double consumption = isConsumptionIncreased
                ? Consumption + increasetConsumption
                : Consumption;

            if (FuelQuantity < distance * consumption)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= consumption * distance;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double litres)
        {
            if (litres <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }

            if (TankCapacity < FuelQuantity + litres)
            {
                throw new ArgumentException($"Cannot fit {litres} fuel in the tank");
            }

            FuelQuantity += litres;
        }

        public override string ToString()
        => $"{this.GetType().Name}: {FuelQuantity:f2}";
    }
}
