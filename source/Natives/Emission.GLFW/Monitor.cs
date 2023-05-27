using System;
using System.Runtime.InteropServices;

using Emission.Core.Mathematics;
using Emission.Core.Memory;
using static Emission.Natives.GLFW.Glfw;

namespace Emission.Natives.GLFW.Window
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Monitor : IEquatable<Monitor>
    {
        /// <summary>
        /// Represent a null monitor
        /// </summary>
        public static readonly Monitor None;

        /// <summary>
        /// Get Current monitor.
        /// </summary>
        public static Monitor PrimaryMonitor => Marshal.PtrToStructure<Monitor>(new IntPtr(glfwGetPrimaryMonitor()));

        /// <summary>
        /// Get an array of all recognize monitors.
        /// </summary>
        public static Monitor[] Monitors
        {
            get
            {
                int count;
                Monitor** ptr = (Monitor**)glfwGetMonitors(&count);
                Monitor[] monitors = new Monitor[count];

                int offset = 0;
                for (int i = 0; i < count; i++, offset += IntPtr.Size)
                    monitors[i] = Marshal.PtrToStructure<Monitor>(new IntPtr(ptr) + offset);

                return monitors;
            }
        }

        /// <summary>
        /// Return the name of the monitor.
        /// </summary>
        public string Name => MemoryHelper.PtrToStringUtf8(glfwGetMonitorName((Monitor*)_handle));

        /// <summary>
        /// Get a <see cref="Rectangle"/> that represent the position and the size with the monitor.
        /// </summary>
        public Rectangle WorkArea
        {
            get
            {
                int x, y, width, height;
                glfwGetMonitorWorkarea((Monitor*)_handle, &x, &y, &width, &height);
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
                float x, y;
                glfwGetMonitorContentScale((Monitor*)_handle, &x, &y);
                return new Vector2(x, y);
            }
        }

        /// <summary>
        /// Returns the current value of the user-defined pointer of the specified monitor.
        /// The user-pointer, or <see cref="IntPtr.Zero"/> if none is defined.
        /// </summary>
        public IntPtr UserPointer
        {
            get => new (glfwGetMonitorUserPointer((Monitor*)_handle));
            set => glfwSetMonitorUserPointer((Monitor*)_handle, value.ToPointer());
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