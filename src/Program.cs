using System.Runtime.InteropServices;
using System.Text;

class Program
{
    #region Shell Variables
    private static IEnumerable<string> ShellBuiltIns = new List<string> { "echo", "exit", "type" };
    #endregion

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

    #region Type Command Methods
    private static void TypeCommandHandler(IEnumerable<string> arguments)
    {
        if (arguments.Count() != 1)
        {
            Console.WriteLine($"{GetArgumentsString(arguments)} is not a valid argument for 'type'");
            return;
        }

        var word = arguments.First();
        if (IsWordShellBuiltIn(word))
        {
            Console.WriteLine($"{word} is a shell builtin");
            return;
        }

        var rawPath = Environment.GetEnvironmentVariable("PATH");
        var directories = rawPath?.Split(Path.PathSeparator);
        var filePath = string.Empty;

        foreach (var dir in directories)
        {
            var fullPath = Path.Combine(dir, word);
            if (File.Exists(fullPath) && HasExecutePermission(fullPath))
            {
                filePath = fullPath;
                break;
            }
        }

        if (!string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine($"{word} is {filePath}");
            return;
        }


        Console.WriteLine($"{word}: not found");
    }
    

    private static bool IsWordShellBuiltIn(string word) => ShellBuiltIns.Contains(word);
        
    #endregion

    private static void NotAValidCommandHandler(string command)
    {
        Console.WriteLine($"{command}: command not found");
    }

    private static void EchoCommandHandler(IEnumerable<string> arguments)
    {
        Console.WriteLine(GetArgumentsString(arguments));
    }

    #region Helper Methods
    private static string GetArgumentsString(IEnumerable<string> arguments) 
    {
        var argumentsStringBuilder = new StringBuilder();
        
        argumentsStringBuilder.AppendJoin(" ", arguments);

        return argumentsStringBuilder.ToString();
    }

    public static bool HasExecutePermission(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            return false;

        // On Unix-like systems (Linux, macOS), check the execute bits
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            try
            {
                var mode = File.GetUnixFileMode(filePath);
                // Check if any execute bit is set (user, group, or other)
                return (mode & UnixFileMode.UserExecute) != 0 ||
                       (mode & UnixFileMode.GroupExecute) != 0 ||
                       (mode & UnixFileMode.OtherExecute) != 0;
            }
            catch
            {
                return false; // Permission denied or file inaccessible
            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // On Windows, "executable" means the extension is in PATHEXT
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            string pathext = Environment.GetEnvironmentVariable("PATHEXT")
                             ?? ".COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE;.WSF;.WSH;.MSC";

            foreach (string exeExt in pathext.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                if (ext == exeExt.ToLowerInvariant())
                    return true;
            }
            return false;
        }

        return false;
    }
    #endregion
}
