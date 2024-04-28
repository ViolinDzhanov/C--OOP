using Raiding.Core.Interfaces;
using Raiding.Factory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raiding.IO.Interfaces;
using Raiding.Models.Interfaces;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IFactory factory;

        private readonly ICollection<IHero> heroes;

        public Engine(IReader reader, IWriter writer, IFactory factory)
        {
            this.reader = reader;
            this.writer = writer;
            this.factory = factory;

            heroes = new List<IHero>();
        }

        public void Run()
        {
           int count = int.Parse(reader.ReadLine());

            while (count > 0)
            {
                string name = reader.ReadLine();
                string heroType = reader.ReadLine();

                try
                {
                    heroes.Add(factory.CreateHero(heroType, name));
                    count--;
                }
                catch (ArgumentException ex)
                {

                    writer.WriteLine(ex.Message);
                }
            }

            foreach (var hero in heroes)
            {
                writer.WriteLine(hero.CastAbility());
            }

            int bossPower = int.Parse(reader.ReadLine());

            if (bossPower <= heroes.Sum(p => p.Power))
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }
    }
}
