using System;
using System.Runtime.InteropServices;
using Emission.Core.IO;

namespace Emission.Natives.GLFW
{
    /// <summary>
    /// Wrapper around a handle for a window cursor object.
    /// </summary>
    /// <seealso cref="Cursor" />
    [StructLayout(LayoutKind.Sequential)]
    public struct Cursor : IEquatable<Cursor>
    {
        public static readonly Cursor None;

        public readonly Icon Icon;
        private readonly IntPtr handle;

        public Cursor(Icon icon)
        {
            Icon = icon;
            handle = IntPtr.Zero;
        }
        
        public static bool operator ==(Cursor left, Cursor right) { return left.Equals(right); }
        public static bool operator !=(Cursor left, Cursor right) { return !left.Equals(right); }
        
        public bool Equals(Cursor other) { return handle.Equals(other.handle); }

        public override bool Equals(object obj)
        {
            if (obj is Cursor cur)
                return Equals(cur);
            return false;
        }

        public override int GetHashCode() { return handle.GetHashCode(); }
    }
}