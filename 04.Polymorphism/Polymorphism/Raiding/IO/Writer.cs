using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raiding.IO.Interfaces;

namespace Raiding.IO
{
    public class Writer : IWriter
    {
        public void WriteLine(string line)
            => Console.WriteLine(line);
    }
}
