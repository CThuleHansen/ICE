using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICE
{
    public interface IConsoleReceiver
    {
        void Run();
    }

    public class ConsoleReceiver : IConsoleReceiver
    {
        private ICommandReceiver _commandReceiver;

        public ConsoleReceiver(ICommandReceiver commandReceiver)
        {
            _commandReceiver = commandReceiver;
        }

        public void Run()
        {
            Console.WriteLine("The program is initialized");
            while (true)
            {
                Console.Write(">");
                var line = Console.ReadLine();
                if (line != null)
                    _commandReceiver.NewCommand(line.Split(' '));
            }
        }
    }
}
