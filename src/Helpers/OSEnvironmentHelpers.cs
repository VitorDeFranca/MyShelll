using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeCrafters.Shell.src.Helpers
{
    public static class OSEnvironmentHelpers
    {
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

        public static bool IsAWindowsExecutable(string filePath)
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

        public static bool IsAnUnixExecutable(string filePath)
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
    }
}
