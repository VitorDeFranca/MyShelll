using CodeCrafters.Shell.src;
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

            var command = userInput.Split(' ')[0];
            var arguments = ArgumentParser.GetArguments(userInput);


            var handler = CommandFactory.GetHandler(command);
            var commandResult = handler.Execute(arguments);

            if (!string.IsNullOrEmpty(commandResult.Message))
                Console.WriteLine(commandResult.Message);

            if (commandResult.Exit) break;
        }
    }
}
