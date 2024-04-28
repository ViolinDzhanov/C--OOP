using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factory.Interfaces
{
    public interface IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] tokens);
    }
}
