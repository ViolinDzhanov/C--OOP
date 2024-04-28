using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = CommandPattern.Core.Contracts.ICommand;

namespace CommandPattern.Core.Comands
{
    public class HelloCommand : ICommand
    {
        public string Execute(string[] args)
         => $"Hello, {args[0]}";
    }
}
