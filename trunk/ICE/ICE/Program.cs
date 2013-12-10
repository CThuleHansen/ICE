using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICE
{
    class Program
    {
        private static CommandReceiver _commandReceiver;

        private static void Main(string[] args)
        {
            _commandReceiver = new CommandReceiver();
            while (true)
            {
                var line = Console.ReadLine();
                if (line != null) 
                    _commandReceiver.NewCommand(line.Split(' '));
            }
        }
    }
}
