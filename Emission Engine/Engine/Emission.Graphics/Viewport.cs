using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Emission.Graphics
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Viewport : IEquatable<Viewport>
    {
        public int X;
        public int Y;
        
        public float Width;
        public float Height;

        public float NearDepth;
        public float FarDepth;

        public float Aspect => Width / Height;

        public Viewport(float width, float height, float nearDepth, float farDepth) : this(0, 0, width, height, nearDepth, farDepth) {}
        public Viewport(int x, int y, float width, float height) : this(x, y, width, height, 0.1f, 1.0f) {}
        public Viewport(int x, int y, float width, float height, float nearDepth, float farDepth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            NearDepth = nearDepth;
            FarDepth = farDepth;
        }

        public bool Equals(Viewport other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height) &&
                   NearDepth.Equals(other.NearDepth) && FarDepth.Equals(other.FarDepth);
        }

        public override bool Equals(object obj)
        {
            return obj is Viewport other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height, NearDepth, FarDepth);
        }
    }
}
