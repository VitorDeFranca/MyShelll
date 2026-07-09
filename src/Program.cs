using System.Diagnostics;
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
                    NotAShellBuiltInHandler(command, arguments);
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

        
        var filePath = GetFullExecutableFilePathFromPathVariable(word);
        if (!string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine($"{word} is {filePath}");
            return;
        }

        Console.WriteLine($"{word}: not found");
    } 

    private static bool IsWordShellBuiltIn(string word) => ShellBuiltIns.Contains(word);
        
    #endregion
    private static void EchoCommandHandler(IEnumerable<string> arguments)
    {
        Console.WriteLine(GetArgumentsString(arguments));
    }

    #region Not A Shell BuiltIn Methods
    private static void NotAShellBuiltInHandler(string command, IEnumerable<string> arguments)
    {
        var filePath = GetFullExecutableFilePathFromPathVariable(command);
        if (!string.IsNullOrEmpty(filePath))
        {
            Process.Start(command, arguments).WaitForExit();
            return;
        }

        NotAValidCommandHandler(command);
    }

    private static void NotAValidCommandHandler(string command)
    {
        Console.WriteLine($"{command}: command not found");
    }
    #endregion

    #region Helper Methods
    private static string GetArgumentsString(IEnumerable<string> arguments) 
    {
        var argumentsStringBuilder = new StringBuilder();
        
        argumentsStringBuilder.AppendJoin(" ", arguments);

        return argumentsStringBuilder.ToString();
    }

    public static string? GetFullExecutableFilePathFromPathVariable(string fileName) 
    {
        var rawPath = Environment.GetEnvironmentVariable("PATH");
        if (rawPath == null)
        {
            Console.WriteLine("PATH environment variable is not set");
            return null;
        }

        var directories = rawPath?.Split(Path.PathSeparator);
        string? filePath = null;

        foreach (var dir in directories)
        {
            var fullPath = Path.Combine(dir, fileName);
            if (File.Exists(fullPath) && IsAnExecutable(fullPath))
            {
                filePath = fullPath;
                break;
            }
        }

        return filePath;
    }

    public static bool IsAnExecutable(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            return false;

        // On Unix-like systems (Linux, macOS), check the execute bits
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return IsAnUnixExecutable(filePath);

        // On Windows, "executable" means the extension is in PATHEXT
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
            return IsAWindowsExecutable(filePath);

        return false;
    }

    private static bool IsAWindowsExecutable(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        string pathext = Environment.GetEnvironmentVariable("PATHEXT")
                         ?? ".COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE;.WSF;.WSH;.MSC";

        foreach (string exeExt in pathext.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
        {
            if (ext == exeExt.ToLowerInvariant())
                return true;
        }
        return false;
    }

    private static bool IsAnUnixExecutable(string filePath)
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
    #endregion
}
