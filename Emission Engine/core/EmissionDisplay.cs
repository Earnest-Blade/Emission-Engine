using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Emission
{
    public struct Display
    {
        /// <summary>
        /// The main monitor.
        /// </summary>
        public static Display Primary = new (Monitors.GetPrimaryMonitor());

        /// <summary>
        /// Get an array of all recognize monitors.
        /// </summary>
        public static Display[] Displays
        {
            get
            {
                Display[] array = new Display[Monitors.GetMonitors().Count];
                for (int i = 0; i < array.Length; i++)
                    array[i] = new Display(Monitors.GetMonitors()[i]);
                return array;
            }
        }

        /// <summary>
        /// If a GLFW Window is created, it will return window's monitor.
        /// Else it return the <see cref="Primary"/> monitor.
        /// </summary>
        public static unsafe Display Current =>
            Instances.Window != null ? new Display(Monitors.GetMonitorFromWindow(Instances.Window.WindowPointer)) 
                : new Display(Monitors.GetPrimaryMonitor());

        public Vector2i Resolution => (_info.HorizontalResolution, _info.VerticalResolution);
        public Vector2 Scale => (_info.HorizontalScale, _info.VerticalScale);
        
        public float Dpi => (_info.HorizontalDpi + _info.VerticalDpi) / 2;
        public float RawDpi => (_info.HorizontalRawDpi + _info.VerticalRawDpi) / 2;

        public IntPtr Pointer => _info.Handle.Pointer; 

        public string Name => _info.Name;
        
        private MonitorInfo _info;

        public Display(MonitorInfo info)
        {
            _info = info;
        }
    }
}
