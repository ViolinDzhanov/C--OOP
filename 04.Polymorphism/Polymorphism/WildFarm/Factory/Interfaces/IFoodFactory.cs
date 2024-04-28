using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factory.Interfaces
{
    public interface IFoodFactory
    {
        public IFood CreateFood(string type, int quantity);
    }
}
