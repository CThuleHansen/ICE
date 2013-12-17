using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICE.Data;
using ICE.RavenDB;
using ICE.Windsor;

namespace ICE
{
    class Program
    {
        private static WindsorSetup _windsorSetup;

        private static void Main(string[] args)
        {
            _windsorSetup = new WindsorSetup();
            var consoleReceiver = _windsorSetup.Container.Resolve<IConsoleReceiver>();
            consoleReceiver.Run();
            //_commandReceiver = new CommandReceiver();
            //while (true)
            //{
            //    var line = Console.ReadLine();
            //    if (line != null) 
            //        _commandReceiver.NewCommand(line.Split(' '));
            //}
        }
    }
}
