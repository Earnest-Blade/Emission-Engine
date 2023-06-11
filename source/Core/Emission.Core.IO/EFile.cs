using Microsoft.VisualBasic.FileIO;

namespace Emission.Core.IO
{
    public static class EFile
    {
        public const int DEFAULT_BUFFER_SIZE = 4096;

        private const string DEBUG_TEMPLATE = "[INFO] Reading '{0}'";

        public static string ReadLine(string? path, int line)
        {
            if (!Exists(path)) 
                throw new FileNotFoundException("Emission File Not Found", path);
            Debug.Log(string.Format(DEBUG_TEMPLATE, path));
            
            using (StreamReader reader = OpenText(path))
            {
                int l = 0;
                while (reader.ReadLine() is { } str)
                {
                    if (l == line)
                    {
                        reader.Close();
                        return str;
                    }
                    l++;
                }
            }
            return null;
        }
        
        public static IEnumerable<string> ReadLines(string? path)
        {
            if (!Exists(path)) 
                throw new FileNotFoundException("Emission File Not Found", path);
            Debug.Log(string.Format(DEBUG_TEMPLATE, path));
            
            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.ReadLine() is { } line) 
                    yield return line;
            }
        }

        public static string ReadAllText(string? path)
        {
            if(!Exists(path)) 
                throw new FileNotFoundException("Emission File Not Found", path);
            Debug.Log(string.Format(DEBUG_TEMPLATE, path));

            using (StreamReader reader = new StreamReader(path, detectEncodingFromByteOrderMarks:true))
                return reader.ReadToEnd();
        }

        public static byte[] ReadAllBytes(string? path)
        {
            if(!Exists(path)) throw new FileNotFoundException("Emission File Not Found", path);
            Debug.Log(string.Format(DEBUG_TEMPLATE, path));

            return System.IO.File.ReadAllBytes(path);
        }

        public static FileStream Create(string path) => Create(path, DEFAULT_BUFFER_SIZE);
        public static FileStream Create(string path, int bufferSize)
            => new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, bufferSize);

        public static void Delete(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            FileSystem.DeleteFile(Path.GetFullPath(path));
        }

        public static FileStream Read(string path) => Read(path, DEFAULT_BUFFER_SIZE);
        public static FileStream Read(string path, int bufferSize)
            => new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);

        public static StreamReader OpenText(string? path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            return new StreamReader(path);
        }

        public static StreamWriter CreateText(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            return new StreamWriter(path, append:false);
        }
        
        public static StreamWriter AppendText(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            return new StreamWriter(path, append:true);
        }

        public static bool Exists(string? path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) throw new ArgumentException("'path' cannot be null or empty.", nameof(path));

                path = Path.GetFullPath(path);

                if (path.Length > 0 && IsDirectorySeparator(path[path.Length - 1]))
                    return false;

                return FileSystem.FileExists(path);
            }
            catch (Exception exception) when (exception is ArgumentException || exception is IOException || exception is UnauthorizedAccessException)
            {
                throw new EmissionException(EmissionException.ERR_IO, exception);
            }
        }
        
        public static string ExtractDirectory(string path) => path.Substring(0, path.LastIndexOf('/'));

        public static bool IsDirectorySeparator(char c) =>
            c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
    }
}
