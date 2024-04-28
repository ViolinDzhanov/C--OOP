using _3.Telephony.Core.Interfaces;
using _3.Telephony.IO.Interfaces;
using _3.Telephony.Models;
using _3.Telephony.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Telephony.Core
{
    public class Enginne : Iengine
    {
        Ireader reader;
        IWriter writer;

        public Enginne(Ireader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {
            string[] phoneNumbers = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] urls = reader.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ICallable callable;
            foreach (var phoneNumber in phoneNumbers)
            {
                if (phoneNumber.Length == 10)
                {
                    callable = new Smartphone();
                }
                else
                {
                    callable = new StationaryPhone();
                }
                try
                {
                   writer.WriteLine(callable.Call(phoneNumber));
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            IBrowsable browsable = new Smartphone();

            foreach (var url in urls)
            {
                try
                {
                   writer.WriteLine(browsable.Browse(url));
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);

                }
            }
        }
    }
}
