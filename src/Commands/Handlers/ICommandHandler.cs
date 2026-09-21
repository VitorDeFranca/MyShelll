using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    public interface ICommandHandler
    {
        string CommandName { get; }
        CommandResult Execute(string[] arguments);
    }
}
