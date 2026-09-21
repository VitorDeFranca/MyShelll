using CodeCrafters.Shell.src.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src
{
    public static class RedirectionHandler
    {
        public static void Execute(string outputMessage, string redirectionFile)
        {
            var directory = Path.GetDirectoryName(redirectionFile);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.AppendAllText(
                redirectionFile,
                outputMessage + Environment.NewLine
            );
        }
    }
}
