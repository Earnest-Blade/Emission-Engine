using System;

namespace Emission
{
    public class Debug
    {
        private static readonly Debug _instance = new Debug();

        public static Debug Singleton
        {
            get => _instance;
        }
        
        private string Header  {  get => "At " + DateTime.Now.ToString("u") + " : ";  }
        private string _logContent;

        internal void WriteLogs()
        {
            Resources.CreateNew("logs.txt");
            Resources.Write("logs.txt", _instance._logContent);
        }

        public static void Log(string s)
        {
            _instance._logContent += "\n" + Singleton.Header + s;
            Console.WriteLine(s);
        }

        public static void Log(float[] f)
        {
            Log($"[{string.Join(", ", f)}]");
        }
        
        public static void Log(int[] f)
        {
            Log($"[{string.Join(", ", f)}]");
        }

        public static void Log(Object o)
        {
            Log(o.ToString());
        }

        public static void LogError(string s)
        {
            _instance._logContent += "\n" + Singleton.Header + s;
            Console.Error.WriteLine(s);
        }

        public static void LogError(Object o)
        {
            LogError(o.ToString());
        }
    }
}
