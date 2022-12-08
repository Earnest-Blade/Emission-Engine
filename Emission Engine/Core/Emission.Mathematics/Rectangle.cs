using Emission.Annotations;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Emission.Mathematics
{
    [Serializable]
    [PageSerializable]
    public struct Rectangle : IEquatable<Rectangle>
    {
        public static readonly Rectangle Zero = new Rectangle(0, 0, 0, 0);
        
        public float X;
        public float Y;
        
        public float Width;
        public float Height;

        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);
        public static bool operator !=(Rectangle left, Rectangle right) => !(left == right);

        public bool Equals(Rectangle other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }
    }
}
