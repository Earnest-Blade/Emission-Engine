using System;
using System.Diagnostics.CodeAnalysis;
using Emission.Mathematics.Numerics;

namespace Emission
{
    [SuppressMessage("Interoperability", "CA1416:Valider la compatibilité de la plateforme")]
    public class Debug
    {
        public const char ARRAY_SEPARATOR = ';';
        
        public Debug() { }

        public Debug(string title)
        {
            Console.Title = title;
        }
        
        public Debug(string title, int width, int height)
        {
            Console.Title = title;
            
            try { Console.SetWindowSize(width, height); }
            catch (Exception) { /* ignored */}
        }

        public static void Log(Array array) => Log('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Log(Vector2 vector2) => Log($"{vector2.ToString()}");
        public static void Log(Vector3 vector3) => Log($"{vector3.ToString()}");
        public static void Log(Vector4 vector4) => Log($"{vector4.ToString()}");
        public static void Log(Quaternion quaternion) => Log($"{quaternion.ToString()}");
        public static void Log(string obj) => Log((object)obj);

        public static void Log(params object[] str)
        {
            Console.WriteLine(string.Join(" ", str));
        }
        
        public static void Log(Matrix4 mat4) => Log($"[{string.Join(ARRAY_SEPARATOR, mat4.ToArray())}]");
        
        public static void LogColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        public static bool HasInstance() => Instances.Debugger != null;
    }
}
