using CodeCrafters.Shell.src.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    internal class TypeCommandHandler : ICommandHandler
    {
        public string CommandName => "type";

        public CommandResult Execute(string[] arguments)
        {
            if (arguments.Count() != 1)
            {
                return new CommandResult($"{ArgumentParser.GetArgumentsString(arguments)} is not a valid argument for 'type'");
            }

            var word = arguments.First();
            if (CommandFactory.IsShellBuiltIn(word))
            {
                return new CommandResult ($"{word} is a shell builtin");
            }


            var filePath = OSEnvironmentHelpers.GetFullExecutableFilePathFromPathVariable(word);
            if (!string.IsNullOrEmpty(filePath))
            {
                return new CommandResult ($"{word} is {filePath}");
            }

            return new CommandResult($"{word}: not found");
        }
    }
}
