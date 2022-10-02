using System;

namespace Emission
{
    public class Debug
    {
        public Debug()
        {
            
        }

        public Debug(string title)
        {
            Console.Title = title;
        }
        
        public Debug(string title, int width, int height)
        {
            Console.Title = title;
            Console.SetWindowSize(width, height);
        }
        
        public static void Log(string str)
        {
            Console.WriteLine(str);
        }

        public static void Log(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        public static bool HasInstance() => Instances.Debugger != null;
    }
}
