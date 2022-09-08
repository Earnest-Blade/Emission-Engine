using System;
using System.Diagnostics;

using Emission.IO;
using Emission.Math;
using Emission.GFX;

using OpenTK.Mathematics;

namespace Emission
{
    public class Debug
    {
        public static Process Process => Process.GetCurrentProcess();
        public static OperatingSystem OperatingSystem => Environment.OSVersion;

        public static long RamUsage => GC.GetTotalMemory(true);
        
        private string _header { get => "At " + DateTime.Now.ToString("u") + " : ";  }
        private string _logContent;

        /// <summary>
        /// Write game's logs into a file.
        /// Create a new file, then write console's content.
        /// </summary>
        public void Save()
        {
            Resources.CreateNew("logs.txt");
            Resources.Write("logs.txt", 
                $"Running with {OperatingSystem.Platform} on version {OperatingSystem.Version}\n"
                + $"Running on process {Process.Id}\n"
                + _logContent);
        }

        /// <summary>
        /// Display a line on the screen, into 3D Space.
        /// </summary>
        /// <param name="start">Start point of the line.</param>
        /// <param name="end">End point of the line</param>
        /// <param name="color">Color of the line.</param>
        /// <param name="transform">Transform use to change the line's rotation, size etc...</param>
        public static void DrawLine(Vector3 start, Vector3 end, Color color, Transform transform)
        {
            Renderer.Singleton.DrawLine(start, end, color, transform);
        }
        
        /// <summary>
        /// Display a line on the screen, into 3D Space.
        /// </summary>
        /// <param name="start">Start point of the line.</param>
        /// <param name="end">End point of the line</param>
        /// <param name="color">Color of the line.</param>
        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            DrawLine(start, end, color, new Transform());
        }

        /// <summary>
        /// Display a simple plane of the screen, into 3D Space.
        /// </summary>
        /// <param name="size">Plane size</param>
        /// <param name="color">Plane color</param>
        /// <param name="transform">Transform use to change the cube's rotation, size etc...</param>
        public static void DrawPlane(Vector2 size, Vector3 color, Transform transform)
        {
            Renderer.Singleton.DrawPlane(size, color, transform);
        }

        /// <summary>
        /// Display a cube on the screen, into 3D Space.
        /// </summary>
        /// <param name="size">Cube size (Width, Height, Depth).</param>
        /// <param name="color">Cube color.</param>
        /// <param name="transform">Transform use to change the cube's rotation, size etc...</param>
        public static void DrawCube(Vector3 size, Vector3 color, Transform transform)
        {
            Renderer.Singleton.DrawCube(size, color, transform);
        }

        /// <summary>
        /// Display a cube on the screen, into 3D Space.
        /// </summary>
        /// <param name="sx">Cube width.</param>
        /// <param name="sy">Cube height.</param>
        /// <param name="sz">Cube depth.</param>
        /// <param name="color">Cube color.</param>
        /// <param name="transform">Transform use to change the line's rotation, size etc...</param>
        public static void DrawCube(float sx, float sy, float sz, Vector3 color, Transform transform)
        {
            Renderer.Singleton.DrawCube(new Vector3(sx, sy, sz), color, transform);
        }
        
        /// <summary>
        /// Write content into the console and add content to logs.
        /// </summary>
        /// <param name="s">String to print.</param>
        public static void Log(string s)
        {
            Singleton._logContent += "\n" + Singleton._header + s;
            Console.WriteLine(s);
        }

        /// <summary>
        /// Write content into the console and add content to logs.
        /// Joins list content into a string.
        /// </summary>
        /// <param name="f">Float Array list to print.</param>
        public static void Log(float[] f)
        {
            Log($"[{string.Join(", ", f)}]");
        }
        
        /// <summary>
        /// Write content into the console and add content to logs.
        /// Joins list content into a string.
        /// </summary>
        /// <param name="i">Int array list to print</param>
        public static void Log(int[] i)
        {
            Log($"[{string.Join(", ", i)}]");
        }

        
        /// <summary>
        /// Write content into the console and add content to logs.
        /// </summary>
        /// <param name="o">Object to print</param>
        public static void Log(Object o)
        {
            Log(o.ToString());
        }
        
        /// <summary>
        /// Write content as error into the console and add content to logs.
        /// </summary>
        /// <param name="s">String to print.</param>
        public static void LogError(string s)
        {
            Singleton._logContent += "\n" + Singleton._header + s;
            Console.Error.WriteLine(s);
        }

        
        /// <summary>
        /// Write content as error into the console and add content to logs.
        /// </summary>
        /// <param name="o">Object to print.</param>
        public static void LogError(Object o)
        {
            LogError(o.ToString());
        }

        /// <summary>
        /// Call an method if condition is true.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Assert(bool condition, Action action)
        {
            if(condition) action.Invoke();
        }

        /// <summary>
        /// Debug a string if condition is true.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="s"></param>
        public static void Assert(bool condition, string s)
        {
            if(condition) Log(s);
        }

        public static Debug Singleton => Application.Singleton.Debugger;
    }
}
