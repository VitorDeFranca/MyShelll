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
            File.AppendAllText(
                redirectionFile,
                outputMessage + Environment.NewLine
            );
        }
    }
}
