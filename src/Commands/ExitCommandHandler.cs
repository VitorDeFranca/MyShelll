using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    public class ExitCommandHandler : ICommandHandler
    {
        public string CommandName => "exit";

        public CommandResult Execute(string[] arguments)
        {
            return new CommandResult(true);
        }
    }
}
