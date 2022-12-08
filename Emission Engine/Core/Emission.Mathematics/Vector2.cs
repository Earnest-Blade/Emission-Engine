using Emission.Annotations;
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace Emission.Mathematics
{
    [PageSerializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        
        public float X;
        public float Y;

        [JsonIgnore]
        public float Length => MathF.Sqrt(X * X + Y * Y);

        [JsonIgnore]
        public float LengthSquared => X * X + Y * Y;

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

        [JsonIgnore]
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

        public Vector2 Normalize() => Normalize(this);
        public Vector2 Pow(float exponent) => Pow(this, exponent);
        public Vector2 Negate() => Negate(this);

        public float Dot(Vector2 b) => Dot(this, b);

        public Vector2 ToRadians()
        {
            return new Vector2(
                MathHelper.DegreesToRadians(X),
                MathHelper.DegreesToRadians(Y));
        }

        public Vector2 ToDegrees()
        {
            return new Vector2(
                MathHelper.RadiansToDegrees(X),
                MathHelper.RadiansToDegrees(Y));
        }

        public float[] ToArray() => new[] { X, Y };

        public static Vector2 Normalize(Vector2 vec)
        {
            float scale = 1.0f / vec.Length;
            vec.X *= scale;
            vec.Y *= scale;
            return vec;
        }

        public static Vector2 Pow(Vector2 vec, float exponent)
        {
            vec.X = MathF.Pow(vec.X, exponent);
            vec.Y = MathF.Pow(vec.Y, exponent);
            return vec;
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        public static Vector2 Negate(Vector2 value)
        {
            return new Vector2(-value.X, -value.Y);
        }

        public static Vector2 Clamp(Vector2 value, float min, float max) => Clamp(value, new Vector2(min), new Vector2(max));
        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            float x = value.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;

            float y = value.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            return new Vector2(x, y);
        }

        public static Vector2 Add(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 Add(Vector2 left, float scale)
        {
            return new Vector2(left.X + scale, left.Y + scale);
        }

        public static Vector2 Subtract(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 Subtract(Vector2 left, float scale)
        {
            return new Vector2(left.X - scale, left.Y - scale);
        }

        public static Vector2 Subtract(float scale, Vector2 right)
        {
            return new Vector2(scale - right.X, scale - right.Y);
        }

        public static Vector2 Multiply(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }

        public static Vector2 Multiply(Vector2 left, float scale)
        {
            return new Vector2(left.X * scale, left.Y * scale);
        }

        public static Vector2 Divide(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        public static Vector2 Divide(Vector2 left, float scale)
        {
            return new Vector2(left.X / scale, left.Y / scale);
        }

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
