﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double CarIncreasetConsumption = 0.9;
        public Car(double fuelQuantity, double consumtion, double tankCapacity) 
            : base(fuelQuantity, consumtion, tankCapacity, CarIncreasetConsumption)
        {
        }
    }
}
