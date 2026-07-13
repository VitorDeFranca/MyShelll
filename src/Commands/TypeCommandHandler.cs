using CodeCrafters.Shell.src.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    internal class TypeCommandHandler : ICommandHandler
    {
        public string CommandName => "type";

        public void Execute(string[] arguments)
        {
            if (arguments.Count() != 1)
            {
                Console.WriteLine($"{ArgumentParser.GetArgumentsString(arguments)} is not a valid argument for 'type'");
                return;
            }

            var word = arguments.First();
            if (CommandFactory.IsShellBuiltIn(word))
            {
                Console.WriteLine($"{word} is a shell builtin");
                return;
            }


            var filePath = OSEnvironmentHelpers.GetFullExecutableFilePathFromPathVariable(word);
            if (!string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine($"{word} is {filePath}");
                return;
            }

            Console.WriteLine($"{word}: not found");
        }
    }
}
