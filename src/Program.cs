using CodeCrafters.Shell.src;
using CodeCrafters.Shell.src.Arguments;
using CodeCrafters.Shell.src.Commands;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        
        while (true)
        {
            Console.Write("$ ");

            var userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                continue;

            var command = CommandParser.GetCommandName(userInput);
            var argumentsResult = ArgumentParser.GetArguments(userInput, command);


            var handler = CommandFactory.GetHandler(command);
            var commandResult = handler.Execute(argumentsResult.ArgumentList);

            var hasRedirection = argumentsResult.HasRedirection && !string.IsNullOrEmpty(argumentsResult.RedirectionFile);

            if (hasRedirection)
            {
                // The file is created even when the command fails, only stdout is written to it.
                var redirectedOutput = commandResult.Type == CommandResultType.Success ? commandResult.Message : string.Empty;
                RedirectionHandler.Execute(redirectedOutput, argumentsResult.RedirectionFile!);
            }

            if ((!hasRedirection || commandResult.Type == CommandResultType.Error) && !string.IsNullOrEmpty(commandResult.Message))
                Console.WriteLine(commandResult.Message);



            if (commandResult.Exit) break;
        }
    }
}
