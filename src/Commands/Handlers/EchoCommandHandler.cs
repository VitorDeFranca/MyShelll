using CodeCrafters.Shell.src.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    internal class EchoCommandHandler : ICommandHandler
    {
        public string CommandName => "echo";

        public CommandResult Execute(string[] arguments)
        {
            return new CommandResult(CommandResultType.Success, ArgumentParser.GetArgumentsString(arguments));
        }
    }
}
