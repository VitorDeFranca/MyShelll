using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    internal class EchoCommandHandler : ICommandHandler
    {
        public string CommandName => "echo";

        public void Execute(string[] arguments)
        {
            Console.WriteLine(ArgumentParser.GetArgumentsString(arguments));
        }
    }
}
