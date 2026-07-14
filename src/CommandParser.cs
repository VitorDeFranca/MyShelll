using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src
{
    public static class CommandParser
    {
        public static string GetCommandName(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                return string.Empty;

            var sb = new StringBuilder();
            var isInsideSingleQuotes = false;
            for (int i = 0; i < userInput.Length; i++)
            {
                var c = userInput[i];
                if (c == '\'')
                {
                    isInsideSingleQuotes = !isInsideSingleQuotes;
                    continue;
                }
                else if (c == ' ' && !isInsideSingleQuotes)
                {
                    break;
                }
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
