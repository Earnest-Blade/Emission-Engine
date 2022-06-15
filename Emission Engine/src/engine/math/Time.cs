using System;
using System.Diagnostics;

using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Emission.Math
{
    public class Time
    {
        /// <summary>
        /// Return current time by using <see cref="GLFW.GetTime()"/>. return a double so it can be change as
        /// a float easily.
        /// </summary>
        public static double CurrentTime
        {
            get => GLFW.GetTime();
        }
        
        /// <summary>
        /// Return delta time use to define movements by time.
        /// </summary>
        public static float DeltaTime
        {
            get; 
            private set;
        }

        /// <summary>
        /// Return current <see cref="Window"/>'s fps.
        /// </summary>
        public static int Fps
        {
            get;
            private set;
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
        /// Static function used to define current <see cref="DeltaTime"/>.
        /// </summary>
        /// <param name="time">New Delta Time value</param>
        public static void SetDeltaTime(double time)
        {
            DeltaTime = (float)time;
        }

        /// <summary>
        /// Static function used to define current framerate (<see cref="Fps"/>).
        /// </summary>
        /// <param name="fps">New FPS value</param>
        public static void SetFps(int fps)
        {
            Fps = fps;
        }
    }
}