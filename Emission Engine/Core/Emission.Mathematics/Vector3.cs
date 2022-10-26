using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Emission.Mathematics
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);
        
        public float X;
        public float Y;
        public float Z;

        public float Length => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public float LengthSquared => X * X + Y * Y + Z * Z;
        
        public float this[int x]
        {
            get => ToArray()[x];
            set
            {
                if (x > 2 || x < 0) throw new IndexOutOfRangeException();
                switch (x)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                }
            }
        }

        public Vector2 Xy
        {
            get => new Vector2(X, Y);
            set => this = new Vector3(value, 0);
        }

        public Vector2 Yx
        {
            get => new Vector2(Y, X);
            set => this = new Vector3(value.Y, value.X, 0);
        }

        public Vector3 Zyx
        {
            get => new Vector3(Z, Y, X);
            set => this = new Vector3(value.Z, value.Y, value.X);
        }
        
        public Vector3(float value) : this(value, value, value) {}
        public Vector3(Vector2 value, float z) : this(value.X, value.Y, z) {}
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 ToRadians()
        {
            return new Vector3(
                MathHelper.DegreesToRadians(X), 
                MathHelper.DegreesToRadians(Y),
                MathHelper.DegreesToRadians(Z));
        }
        
        public Vector3 ToDegrees()
        {
            return new Vector3(
                MathHelper.RadiansToDegrees(X), 
                MathHelper.RadiansToDegrees(Y),
                MathHelper.RadiansToDegrees(Z));
        }

        public Vector3 Normalize() => Normalize(this);
        public Vector3 Pow(float exponent) => Pow(this, exponent);
        public Vector3 Cross(Vector3 b) => Cross(this, b);
        public Vector3 Negate() => Negate(this);
        public float Dot(Vector3 b) => Dot(this, b);

        public float[] ToArray() => new[] { X, Y, Z };

        public static Vector3 Normalize(Vector3 vec)
        {
            float scale = 1.0f / vec.Length;
            vec.X *= scale;
            vec.Y *= scale;
            vec.Z *= scale;
            return vec;
        }

        public static Vector3 Pow(Vector3 vec, float exponent)
        {
            vec.X = MathF.Pow(vec.X, exponent);
            vec.Y = MathF.Pow(vec.Y, exponent);
            vec.Z = MathF.Pow(vec.Z, exponent);
            return vec;
        }
        
        public static float Dot(Vector3 a, Vector3 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }
        
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                (a.Y * b.Z) - (a.Z * b.Y),
                (a.Z * b.X) - (a.X * b.Z),
                (a.X * b.Y) - (a.Y * b.X)
            );
        }

        public static Vector3 Negate(Vector3 value)
        {
            return new Vector3(-value.X, -value.Y, -value.Z);
        }

        public static Vector3 Clamp(Vector3 value, float min, float max) => Clamp(value, new Vector3(min), new Vector3(max));
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            float x = value.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;

            float y = value.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            float z = value.X;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;

            return new Vector3(x, y, z);
        }
        
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            return left;
        }

        public static Vector3 operator -(Vector3 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;
            vec.Z = -vec.Z;
            return vec;
        }

        public static Vector3 operator *(Vector3 vec, float scale)
        {
            vec.X *= scale;
            vec.Y *= scale;
            vec.Z *= scale;
            return vec;
        }

        public static Vector3 operator *(float scale, Vector3 vec)
        {
            vec.X *= scale;
            vec.Y *= scale;
            vec.Z *= scale;
            return vec;
        }

        public static Vector3 operator *(Vector3 vec, Vector3 scale)
        {
            vec.X *= scale.X;
            vec.Y *= scale.Y;
            vec.Z *= scale.Z;
            return vec;
        }

        public static Vector3 operator /(Vector3 vec, float scale)
        {
            vec.X /= scale;
            vec.Y /= scale;
            vec.Z /= scale;
            return vec;
        }

        public static Vector3 operator /(Vector3 vec, Vector3 scale)
        {
            vec.X /= scale.X;
            vec.Y /= scale.Y;
            vec.Z /= scale.Z;
            return vec;
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        public static implicit operator Vector3((float X, float Y, float Z) values)
        {
            return new Vector3(values.X, values.Y, values.Z);
        }
        
        public bool Equals(Vector3 other)
        {
            return (MathF.Abs(other.X - X) < MathHelper.ZeroTolerance 
                    && MathF.Abs(other.Y - Y) < MathHelper.ZeroTolerance
                    && MathF.Abs(other.Z - Z) < MathHelper.ZeroTolerance);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        public override string ToString() => $"[{X}, {Y}, {Z}]";

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }
    }
}