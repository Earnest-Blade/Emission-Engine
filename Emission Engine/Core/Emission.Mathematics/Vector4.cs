using System;
using System.Runtime.InteropServices;

namespace Emission.Mathematics
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4 : IEquatable<Vector4>
    {
        public static readonly Vector4 UnitX = new Vector4(1, 0, 0, 0);
        public static readonly Vector4 UnitY = new Vector4(0, 1, 0, 0);
        public static readonly Vector4 UnitZ = new Vector4(0, 0, 1, 0);
        public static readonly Vector4 UnitW = new Vector4(0, 0, 0, 1);

        public static readonly Vector4 Zero = new Vector4(0, 0, 0, 0);
        public static readonly Vector4 One = new Vector4(1, 1, 1, 1);
        
        public float X;
        public float Y;
        public float Z;
        public float W;
        
        public readonly float Length => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
        public readonly float LengthSquared => X * X + Y * Y + Z * Z + W * W;

        public float this[int x]
        {
            get => ToArray()[x];
            set
            {
                if (x > 3 || x < 0) throw new IndexOutOfRangeException();
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
                    case 3:
                        W = value;
                        break;
                }
            }
        }

        public Vector3 Xyz
        {
            get => new Vector3(X, Y, Z);
            set => this = new Vector4(value, 0);
        }

        public Vector2 Xy
        {
            get => new (X, Y);
            set => this = new Vector4(value, 0, 0);
        }
        
        public Vector4(float value) : this(value, value, value, value) {}
        public Vector4(Vector2 value, float z, float w) : this(value.X, value.Y, z, w) {}
        public Vector4(Vector3 value, float w) : this(value.X, value.Y, value.Z, w) {}
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        
        public Vector4 ToRadians()
        {
            return new Vector4(
                MathHelper.DegreesToRadians(X), 
                MathHelper.DegreesToRadians(Y),
                MathHelper.DegreesToRadians(Z),
                MathHelper.DegreesToRadians(W));
        }
        
        public Vector4 ToDegrees()
        {
            return new Vector4(
                MathHelper.RadiansToDegrees(X), 
                MathHelper.RadiansToDegrees(Y),
                MathHelper.RadiansToDegrees(Z),
                MathHelper.RadiansToDegrees(W));
        }
        
        public float[] ToArray() => new[] { X, Y, Z, W };
        
        public static Vector4 Negate(Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }
        
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            left.W += right.W;
            return left;
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            left.W -= right.W;
            return left;
        }

        public static Vector4 operator -(Vector4 vec)
        {
            vec.X = -vec.X;
            vec.Y = -vec.Y;
            vec.Z = -vec.Z;
            vec.W = -vec.W;
            return vec;
        }

        public static Vector4 operator *(Vector4 vec, float scale)
        {
            vec.X *= scale;
            vec.Y *= scale;
            vec.Z *= scale;
            vec.W *= scale;
            return vec;
        }

        public static Vector4 operator *(float scale, Vector4 vec)
        {
            vec.X *= scale;
            vec.Y *= scale;
            vec.Z *= scale;
            vec.W *= scale;
            return vec;
        }

        public static Vector4 operator *(Vector4 vec, Vector4 scale)
        {
            vec.X *= scale.X;
            vec.Y *= scale.Y;
            vec.Z *= scale.Z;
            vec.W *= scale.W;
            return vec;
        }

        public static Vector4 operator /(Vector4 vec, float scale)
        {
            vec.X /= scale;
            vec.Y /= scale;
            vec.Z /= scale;
            vec.W /= scale;
            return vec;
        }

        public static Vector4 operator /(Vector4 vec, Vector4 scale)
        {
            vec.X /= scale.X;
            vec.Y /= scale.Y;
            vec.Z /= scale.Z;
            vec.W /= scale.W;
            return vec;
        }

        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !(left == right);
        }

        public bool Equals(Vector4 other)
        {
            return (MathF.Abs(other.X - X) < MathHelper.ZeroTolerance 
                && MathF.Abs(other.Y - Y) < MathHelper.ZeroTolerance
                && MathF.Abs(other.Z - Z) < MathHelper.ZeroTolerance
                && MathF.Abs(other.W - W) < MathHelper.ZeroTolerance);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4 other && Equals(other);
        }
        
        public override string ToString() => $"[{X}, {Y}, {Z}, {W}]";

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }
    }
}

