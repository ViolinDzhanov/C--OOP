using Vehicles.Factory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Core.Interfaces;
using Vehicles.IO.Interfaces;
using Vehicles.Models.Interfaces;
using Vehicles.Models;
using System.Runtime.CompilerServices;

namespace Vehicles.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IFactory factory;

        private readonly ICollection<Ivehicle> vehicles;

        public Engine(IReader reader, IWriter writer, IFactory factory)
        {
            this.reader = reader;
            this.writer = writer;
            this.factory = factory;

            vehicles = new List<Ivehicle>();
        }

        public void Run()
        {
            vehicles.Add(CreateVehicle());
            vehicles.Add(CreateVehicle());
            vehicles.Add(CreateVehicle());

            int numberOfCommands = int.Parse(reader.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                try
                {
                    ProcessCommand();
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
                catch (Exception)
                {

                    throw;
                }
            }


            foreach (var vehicle in vehicles)
            {
                writer.WriteLine(vehicle.ToString());
            }
        }

        private void ProcessCommand()
        {
            string[] tokens = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string comand = tokens[0];
            string vehicleType = tokens[1];

            Ivehicle vehicle = vehicles.FirstOrDefault(v => v.GetType().Name == vehicleType);

            if (vehicle == null)
            {
                throw new ArgumentException("Invalid vehicle type!");
            }
            else if (comand == "Drive")
            {
                double distance = double.Parse(tokens[2]);

                writer.WriteLine(vehicle.Drive(distance));
            }
            else if (comand == "DriveEmpty")
            {
                double distance = double.Parse(tokens[2]);

                writer.WriteLine(vehicle.Drive(distance, false));
            }
            else if (comand == "Refuel")
            {
                double litres = double.Parse(tokens[2]);

                vehicle.Refuel(litres);
            }
        }

        private Ivehicle CreateVehicle()
        {
            string[] tokens = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return factory.Create(tokens[0],
                double.Parse(tokens[1]),
                double.Parse(tokens[2]),
                double.Parse(tokens[3]));
        }
    }
}
