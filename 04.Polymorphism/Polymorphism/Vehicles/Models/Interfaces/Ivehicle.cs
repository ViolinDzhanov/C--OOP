using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models.Interfaces
{
    public interface Ivehicle
    {
        double FuelQuantity { get; }
        double Consumption { get; }

        string Drive(double distance, bool isConsumptionIncreased = true);
        void Refuel(double litres);
    }
}
