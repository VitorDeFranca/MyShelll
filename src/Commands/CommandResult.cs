using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    public class CommandResult
    {
        public string Message { get; set; }
        public bool Exit { get; set; } = false;

        public CommandResult()
        {
            Message = string.Empty;
        }

        public CommandResult(bool exit)
        {
            Exit = exit;
        }

        public CommandResult(string message, bool? exit = false)
        {
            Message = message;
            Exit = exit ?? false;
        }

    }
}
