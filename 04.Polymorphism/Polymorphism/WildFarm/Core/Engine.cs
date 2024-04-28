using WildFarm.Factory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.IO.Interfaces;
using WildFarm.Core.Interfaces;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IFoodFactory foodFactory;
        private readonly IAnimalFactory animalFactory;

        private readonly ICollection<IAnimal> animals;

        public Engine(IReader reader,
            IWriter writer, 
            IFoodFactory foodFactory,
            IAnimalFactory animalFactory)
        {
            this.reader = reader;
            this.writer = writer;
       
            this.foodFactory = foodFactory;
            this.animalFactory = animalFactory;

            animals = new List<IAnimal>();
        }

        public void Run()
        {
            string comand;

            while((comand = reader.ReadLine()) != "End")
            {
                IAnimal animal = null;

                try
                {
                    animal = CreateAnimal(comand);

                    IFood food = CreateFood();

                    writer.WriteLine(animal.ProduceSound());

                    animal.Eat(food);
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
                animals.Add(animal);
            }

            foreach (IAnimal animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
        private IAnimal CreateAnimal(string input)
        {
            string[] animalTokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IAnimal animal = animalFactory.CreateAnimal(animalTokens);

            return animal;
        }
        private IFood CreateFood()
        {
            string[] foodTokens = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string type = foodTokens[0];
            int quantity = int.Parse(foodTokens[1]);

            IFood food = foodFactory.CreateFood(type, quantity);
            return food;
        }
    }
    
}
