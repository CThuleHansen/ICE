using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ICE.MasterReceivers
{
    public class DirCommandReceiver : IMasterCommandReceiver
    {
        private const string _executingCommand = "dir";

        private const int ExecutingCommandLength = 3;

        public string ExecutingCommand
        {
            get { return _executingCommand; }
        }

        public void NewCommand(string[] args)
        {
            //Find the characters before dir in operation
            //Remove dir
            var command = args[0].Substring(ExecutingCommandLength, args[0].Length - ExecutingCommandLength);

            var directory = FileSystemUtilities.DirSearch(@"C:\", "Dropbox");
        }
    }
}
