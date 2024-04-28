using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Factory.Interfaces
{
    public interface IFactory
    {
        IHero CreateHero(string type, string name);
    }
}
