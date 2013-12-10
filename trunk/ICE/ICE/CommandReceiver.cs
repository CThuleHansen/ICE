using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ICE.MasterReceivers;

namespace ICE
{
    public class CommandReceiver
    {
        Dictionary<string, IMasterCommandReceiver> _commandReceivers = new Dictionary<string, IMasterCommandReceiver>(); 
        
        public CommandReceiver()
        { LoadReceivers();}

        private void LoadReceivers()
        {
            IMasterCommandReceiver dirCommandReceiver = new DirCommandReceiver();
            _commandReceivers.Add(dirCommandReceiver.ExecutingCommand, dirCommandReceiver);
        }

        public void NewCommand(string[] args)
        {
            try
            {
                _commandReceivers[args[0]].NewCommand(args);
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                Console.WriteLine("Error: Key {0} was not found.", args[0]);
            }
        }
    }
}
