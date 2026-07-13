using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    public interface ICommandHandler
    {
        string CommandName { get; }
        void Execute(string[] arguments);
    }
}
