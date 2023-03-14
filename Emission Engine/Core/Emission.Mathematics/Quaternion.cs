using System;
using System.Runtime.InteropServices;

namespace Emission.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        public static readonly Quaternion Zero = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        public static readonly Quaternion One = new Quaternion(1.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        
        public float X;
        public float Y;
        public float Z;
        public float W;

        public readonly float Xx => X * X;
        public readonly float Yy => Y * Y;
        public readonly float Zz => Z * Z;
        public readonly float Ww => W * W;

        public readonly float Xy => X * Y;
        public readonly float Xz => X * Z;
        public readonly float Zw => Z * W;
        public readonly float Zx => Z * X;
        public readonly float Yw => Y * W;
        public readonly float Yz => Y * Z;
        public readonly float Xw => X * W;

        public Vector3 Xyz => new Vector3(X, Y, Z);

        public readonly float Length => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
        public readonly float LengthSquared => X * X + Y * Y + Z * Z + W * W;

        public readonly float Angle
        {
            get
            {
                float length = X * X + Y * Y + Z * Z;
                if (length < MathHelper.ZeroTolerance) return 0.0f;
                return 2 * MathF.Acos(W);
            }
        }

        public readonly Vector3 Axis
        {
            get
            {
                float length = X * X + Y * Y + Z * Z;
                if(length < MathHelper.ZeroTolerance) return Vector3.UnitX;
                float inverse = 1.0f / Length;
                return new Vector3(X * inverse, Y * inverse, Z * inverse);
            }
        }

        public Quaternion() : this(0, 0, 0, 0) {}
        public Quaternion(Vector2 value, float z, float w) : this(value.X, value.Y, z, w) {}
        public Quaternion(Vector3 value, float w) : this(value.X, value.Y, value.Z, w) {}
        public Quaternion(Vector4 value) : this(value.X, value.Y, value.Z, value.W) {}
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector3 ToEulerAngles()
        {
            Vector3 euler = Vector3.Zero;
                
            // roll
            euler.X = MathF.Atan2(2 * (W * X + Y * Z), 1 - 2 * (X * X + Y * Y));

            // Pitch
            float fSinPitch = 2 * (W * Y - Z * X);
            if (MathF.Abs(fSinPitch) >= 1)
                euler.Y = MathF.CopySign(MathHelper.Pi / 2, fSinPitch);
            else
                euler.Y = MathF.Asin(fSinPitch);
                
            // Yaw
            euler.Z = MathF.Atan2(2 * (W * Z + X * Y), 1 - 2 * (Y * Y + Z * Z));
                
            return euler;
        }
        
        public float[] ToArray() => new[] { X, Y, Z, W };

        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            return new Quaternion()
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
                W = left.W + right.W
            };
        }
        
        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            return new Quaternion()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
                Z = left.Z - right.Z,
                W = left.W - right.W
            };
        }

        public static Quaternion Multiply(Quaternion left, float scale)
        {
            return new Quaternion()
            {
                X = left.X * scale,
                Y = left.Y * scale,
                Z = left.Z * scale,
                W = left.W * scale
            };
        }
        
        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            float lx = left.X;
            float ly = left.Y;
            float lz = left.Z;
            float lw = left.W;
            float rx = right.X;
            float ry = right.Y;
            float rz = right.Z;
            float rw = right.W;

            return new Quaternion(
                (rx * lw + lx * rw + ry * lz) - (rz * ly),
                (ry * lw + ly * rw + rz * lx) - (rx * lz),
                (rz * lw + lz * rw + rx * ly) - (ry * lx),
                (rw * lw) - (rx * lx + ry * ly + rz * lz));
        }

       
        public static Quaternion Invert(Quaternion value)
        {
            if (value.LengthSquared > MathHelper.ZeroTolerance)
            {
                float inverse = 1.0f / value.LengthSquared;

                value.X = -value.X * inverse;
                value.Y = -value.Y * inverse;
                value.Z = -value.Z * inverse;
                value.W = -value.W * inverse;
            }
            return value;
        }

        public static Quaternion Negate(Quaternion value)
        {
            return new Quaternion(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static Quaternion Normalize(Quaternion value)
        {
            if (value.Length > MathHelper.ZeroTolerance)
            {
                float inverse = 1.0f / value.Length;
                value.X *= inverse;
                value.Y *= inverse;
                value.Z *= inverse;
                value.W *= inverse;
            }

            return value;
        }

        public static Quaternion RotateX(float x)
        {
            return new Quaternion(
                MathF.Cos(x / 2), MathF.Sin(x / 2), 0, 0
            );
        }
        
        public static Quaternion RotateY(float y)
        {
            return new Quaternion(
                MathF.Cos(y / 2), 0, MathF.Sin(y / 2), 0
            );
        }
        
        public static Quaternion FromAxis(Vector3 axis, float angle)
        {
            Vector3 norm = Vector3.Normalize(axis);
            float half = angle * 0.5f;
            float sin = MathF.Sin(half);

            return new Quaternion()
            {
                X = norm.X * sin,
                Y = norm.Y * sin,
                Z = norm.Z * sin,
                W = MathF.Cos(half)
            };
        }

        public static Quaternion FromMatrix(Matrix4 matrix)
        {
            float sqrt;
            float half;
            float scale = matrix.M11 + matrix.M22 + matrix.M33;

            if (scale > 0.0f)
            {
                sqrt = MathF.Sqrt(scale + 1.0f);
                sqrt = 0.5f / sqrt;

                return new Quaternion
                {
                    X = (matrix.M23 - matrix.M32) * sqrt,
                    Y = (matrix.M31 - matrix.M13) * sqrt,
                    Z = (matrix.M12 - matrix.M21) * sqrt,
                    W = sqrt * 0.5f
                };
            }

            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
                half = 0.5f / sqrt;

                return new Quaternion
                {
                    X = 0.5f * sqrt,
                    Y = (matrix.M12 + matrix.M21) * half,
                    Z = (matrix.M13 + matrix.M31) * half,
                    W = (matrix.M23 - matrix.M32) * half
                };
            }

            if (matrix.M22 > matrix.M33)
            {
                sqrt = MathF.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
                half = 0.5f / sqrt;

                return new Quaternion
                {
                    X = (matrix.M21 + matrix.M12) * half,
                    Y = 0.5f * sqrt,
                    Z = (matrix.M32 + matrix.M23) * half,
                    W = (matrix.M31 - matrix.M13) * half
                };
            }

            sqrt = MathF.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
            half = 0.5f / sqrt;

            return new Quaternion
            {
                X = (matrix.M31 + matrix.M13) * half,
                Y = (matrix.M32 + matrix.M23) * half,
                Z = 0.5f * sqrt,
                W = (matrix.M12 - matrix.M21) * half
            };
        }

        public static Quaternion FromEulerAngles(float pitch, float yaw, float roll) => FromEulerAngles(new Vector3(roll, pitch, yaw));
        public static Quaternion FromEulerAngles(Vector3 value)
        {
            var cosX = MathF.Cos(value.X * 0.5f);
            var cosY = MathF.Cos(value.Y * 0.5f);
            var cosZ = MathF.Cos(value.Z * 0.5f);
            var sinX = MathF.Sin(value.X * 0.5f);
            var sinY = MathF.Sin(value.Y * 0.5f);
            var sinZ = MathF.Sin(value.Z * 0.5f);

            return new Quaternion()
            {
                X = (sinX * cosY * cosZ) + (cosX * sinY * sinZ),
                Y = (cosX * sinY * cosZ) - (sinX * cosY * sinZ),
                Z = (cosX * cosY * sinZ) + (sinX * sinY * cosZ),
                W = (cosX * cosY * cosZ) - (sinX * sinY * sinZ)
            };
        }

        public static Quaternion operator +(Quaternion left, Quaternion right) => Add(left, right);
        public static Quaternion operator -(Quaternion left, Quaternion right) => Subtract(left, right);
        public static Quaternion operator *(Quaternion quaternion, float scale) => Multiply(quaternion, scale);
        public static Quaternion operator *(float scale, Quaternion quaternion) => Multiply(quaternion, scale);
        public static Quaternion operator *(Quaternion left, Quaternion right) => Multiply(left, right);
        public static bool operator ==(Quaternion left, Quaternion right) => left.Equals(right);
        public static bool operator !=(Quaternion left, Quaternion right) => !(left == right);

        public bool Equals(Quaternion other)
        {
            return (MathF.Abs(other.X - X) < MathHelper.ZeroTolerance
                    && MathF.Abs(other.Y - Y) < MathHelper.ZeroTolerance
                    && MathF.Abs(other.Z - Z) < MathHelper.ZeroTolerance
                    && MathF.Abs(other.W - W) < MathHelper.ZeroTolerance);
        }

        public override bool Equals(object obj)
        {
            return obj is Quaternion q && Equals(q);
        }

        public override string ToString() => $"[{X}, {Y}, {Z}, {W}]";

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }
    }
}