using RobotService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> interfaceStandards;

        protected Robot(string model, int batteryCapacity, int convertionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            this.batteryLevel = batteryCapacity;

            this.convertionCapacityIndex = convertionCapacityIndex;
            this.interfaceStandards = new List<int>();
        }

        public string Model
        {
            get => model;
            private set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model cannot be null or empty.");
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Battery capacity cannot drop below zero.");
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel => batteryLevel;

        public int ConvertionCapacityIndex => convertionCapacityIndex;

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards.AsReadOnly();

        public void Eating(int minutes)
        {
            batteryLevel += ConvertionCapacityIndex * minutes;

            if (BatteryLevel > BatteryCapacity)
            {
                batteryLevel = BatteryCapacity;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
           if (BatteryLevel >= consumedEnergy)
            {
                batteryLevel -= consumedEnergy;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            batteryCapacity -= supplement.BatteryUsage;
            batteryLevel -= supplement.BatteryUsage;
        }
        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
           

            if (InterfaceStandards.Any())
            {
                sb.AppendLine($"--Supplements installed: {string.Join(" ", interfaceStandards)}");
            }
            else
            {
                sb.AppendLine("--Supplements installed: none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
