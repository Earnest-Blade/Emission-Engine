using System;
using System.Runtime.InteropServices;

using Emission.Mathematics;
using Emission.Window.GLFW;

namespace Emission
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Monitor : IEquatable<Monitor>
    {
        /// <summary>
        /// Represent a null monitor
        /// </summary>
        public static readonly Monitor None;

        /// <summary>
        /// Get Current monitor.
        /// </summary>
        public static Monitor PrimaryMonitor => Glfw.GetPrimaryMonitor();

        /// <summary>
        /// Get an array of all recognize monitors.
        /// </summary>
        public static Monitor[] Monitors
        {
            get
            {
                var ptr = Glfw.GetMonitors(out var count);
                var monitors = new Monitor[count];
                var offset = 0;
                for (var i = 0; i < count; i++, offset += IntPtr.Size)
                {
                    monitors[i] = Marshal.PtrToStructure<Monitor>(ptr + offset);
                }

                return monitors;
            }
        }

        /// <summary>
        /// Return the name of the monitor.
        /// </summary>
        public string Name => Glfw.GetMonitorName(this);

        /// <summary>
        /// Get a <see cref="Rectangle"/> that represent the position and the size with the monitor.
        /// </summary>
        public Rectangle WorkArea
        {
            get
            {
                Glfw.GetMonitorWorkArea(_handle, out var x, out var y, out var width, out var height);
                return new Rectangle(x, y, width, height);
            }
        }
        
        /// <summary>
        /// Retrieves the content scale for the specified monitor.
        /// The content scale is the ratio between the current DPI and the platform's default DPI.
        /// </summary>
        public Vector2 ContentScale
        {
            get
            {
                Glfw.GetMonitorContentScale(_handle, out var x, out var y);
                return new Vector2(x, y);
            }
        }
        
        /// <summary>
        /// Returns the current value of the user-defined pointer of the specified monitor.
        /// The user-pointer, or <see cref="IntPtr.Zero"/> if none is defined.
        /// </summary>
        public IntPtr UserPointer
        {
            get => Glfw.GetMonitorUserPointer(_handle);
            set => Glfw.SetMonitorUserPointer(_handle, value);
        }
        
        private readonly IntPtr _handle;

        public Monitor()
        {
            _handle = default;
        }

        public bool Equals(Monitor other) { return _handle.Equals(other._handle); }
        public override bool Equals(object obj)
        {
            if (obj is Monitor monitor) return Equals(monitor);
            return false;
        }

        public override int GetHashCode() { return _handle.GetHashCode(); }

        public static bool operator ==(Monitor left, Monitor right) { return left.Equals(right); }
        public static bool operator !=(Monitor left, Monitor right) { return !left.Equals(right); }

        public override string ToString() { return _handle.ToString(); }
        
    }
}