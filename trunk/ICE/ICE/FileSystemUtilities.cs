using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICE
{
    public class FileSystemUtilities
    {
        public static string DirSearch(string rootFolder, string folderName)
        {
            bool dirFound = false;
            foreach (var fileSystemEntry in Directory.GetFileSystemEntries(rootFolder))
            {
                if (fileSystemEntry.Contains("Dropbox")) 
                    Console.WriteLine("woop woop");
                var fileAttributes = File.GetAttributes(fileSystemEntry);
                bool isHidden = (fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isDir = (fileAttributes & FileAttributes.Directory) == FileAttributes.Directory;

                if (isDir && !isHidden)
                {
                   File.AppendAllText(@"test.txt", fileSystemEntry);
                    if (Path.GetDirectoryName(fileSystemEntry).Equals(folderName, StringComparison.InvariantCultureIgnoreCase))
                        return fileSystemEntry;
                    else
                    {
                        var dirReturn = DirSearch(fileSystemEntry, folderName);
                        if (dirReturn != null)
                        {
                            File.WriteAllText(@"test.txt", "\t" + dirReturn);
                            Console.WriteLine("\t" + dirReturn);
                            return dirReturn;
                        }
                    }
                    File.AppendAllText(@"test.txt", "\n");
                }
            }

            return null;
        }
    }
}
