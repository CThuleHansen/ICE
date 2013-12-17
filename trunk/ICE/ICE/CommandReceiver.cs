using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ICE.MasterReceivers;

namespace ICE
{
    public interface ICommandReceiver
    {
        void NewCommand(string[] args);
    }

    public class CommandReceiver : ICommandReceiver
    {
        Dictionary<string, IMasterCommandReceiver> _commandReceivers = new Dictionary<string, IMasterCommandReceiver>(); 
        
        public CommandReceiver(IList<IMasterCommandReceiver> masterCommandReceivers )
        {
            foreach (var masterCommandReceiver in masterCommandReceivers)
            {
                _commandReceivers.Add(masterCommandReceiver.ExecutingCommand, masterCommandReceiver);
            }
        }

    

        public void NewCommand(string[] args)
        {
            try
            {
                _commandReceivers[args[0]].NewCommand(args.Skip(1).Take(args.Length-1).ToArray());
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                Console.WriteLine("Error: Key {0} was not found.", args[0]);
            }
        }
    }
}
