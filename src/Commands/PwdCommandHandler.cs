using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    internal class PwdCommandHandler : ICommandHandler
    {
        public string CommandName => "pwd";

        public void Execute(string[] arguments)
        {
            if (arguments.Any())
            {
                Console.WriteLine($"pwd: too many arguments");
                return;
            }
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}
