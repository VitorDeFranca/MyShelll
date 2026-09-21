using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    public class ExitCommandHandler : ICommandHandler
    {
        public string CommandName => "exit";

        public CommandResult Execute(string[] arguments)
        {
            return new CommandResult(CommandResultType.Success, string.Empty, exit: true);
        }
    }
}
