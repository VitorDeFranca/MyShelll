using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    public class CommandResult
    {
        public string Message { get; set; }
        public CommandResultType Type { get; set; }
        public bool Exit { get; set; } = false;

        public CommandResult()
        {
            Message = string.Empty;
            Type = CommandResultType.Success;
        }

        public CommandResult(bool exit)
        {
            Exit = exit;
        }

        public CommandResult(CommandResultType success, string message, bool? exit = false)
        {
            Type = success;
            Message = message;
            Exit = exit ?? false;
        }

    }

    public enum CommandResultType
    {
        Success,
        Error,
    }
}
