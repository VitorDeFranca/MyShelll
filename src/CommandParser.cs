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
            var isInsideDoubleQuotes = false;
            var specialCharactersEscapedInDoubleQuotes = new HashSet<char> { '\\', '\"' };

            for (int i = 0; i < userInput.Length; i++)
            {
                var c = userInput[i];
                // Detects if the character is a single quote
                if (c == '\'')
                {
                    // Append single quote if inside double quotes
                    if (isInsideDoubleQuotes)
                        sb.Append(c);
                    else
                        // Inverts the state, indicating whether we are inside or outside single quotes
                        isInsideSingleQuotes = !isInsideSingleQuotes;
                    continue;
                }
                else if (c == '\"')
                {
                    if (!isInsideSingleQuotes)
                        isInsideDoubleQuotes = !isInsideDoubleQuotes;
                    else
                    {
                        sb.Append(c);
                    }
                    continue;
                }
                else if (c == '\\')
                {
                    if (!isInsideSingleQuotes)
                    {
                        i++;
                        if (i < userInput.Length)
                        {
                            var nextChar = userInput[i];
                            if (!isInsideDoubleQuotes || specialCharactersEscapedInDoubleQuotes.Contains(nextChar))
                                c = nextChar;
                        }
                    }
                }
                else if (c == ' ' && !isInsideSingleQuotes && !isInsideDoubleQuotes)
                {
                    break;
                }
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
