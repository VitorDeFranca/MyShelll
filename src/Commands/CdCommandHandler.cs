using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    internal class CdCommandHandler : ICommandHandler
    {
        public string CommandName => "cd";

        public void Execute(string[] arguments)
        {
            if (arguments.Count() != 1)
            {
                Console.WriteLine("cd: Invalid amount of arguments");
                return;
            }

            var path = arguments.First();

            if (string.Equals(path, "~"))
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            try
            {
                Directory.SetCurrentDirectory(path);
            }
            catch (Exception ex) when
                (ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                Console.WriteLine($"cd: {path}: No such file or directory");
            }
            return;
        }
    }
}
