using _3.Telephony.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Telephony.IO
{
    public class ConsoleReader : Ireader
    {
        public string ReadLine() => Console.ReadLine();
       
    }
}
