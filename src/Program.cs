class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("$ ");
            var userInput = Console.ReadLine();
            if (string.Equals(userInput, "exit")) 
                break;
            Console.WriteLine($"{userInput}: command not found");
            
        }
    }
}
