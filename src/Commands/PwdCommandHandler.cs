using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    internal class PwdCommandHandler : ICommandHandler
    {
        public string CommandName => "pwd";

        public CommandResult Execute(string[] arguments)
        {
            if (arguments.Any())
                return new CommandResult($"{CommandName}: too many arguments");

            return new CommandResult(Directory.GetCurrentDirectory());
        }
    }
}
