using CodeCrafters.Shell.src.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeCrafters.Shell.src.Commands.Handlers
{
    internal class ExternalCommandHandler : ICommandHandler
    {
        public string CommandName { get; set; }

        public ExternalCommandHandler(string commandName)
        {
            CommandName = commandName;
        }

        public CommandResult Execute(string[] arguments)
        {
            var filePath = OSEnvironmentHelpers.GetFullExecutableFilePathFromPathVariable(CommandName);
            if (string.IsNullOrEmpty(filePath))
                return new CommandResult(CommandResultType.Error, $"{CommandName}: command not found");

            var startInfo = new ProcessStartInfo(CommandName)
            {
                UseShellExecute = false,
                // Standard output is captured so it can be redirected to a file when requested.
                RedirectStandardOutput = true,
                // Standard error is left attached to the terminal, it is never redirected by '>'.
                RedirectStandardError = false,
            };

            foreach (var argument in arguments)
                startInfo.ArgumentList.Add(argument);

            using var process = Process.Start(startInfo);
            if (process == null)
                return new CommandResult(CommandResultType.Error, $"{CommandName}: command not found");

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return new CommandResult(CommandResultType.Success, TrimTrailingNewLine(output));
        }

        private static string TrimTrailingNewLine(string output)
        {
            if (output.EndsWith("\r\n", StringComparison.Ordinal))
                return output[..^2];

            if (output.EndsWith("\n", StringComparison.Ordinal))
                return output[..^1];

            return output;
        }
    }
}
