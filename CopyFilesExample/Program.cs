using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileCopy = new FileCopy();
            fileCopy.Nested = true;
            fileCopy.Execute("D:\\VS Projects\\Trash projects\\SandboxConsole\\CopyFilesExample\\Test Folder From",
                "D:\\VS Projects\\Trash projects\\SandboxConsole\\CopyFilesExample\\Test Folder To");
        }
    }
    public class FileCopy
    {
        public bool Nested = false;
        public void Execute(string pathFrom, string pathTo)
        {
            if (!Directory.Exists(pathFrom) || !Directory.Exists(pathTo))
            {
                Console.WriteLine("This directory is not exists");
            }
            var files = Directory.GetFiles(pathFrom);
            foreach (var file in files)
            {
                var fileTo = pathTo + file.Substring(pathFrom.Length);
                File.Copy(file, fileTo);
            }
            if (Nested)
            {
                var pathToContinue = Directory.GetDirectories(pathFrom);
                foreach(var path in  pathToContinue)
                {
                    pathTo += path.Substring(pathFrom.Length);
                    Directory.CreateDirectory(pathTo);
                    Execute(path, pathTo);
                }
            }
        }
    }
}
