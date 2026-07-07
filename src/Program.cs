class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("$ ");
            var userInput = Console.ReadLine();
            Console.WriteLine($"{userInput}: command not found");
        }
    }
}
