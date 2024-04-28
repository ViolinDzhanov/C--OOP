using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Telephony.Models.Interfaces
{
    public interface ICallable
    {
        string Call(string phoneNumber);
    }
}
