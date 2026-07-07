class Program
{
    static void Main()
    {
        Console.Write("$ ");
        var userInput = Console.ReadLine();
        Console.WriteLine($"{userInput}: command not found");
    }
}
