using System;
using System.Runtime.InteropServices;

namespace Emission.Mathematics
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        
        public float X;
        public float Y;

        public float this[int x]
        {
            get => ToArray()[x];
            set
            {
                if (x > 1 || x < 0) throw new IndexOutOfRangeException();
                switch (x)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                }
            }
        }

        public Vector2 Yx
        {
            get => new Vector2(Y, X);
            set => this = new Vector2(value.Y, value.X);
        }

        public Vector2() : this(0, 0){}
        public Vector2(float value) : this(value, value) {}
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public float[] ToArray() => new[] { X, Y };

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        public static Vector2 operator -(Vector2 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;
            return vec;
        }

        public static Vector2 operator *(Vector2 vec, float scale)
        {
            vec.X *= scale;
            vec.Y *= scale;
            return vec;
        }

        public static Vector2 operator *(float scale, Vector2 vec)
        {
            vec.X *= scale;
            vec.Y *= scale;
            return vec;
        }

        public static Vector2 operator *(Vector2 vec, Vector2 scale)
        {
            vec.X *= scale.X;
            vec.Y *= scale.Y;
            return vec;
        }

        public static Vector2 operator /(Vector2 vec, float scale)
        {
            vec.X /= scale;
            vec.Y /= scale;
            return vec;
        }

        public static Vector2 operator /(Vector2 vec, Vector2 scale)
        {
            vec.X /= scale.X;
            vec.Y /= scale.Y;
            return vec;
        }
        
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public static implicit operator Vector2((float X, float Y) values)
        {
            return new Vector2(values.X, values.Y);
        }

        public bool Equals(Vector2 other)
        {
            return (MathF.Abs(other.X - X) < MathHelper.ZeroTolerance &&
                    MathF.Abs(other.Y - Y) < MathHelper.ZeroTolerance);
        }
        
        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override string ToString() => $"[{X}, {Y}]";

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }
}
