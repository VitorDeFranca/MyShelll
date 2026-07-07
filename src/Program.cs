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
            var arguments = userInput.Split(' ').Skip(1).ToList();

            if (string.Equals(command, "exit")) break;

            switch (command)
            {
                case "echo":
                    EchoCommandHandler(arguments);
                    break;
                default:
                    NotAValidCommandHandler(command);
                    break;
            };
        }
    }

    private static void NotAValidCommandHandler(string command)
    {
        Console.WriteLine($"{command}: command not found");
    }

    private static void EchoCommandHandler(List<string> arguments)
    {
        Console.WriteLine(String.Join(" ", arguments));
    }
}
