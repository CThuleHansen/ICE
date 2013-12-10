using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ICE
{
    class DirCommandReceiver
    {
        private string _executingCommand = "dir";

        public string ExecutingCommand
        {
            get { return _executingCommand; }
            private set { _executingCommand = value; }
        }

        private const int ExecutingCommandLength = 3;

        public void NewCommand(string operation, string[] args)
        {
            //Find the characters before dir in operation
            //Remove dir
            var command = operation.Substring(0, operation.Length - ExecutingCommandLength);

        }
    }
}
