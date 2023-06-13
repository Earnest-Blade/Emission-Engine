using System;
using System.Diagnostics;

namespace Emission.Core
{
    public static class Time
    {
        /// <summary>
        /// Return the time difference between the current and previous frame.
        /// </summary>
        public static double DeltaTime
        {
            get; 
            internal set;
        }

        /// <summary>
        /// Return the time difference between the current and previous frame.
        /// </summary>
        public static float DeltaTimeAsFloat => (float)DeltaTime;
        
        /// <summary>
        /// Return current <see cref="Application"/>'s fps.
        /// </summary>
        public static uint Fps
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

        /// <summary>
        /// Return the current time as a string.
        /// </summary>
        /// <returns></returns>
        public static string TimeAsString()
        {
            return ((DateTimeOffset)Process.GetCurrentProcess().StartTime).ToString("G");
        } 
    }
}
