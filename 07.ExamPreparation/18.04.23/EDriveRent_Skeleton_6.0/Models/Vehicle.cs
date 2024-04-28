using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private double maxMileage;
        private string licenseNumberPlate;
        private int batteryLevel;
        private bool isDamaged;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
           this.maxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;

            this.batteryLevel = 100;
            isDamaged = false;
        }

        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Brand cannot be null or whitespace!");
                }
                brand = value;
            }
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model cannot be null or whitespace!");
                }
                model = value;
            }
        }

        public double MaxMileage => maxMileage;

        public string LicensePlateNumber
        {
            get => licenseNumberPlate;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("License plate number is required!");
                }
                licenseNumberPlate = value;
            }
        }

        public int BatteryLevel => batteryLevel;

        public bool IsDamaged => isDamaged;

        public void ChangeStatus()
        {
            if (!IsDamaged)
            {
                isDamaged = true;
            }
            else
            {
                isDamaged = false;
            }
        }

        public void Drive(double mileage)
        {
            double percentage = Math.Round(mileage / MaxMileage * 100);

            batteryLevel -= (int)percentage;

            if (GetType().Name == "CargoVan")
            {
                batteryLevel -= 5;
            }
        }

        public void Recharge()
        => batteryLevel = 100;

        public override string ToString()
        {
            string condition;

            if (IsDamaged)
            {
               condition = "damaged";
            }
            else
            {
                condition = "OK";
            }

            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {condition}";
        }
    }
}
