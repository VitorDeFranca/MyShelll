using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    internal class CdCommandHandler : ICommandHandler
    {
        public string CommandName => "cd";

        public CommandResult Execute(string[] arguments)
        {
            if (arguments.Count() != 1)
            {
                return new CommandResult(CommandResultType.Error, "cd: Invalid amount of arguments.");
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
                return new CommandResult(CommandResultType.Error, $"cd: {path}: No such file or directory");
            }
            catch (Exception ex) when
                (ex is ArgumentException)
            {
                return new CommandResult(CommandResultType.Error, $"cd: path cannot be empty");
            }

            return new CommandResult();
        }
    }
}
