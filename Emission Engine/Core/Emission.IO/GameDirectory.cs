using System;
using System.Collections.Generic;
using System.IO;

namespace Emission.IO
{
    public static class GameDirectory
    {
        public static void SetCurrentDirectory(string? path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException();
            
            Environment.CurrentDirectory = Path.GetFullPath(path);
        }

        public static string GetCurrentDirectory() => Environment.CurrentDirectory;

        public static IEnumerable<string> EnumerateFiles(string path)
        {
            return System.IO.Directory.EnumerateFiles(path);
        }
    }
}
