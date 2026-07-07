using System.Diagnostics.Tracing;
using System.Text;

class Program
{
    private static IEnumerable<string> ShellBuiltIns = new List<string> { "echo", "exit", "type" };
    static void Main()
    {
        
        while (true)
        {
            Console.Write("$ ");

            var userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                continue;

            var command = userInput.Split(' ')[0];
            var arguments = userInput.Split(' ').Skip(1);

            if (string.Equals(command, "exit")) break;

            switch (command)
            {
                case "echo":
                    EchoCommandHandler(arguments);
                    break;

                case "type":
                    TypeCommandHandler(arguments);
                    break;

                default:
                    NotAValidCommandHandler(command);
                    break;
            };
        }
    }

    private static void TypeCommandHandler(IEnumerable<string> arguments)
    {
        if (arguments.Count() != 1)
        {
            Console.WriteLine($"{GetArgumentsString(arguments)} is not a valid argument for 'type'");
            return;
        }

        var word = arguments.First();
        if (ShellBuiltIns.Contains(word))
        {
            Console.WriteLine($"{word} is a shell builtin");
            return;
        }

        Console.WriteLine($"{word}: not found");
    }

    private static void NotAValidCommandHandler(string command)
    {
        Console.WriteLine($"{command}: command not found");
    }

    private static void EchoCommandHandler(IEnumerable<string> arguments)
    {
        Console.WriteLine(GetArgumentsString(arguments));
    }

    private static string GetArgumentsString(IEnumerable<string> arguments) 
    {
        var argumentsStringBuilder = new StringBuilder();
        
        argumentsStringBuilder.AppendJoin(" ", arguments);

        return argumentsStringBuilder.ToString();
    }
}
