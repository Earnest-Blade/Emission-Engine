using System;
using System.Collections.Generic;
using System.IO;

namespace Emission.IO
{
    public static class File
    {
        public static string ASSET_FILE => Path.Combine(Directory.GetCurrentDirectory(), "Assets/");
        public static string DATA_FILE => Path.Combine(Directory.GetCurrentDirectory(), "Data/");

        public static string CURRENT_DIRECTORY => Directory.GetCurrentDirectory();

        public static string ReadLine(string path, int line)
        {
            if (!Exists(path)) FileNotFound(path);
            LogReading(path);
            
            using (StreamReader reader = System.IO.File.OpenText(path))
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
            Bundle bundle = Instances.Data.Find(split[0]);
            return bundle.ReadFile(split[1]);
        }

        public static void SetCurrentDirectory(string path) => Directory.SetCurrentDirectory(path);
        public static bool Exists(string path) => System.IO.File.Exists(path);

        private static void FileNotFound(string path)
        {
            throw new EmissionException(Errors.EmissionIOException, $"'{path}': File Not Found!");
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
