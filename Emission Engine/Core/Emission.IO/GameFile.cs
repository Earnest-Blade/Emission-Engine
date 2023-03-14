using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.VisualBasic.FileIO;

namespace Emission.IO
{
    public static class GameFile
    {
        public static string ASSET_FILE => Path.Combine(GameDirectory.GetCurrentDirectory(), "Assets/");
        public static string DATA_FILE => Path.Combine(GameDirectory.GetCurrentDirectory(), "Data/");

        internal const int DEFAULT_BUFFER_SIZE = 4096;

        public static string ReadLine(string path, int line)
        {
            if (!Exists(path)) FileNotFound(path);
            LogReading(path);
            
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
        
        public static IEnumerable<string> ReadLines(string path)
        {
            if (!Exists(path)) FileNotFound(path);
            LogReading(path);

            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.ReadLine() is { } line) 
                    yield return line;
            }
        }

        public static string ReadAllText(string path)
        {
            if(!Exists(path)) FileNotFound(path);
            LogReading(path);

            using (StreamReader reader = new StreamReader(path, detectEncodingFromByteOrderMarks:true))
                return reader.ReadToEnd();
        }

        public static byte[] ReadAllBytes(string path)
        {
            if(!Exists(path)) FileNotFound(path);
            LogReading(path);
            
            return System.IO.File.ReadAllBytes(path);
        }

        public static string ReadAllData(string path)
        {
            string[] split = path.Split('/');
            Bundle bundle = GameInstance.Data.Find(split[0]);
            return bundle.ReadFile(split[1]);
        }

        public static FileStream Create(string path) => Create(path, DEFAULT_BUFFER_SIZE);
        public static FileStream Create(string path, int bufferSize)
            => new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, bufferSize);

        public static void Delete(string path)
        {
            if (path == null) throw new EmissionException(EmissionErrors.EmissionIOException, $"{nameof(path)} is null");
            FileSystem.DeleteFile(Path.GetFullPath(path));
        }

        public static FileStream Read(string path) => Read(path, DEFAULT_BUFFER_SIZE);
        public static FileStream Read(string path, int bufferSize)
            => new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);

        public static StreamReader OpenText(string path)
        {
            if (path == null) throw new EmissionException(EmissionErrors.EmissionIOException, $"{nameof(path)} is null");
            return new StreamReader(path);
        }

        public static StreamWriter CreateText(string path)
        {
            if (path == null) throw new EmissionException(EmissionErrors.EmissionIOException, $"{nameof(path)} is null");
            return new StreamWriter(path, append:false);
        }
        
        public static StreamWriter AppendText(string path)
        {
            if (path == null) throw new EmissionException(EmissionErrors.EmissionIOException, $"{nameof(path)} is null");
            return new StreamWriter(path, append:true);
        }

        public static bool Exists(string? path)
        {
            try
            {
                if (path == null) return false;
                if (path.Length == 0) return false;

                path = Path.GetFullPath(path);

                if (path.Length > 0 && IsDirectorySeparator(path[path.Length - 1]))
                    return false;
                
                return FileSystem.FileExists(path);
            }
            catch(ArgumentException){}
            catch(IOException){}
            catch(UnauthorizedAccessException){}

            return false;
        }
        
        public static string ExtractDirectory(string path) => path.Substring(0, path.LastIndexOf('/'));

        public static bool IsDirectorySeparator(char c) =>
            c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;

        private static void FileNotFound(string path)
        {
            throw new EmissionException(EmissionErrors.EmissionIOException, $"'{path}': File Not Found!");
        }

        private static void LogReading(string path)
        {
            Debug.Log($"[IO] Reading file '{path}'");
        }

        internal static bool Exists(object value)
        {
            throw new NotImplementedException();
        }

    }
}
