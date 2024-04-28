using WildFarm.IO;
using WildFarm.Core;
using WildFarm.Core.Interfaces;
using WildFarm.Factory.Interfaces;
using WildFarm.IO.Interfaces;
using WildFarm.Factory;

IReader reader = new Reader();
IWriter writer = new Writer();
IFoodFactory foodFactory = new FoodFactory();
IAnimalFactory animalFactory = new AnimalFactory();

IEngine engine = new Engine(reader, writer, foodFactory, animalFactory);

engine.Run();