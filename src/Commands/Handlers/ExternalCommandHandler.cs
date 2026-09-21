using CodeCrafters.Shell.src.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    internal class ExternalCommandHandler : ICommandHandler
    {
        public string CommandName { get; set; }

        public ExternalCommandHandler(string commandName)
        {
            CommandName = commandName;
        }

        public CommandResult Execute(string[] arguments)
        {

            var filePath = OSEnvironmentHelpers.GetFullExecutableFilePathFromPathVariable(CommandName);
            if (!string.IsNullOrEmpty(filePath))
            {
                Process.Start(CommandName, arguments).WaitForExit();
                return new CommandResult(CommandResultType.Success, string.Empty);
            }

            return new CommandResult(CommandResultType.Success, $"{CommandName}: command not found");
        }
    }
}
