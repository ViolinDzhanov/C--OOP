using BorderControl.Core.Interfaces;
using BorderControl.IO.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl.Core
{
    public class Engine : IEngine
    {
        IReader reader;
        IWriter writer;
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {
            List<IBuyer> comunity = new();

            int count = int.Parse(reader.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string[] input = reader.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (input.Length == 4)
                {
                    IBuyer citizen = new Citizen(input[0], int.Parse(input[1]), input[2], input[3]);

                    comunity.Add(citizen);
                }
                else
                {
                    IBuyer rebel = new Rebel(input[0], int.Parse(input[1]), input[2]);

                    comunity.Add(rebel);
                }
            }

            string name;

            while ((name = reader.ReadLine()) != "End")
            {
                var element = comunity.FirstOrDefault(n => n.Name == name);
                if (element != null)
                {
                    element.BuyFood();
                }
            }

            Console.WriteLine(comunity.Sum(c => c.Food));
        }
    }
}
