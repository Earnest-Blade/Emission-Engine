using System;
using System.Runtime.InteropServices;

using Emission.Annotations;
using Newtonsoft.Json;

namespace Emission.Mathematics
{
    [PageSerializable]
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

        [JsonIgnore]
        public readonly float Length => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);

        [JsonIgnore]
        public readonly float LengthSquared => X * X + Y * Y + Z * Z + W * W;

        [JsonIgnore]
        public Vector3 Xyz
        {
            get => new Vector3(X, Y, Z);
            set => this = new Vector4(value, 0);
        }

        [JsonIgnore]
        public Vector2 Xy
        {
            get => new(X, Y);
            set => this = new Vector4(value, 0, 0);
        }

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

        public Vector4 Normalize() => Normalize(this);
        public Vector4 Pow(float exponent) => Pow(this, exponent);
        public Vector4 Cross(Vector4 b) => Cross(this, b);
        public Vector4 Negate() => Negate(this);
        
        public float Dot(Vector4 b) => Dot(this, b);

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
        
        public static Vector4 Normalize(Vector4 vector)
        {
            float scale = 1.0f / vector.Length;
            vector.X *= scale;
            vector.Y *= scale;
            vector.Z *= scale;
            vector.W *= scale;

            return vector;
        }

        public static Vector4 Pow(Vector4 vector, float exponent)
        {
            vector.X = MathF.Pow(vector.X, exponent);
            vector.Y = MathF.Pow(vector.Y, exponent);
            vector.Z = MathF.Pow(vector.Z, exponent);
            vector.W = MathF.Pow(vector.W, exponent);
            return vector;
        }

        public static float Dot(Vector4 left, Vector4 right)
        {
            return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W);
        }

        public static Vector4 Cross(Vector4 left, Vector4 right)
        {
            return new Vector4(Vector3.Cross(left.Xyz, right.Xyz), 0);
        }

        public static Vector4 Negate(Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static Vector4 Clamp(Vector4 value, float min, float max) => Clamp(value, new Vector4(min), new Vector4(max));
        public static Vector4 Clamp(Vector4 value, Vector4 min, Vector4 max)
        {
            float x = value.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;

            float y = value.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            float z = value.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;

            float w = value.X;
            w = (w > max.W) ? max.W : w;
            w = (w < min.W) ? min.W : w;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 Add(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 Add(Vector4 left, float scale)
        {
            return new Vector4(left.X + scale, left.Y + scale, left.Z + scale, left.W + scale);
        }

        public static Vector4 Subtract(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4 Subtract(Vector4 left, float scale)
        {
            return new Vector4(left.X - scale, left.Y - scale, left.Z - scale, left.W - scale);
        }

        public static Vector4 Subtract(float scale, Vector4 right)
        {
            return new Vector4(scale - right.X, scale - right.Y, scale - right.Z, scale - right.W);
        }

        public static Vector4 Multiply(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }

        public static Vector4 Multiply(Vector4 left, float scale)
        {
            return new Vector4(left.X * scale, left.Y * scale, left.Z * scale, left.W * scale);
        }

        public static Vector4 Divide(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
        }

        public static Vector4 Divide(Vector4 left, float scale)
        {
            return new Vector4(left.X / scale, left.Y / scale, left.Z / scale, left.W / scale);
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

