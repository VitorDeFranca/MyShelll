using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src
{
    public static class ArgumentParser
    {
        public static string[] GetArguments(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                return [];

            var whitespaceTreatedUserInput = ReplaceWhitespacesOutsideQuotes(userInput);

            var splitInput = whitespaceTreatedUserInput.Split(';');
            return [..splitInput.Skip(1)];
        }

        public static string ReplaceWhitespacesOutsideQuotes(string userInput)
        {
            if (string.IsNullOrEmpty(userInput)) return userInput;

            var result = new StringBuilder();
            var isInsideSingleQuotes = false;
            var isInsideDoubleQuotes = false;

            for (int i = 0; i < userInput.Length; i++)
            {
                var c = userInput[i];

                // Detects if the character is a single quote
                if (c == '\'')
                {
                    // Inverts the state, indicating whether we are inside or outside single quotes
                    isInsideSingleQuotes = !isInsideSingleQuotes;
                    if (isInsideDoubleQuotes) result.Append(c); // Append single quote if inside double quotes
                    continue;
                }
                else if (c == '\"')
                {
                    isInsideDoubleQuotes = !isInsideDoubleQuotes;
                    continue;
                }

                // If we are outside single quotes and the character is a whitespace
                if (!isInsideSingleQuotes && !isInsideDoubleQuotes && char.IsWhiteSpace(c))
                {
                    // Replace the whitespace with a semicolon
                    result.Append(';');

                    // Jumps to the next character, skipping any additional whitespaces
                    while (i + 1 < userInput.Length && char.IsWhiteSpace(userInput[i + 1]))
                    {
                        i++;
                    }
                }
                else
                {
                    // Any other character (including whitespaces inside single quotes) is appended
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public static string GetArgumentsString(IEnumerable<string> arguments)
        {
            var argumentsStringBuilder = new StringBuilder();

            argumentsStringBuilder.AppendJoin(" ", arguments);

            return argumentsStringBuilder.ToString();
        }
    }
}
