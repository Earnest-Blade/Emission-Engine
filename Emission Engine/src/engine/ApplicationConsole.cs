using System;
using System.Collections.Generic;
using System.Text;

namespace Emission
{
    class ApplicationConsole
    {
        private static readonly ApplicationConsole _instance = new ApplicationConsole();

        public string LogContent { get; private set; }

        public ApplicationConsole()
        {
            Console.Title = "Emission Debug Reader Console";
        }

        public static void Write()
        {
            Resources.CreateNew("logs.txt");
            Resources.Write("logs.txt", _instance.LogContent);
        }

        public static void Print(string s)
        {
            _instance.LogContent += "\n" + Header() + s;
            Console.WriteLine(s);
        }

        public static void Print(float[] f)
        {
            Console.WriteLine("[{0}]", string.Join(", ", f));
        }

        public static void Print(Object o)
        {
            _instance.LogContent += "\n" + Header() + o.ToString();
            Console.WriteLine(o.ToString());
        }

        public static void PrintError(string s)
        {
            _instance.LogContent += "\n" + Header() + s;
            Console.Error.WriteLine(s);
        }

        public static void PrintError(Object o)
        {
            _instance.LogContent += "\n" + Header() + o.ToString();
            Console.Error.WriteLine(o.ToString());
        }

        private static string Header()
        {
            return "At " + DateTime.Now.ToString("u") + " : ";
        }
    }
}
