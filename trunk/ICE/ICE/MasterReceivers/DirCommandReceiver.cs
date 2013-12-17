using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ICE.Data;
using ICE.RavenDB;

namespace ICE.MasterReceivers
{
    public class DirCommandReceiver : IMasterCommandReceiver
    {
        private IRavenDBConnector _ravenDBConnector;
        private List<FolderShortcut> _folderShortcuts = new List<FolderShortcut>();
        private const string _executingCommand = "dir";

        public string ExecutingCommand
        {
            get { return _executingCommand; }
        }

        public DirCommandReceiver(IRavenDBConnector ravenDBConnector)
        {
            _ravenDBConnector = ravenDBConnector;
            LoadDirShortcuts();
        }

        private void LoadDirShortcuts()
        {
            using (var session = _ravenDBConnector.Store.OpenSession())
            {
                _folderShortcuts = session.Query<FolderShortcut>().ToList();
            }
        }
        public void NewCommand(string[] args)
        {
            //Find the characters before dir in operation
            //Remove dir
            if (args.Length > 0)
            {
                var command = args[0];
                switch (command)
                {
                    case "set":
                        SetCommand(args.Skip(1).Take(args.Length - 1).ToArray());
                        break;
                    case "open":
                        OpenCommand(args.Skip(1).Take(args.Length-1).ToArray());
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }

            }
        }

        private void OpenCommand(string[] args)
        {
            if (args.Length == 1)
            {
                var folderShortcut =
                    _folderShortcuts.SingleOrDefault(x => x.Shortcut.Equals(args[0], StringComparison.OrdinalIgnoreCase));
                if (folderShortcut != null)
                {
                    Process.Start(folderShortcut.DirectoryPath);
                }
                
            }


        }

        private void SetCommand(string[] args)
        {
            if (args.Length.Equals(2))
            {
                var folderShortcut =
                    _folderShortcuts.SingleOrDefault(x => x.Shortcut.Equals(args[0], StringComparison.OrdinalIgnoreCase));
                if (folderShortcut != null)
                {
                    if (folderShortcut.DirectoryPath.Equals(args[1], StringComparison.OrdinalIgnoreCase) == false)
                    {
                        Console.WriteLine("Updating existing shortcut...");
                        folderShortcut.DirectoryPath = args[1];
                        UpdateFolderShortcut(folderShortcut);
                    }
                }
                else
                {
                    folderShortcut = new FolderShortcut() { Shortcut = args[0], DirectoryPath = args[1] };
                    _folderShortcuts.Add(folderShortcut);
                    SaveFolderShortcut(folderShortcut);
                }

            }
        }

        private void UpdateFolderShortcut(FolderShortcut folderShortcut)
        {
            string oldDirectoryPath;
            using (var session = _ravenDBConnector.Store.OpenSession())
            {
                var folderShortcutLoaded = session.Load<FolderShortcut>(folderShortcut.Id);
                oldDirectoryPath = folderShortcutLoaded.DirectoryPath;
                folderShortcutLoaded.DirectoryPath = folderShortcut.DirectoryPath;
                session.SaveChanges();
            }
            Console.WriteLine(string.Format("The existing shortcut {0} has been updated. The directory path changed from {1} to {2}", folderShortcut.Shortcut, oldDirectoryPath, folderShortcut.DirectoryPath));

        }

        private void SaveFolderShortcut(FolderShortcut folderShortcut)
        {
            using (var session = _ravenDBConnector.Store.OpenSession())
            {
                session.Store(folderShortcut);
                session.SaveChanges();
            }
            Console.WriteLine(string.Format("The shortcut {0} has been added with the directory path: {1}", folderShortcut.Shortcut, folderShortcut.DirectoryPath));
        }
    }
}
