using System;
using System.IO;

namespace Emission.IO
{
    public class File
    {
        public static string ASSET_FILE = Path.Combine(Directory.GetCurrentDirectory(), "Assets/");
        public static string DATA_FILE = Path.Combine(Directory.GetCurrentDirectory(), "Data/");

        public static string ReadLine(string path, int line)
        {
            if (!Exists(path)) FileNotFound(path);
            
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
        
        public static string[] ReadLines(string path)
        {
            if (!Exists(path)) FileNotFound(path);
            return System.IO.File.ReadAllLines(path);
        }

        public static string ReadAllText(string path)
        {
            if(!Exists(path)) FileNotFound(path);
            return System.IO.File.ReadAllText(path);
        }

        public static byte[] ReadAllBytes(string path)
        {
            if(!Exists(path)) FileNotFound(path);
            return System.IO.File.ReadAllBytes(path);
        }

        public static string ReadAllData(string path)
        {
            string[] split = path.Split('/');
            Bundle bundle = Instances.Data.Find(split[0]);
            return bundle.ReadFile(split[1]);
        }

        public static bool Exists(string path) => System.IO.File.Exists(path);

        private static void FileNotFound(string path)
        {
            throw new EmissionException(EmissionException.EmissionIoException, $"'{path}': File Not Found!");
        }
    }
}
