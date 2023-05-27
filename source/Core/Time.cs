using System;
using System.Diagnostics;

namespace Emission.Core
{
    public static class Time
    {
        /// <summary>
        /// Return current time by using <see cref="Time"/>. return a double so it can be change as
        /// a float easily.
        /// </summary>
        //public static long Current() => ((DateTimeOffset)Process.GetCurrentProcess().StartTime).ToUnixTimeMilliseconds(); //(float)Glfw.glfwGetTime();

        /// <summary>
        /// Return delta time use to define movements by time.
        /// </summary>
        public static float DeltaTime
        {
            get; 
            internal set;
        }

        /// <summary>
        /// Return current <see cref="Window"/>'s fps.
        /// </summary>
        public static int Fps
        {
            get;
            internal set;
        }
        
        /// <summary>
        /// Return the current time in nano second.
        /// Equivalent to System.nanoTime() in Java.
        /// </summary>
        /// <returns>Current time</returns>
        public static long NanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

        /// <summary>
        /// Return the current time in milliseconds.
        /// </summary>
        /// <returns></returns>
        public static long MsTime()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
        }

        public static string TimeAsString()
        {
            return ((DateTimeOffset)Process.GetCurrentProcess().StartTime).ToString("G");
        } 
    }
}
