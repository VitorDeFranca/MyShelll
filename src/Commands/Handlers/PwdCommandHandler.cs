using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    internal class PwdCommandHandler : ICommandHandler
    {
        public string CommandName => "pwd";

        public CommandResult Execute(string[] arguments)
        {
            if (arguments.Any())
                return new CommandResult(CommandResultType.Error, $"{CommandName}: too many arguments");

            return new CommandResult(CommandResultType.Success, Directory.GetCurrentDirectory());
        }
    }
}
