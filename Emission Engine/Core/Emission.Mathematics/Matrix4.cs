using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Emission.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class Matrix4 : IEquatable<Matrix4>
    {
        public static Matrix4 Identity => new Matrix4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);
        public static Matrix4 Zero => new Matrix4(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);
        public static Matrix4 One => new Matrix4(Vector4.One, Vector4.One, Vector4.One, Vector4.One);

        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;
        
        public float this[int x, int y]
        {
            get
            {
                if (x > 3 || x < 0) throw new IndexOutOfRangeException();

                return y switch
                {
                    0 => Row0.ToArray()[x],
                    1 => Row1.ToArray()[x],
                    2 => Row2.ToArray()[x],
                    3 => Row3.ToArray()[x],
                    _ => 0
                };
            }
            set
            {
                if (x > 3 || x < 0) throw new IndexOutOfRangeException();

                switch (y)
                {
                    case 0:
                        Row0[x] = value;
                        break;
                    case 1:
                        Row1[x] = value;
                        break;
                    case 2:
                        Row2[x] = value;
                        break;
                    case 3:
                        Row3[x] = value;
                        break;
                }
            }
        }

        [IgnoreDataMember]
        public Vector4 Column0
        {
            get => new Vector4(Row0.X, Row1.X, Row2.X, Row3.X);
            set
            {
                Row0.X = value.X;
                Row1.X = value.Y;
                Row2.X = value.Z;
                Row3.X = value.W;
            }
        }

        [IgnoreDataMember]
        public Vector4 Column1
        {
            get => new Vector4(Row0.Y, Row1.Y, Row2.Y, Row3.Y);
            set
            {
                Row0.Y = value.X;
                Row1.Y = value.Y;
                Row2.Y = value.Z;
                Row3.Y = value.W;
            }
        }
        
        [IgnoreDataMember]
        public Vector4 Column2
        {
            get => new Vector4(Row0.Z, Row1.Z, Row2.Z, Row3.Z);
            set
            {
                Row0.Z = value.X;
                Row1.Z = value.Y;
                Row2.Z = value.Z;
                Row3.Z = value.W;
            }
        }

        [IgnoreDataMember]
        public Vector4 Column3
        {
            get => new Vector4(Row0.W, Row1.W, Row2.W, Row3.W);
            set
            {
                Row0.W = value.X;
                Row1.W = value.Y;
                Row2.W = value.Z;
                Row3.W = value.W;
            }
        }
        
        /// <summary>
        /// Gets or sets the value at row 1, column 1 of this instance.
        /// </summary>
        public float M11
        {
            get => Row0.X;
            set => Row0.X = value;
        }

        /// <summary>
        /// Gets or sets the value at row 1, column 2 of this instance.
        /// </summary>
        public float M12
        {
            get => Row0.Y;
            set => Row0.Y = value;
        }

        /// <summary>
        /// Gets or sets the value at row 1, column 3 of this instance.
        /// </summary>
        public float M13
        {
            get => Row0.Z;
            set => Row0.Z = value;
        }

        /// <summary>
        /// Gets or sets the value at row 1, column 4 of this instance.
        /// </summary>
        public float M14
        {
            get => Row0.W;
            set => Row0.W = value;
        }

        /// <summary>
        /// Gets or sets the value at row 2, column 1 of this instance.
        /// </summary>
        public float M21
        {
            get => Row1.X;
            set => Row1.X = value;
        }

        /// <summary>
        /// Gets or sets the value at row 2, column 2 of this instance.
        /// </summary>
        public float M22
        {
            get => Row1.Y;
            set => Row1.Y = value;
        }

        /// <summary>
        /// Gets or sets the value at row 2, column 3 of this instance.
        /// </summary>
        public float M23
        {
            get => Row1.Z;
            set => Row1.Z = value;
        }

        /// <summary>
        /// Gets or sets the value at row 2, column 4 of this instance.
        /// </summary>
        public float M24
        {
            get => Row1.W;
            set => Row1.W = value;
        }

        /// <summary>
        /// Gets or sets the value at row 3, column 1 of this instance.
        /// </summary>
        public float M31
        {
            get => Row2.X;
            set => Row2.X = value;
        }

        /// <summary>
        /// Gets or sets the value at row 3, column 2 of this instance.
        /// </summary>
        public float M32
        {
            get => Row2.Y;
            set => Row2.Y = value;
        }

        /// <summary>
        /// Gets or sets the value at row 3, column 3 of this instance.
        /// </summary>
        public float M33
        {
            get => Row2.Z;
            set => Row2.Z = value;
        }

        /// <summary>
        /// Gets or sets the value at row 3, column 4 of this instance.
        /// </summary>
        public float M34
        {
            get => Row2.W;
            set => Row2.W = value;
        }

        /// <summary>
        /// Gets or sets the value at row 4, column 1 of this instance.
        /// </summary>
        public float M41
        {
            get => Row3.X;
            set => Row3.X = value;
        }

        /// <summary>
        /// Gets or sets the value at row 4, column 2 of this instance.
        /// </summary>
        public float M42
        {
            get => Row3.Y;
            set => Row3.Y = value;
        }

        /// <summary>
        /// Gets or sets the value at row 4, column 3 of this instance.
        /// </summary>
        public float M43
        {
            get => Row3.Z;
            set => Row3.Z = value;
        }

        /// <summary>
        /// Gets or sets the value at row 4, column 4 of this instance.
        /// </summary>
        public float M44
        {
            get => Row3.W;
            set => Row3.W = value;
        }

        public Matrix4() : this(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero) {}
        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }
        
        public Matrix4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, 
            float m21, float m22, float m23, float m30, float m31, float m32, float m33)
        {
            Row0 = new Vector4(m00, m01, m02, m03);
            Row1 = new Vector4(m10, m11, m12, m13);
            Row2 = new Vector4(m20, m21, m22, m23);
            Row3 = new Vector4(m30, m31, m32, m33);
        }

        public float Determinant()
        {
            float temp1 = M33 * M44 - M34 * M43;
            float temp2 = M32 * M44 - M34 * M42;
            float temp3 = M32 * M43 - M33 * M42;
            float temp4 = M31 * M44 - M34 * M41;
            float temp5 = M31 * M43 - M33 * M41;
            float temp6 = M31 * M42 - M32 * M41;

            return M11 * (M22 * temp1 - M23 * temp2 + M24 * temp3) - M12 * (M21 * temp1 - M23 * temp4 + M24 * temp5) +
                M13 * (M21 * temp2 - M22 * temp4 + M24 * temp6) - M14 * (M21 * temp3 - M22 * temp5 + M23 * temp6);
        }

        public Quaternion ExtractRotation() => ExtractRotation(true);
        public Quaternion ExtractRotation(bool rowNormalize)
        {
            Vector3 row0 = Row0.Xyz;
            Vector3 row1 = Row1.Xyz;
            Vector3 row2 = Row2.Xyz;

            if (rowNormalize)
            {
                row0 = Vector3.Normalize(row0);
                row1 = Vector3.Normalize(row1);
                row2 = Vector3.Normalize(row2);
            }

            // code below adapted from Blender
            Quaternion q = default(Quaternion);
            float trace = 0.25f * (row0[0] + row1[1] + row2[2] + 1.0f);

            if (trace > 0)
            {
                float sq = MathF.Sqrt(trace);

                q.W = sq;
                sq = 1.0f / (4.0f * sq);
                q.X = (row1[2] - row2[1]) * sq;
                q.Y = (row2[0] - row0[2]) * sq;
                q.Z = (row0[1] - row1[0]) * sq;
            }
            
            else if (row0[0] > row1[1] && row0[0] > row2[2])
            {
                float sq = 2.0f * MathF.Sqrt(1.0f + row0[0] - row1[1] - row2[2]);

                q.X = (float)(0.25 * sq);
                sq = 1.0f / sq;
                q.W = (row2[1] - row1[2]) * sq;
                q.Y = (row1[0] + row0[1]) * sq;
                q.Z = (row2[0] + row0[2]) * sq;
            }
            
            else if (row1[1] > row2[2])
            {
                float sq = 2.0f * MathF.Sqrt(1.0f + row1[1] - row0[0] - row2[2]);

                q.Y = (float)(0.25 * sq);
                sq = 1.0f / sq;
                q.W = (row2[0] - row0[2]) * sq;
                q.X = (row1[0] + row0[1]) * sq;
                q.Z = (row2[1] + row1[2]) * sq;
            }
            
            else
            {
                float sq = 2.0f * MathF.Sqrt(1.0f + row2[2] - row0[0] - row1[1]);

                q.Z = (float)(0.25 * sq);
                sq = 1.0f / sq;
                q.W = (row1[0] - row0[1]) * sq;
                q.X = (row2[0] + row0[2]) * sq;
                q.Y = (row2[1] + row1[2]) * sq;
            }

            q = Quaternion.Normalize(q);
            return q;
        }
        
        public float[] ToArray()
        {
            return new[] { M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44 };
        }

        /// <summary>
        /// Calculate the inverse of a matrix4.
        /// If the matrix cannot be inverted, then it will throw a new <see cref="InvalidOperationException"/> error.
        /// </summary>
        /// <param name="matrix4">Matrix4 to invert</param>
        /// <returns>An inverted matrix4</returns>
        /// <exception cref="InvalidOperationException">If the matrix cannot be inverted</exception>
        public static Matrix4 Invert(Matrix4 matrix4)
        { 
            float kpLo = matrix4.M33 * matrix4.M44 - matrix4.M43 * matrix4.M34;
            float jpLn = matrix4.M23 * matrix4.M44 - matrix4.M43 * matrix4.M24;
            float joKn = matrix4.M23 * matrix4.M34 - matrix4.M33 * matrix4.M24;
            float ipLm = matrix4.M13 * matrix4.M44 - matrix4.M43 * matrix4.M14;
            float ioKm = matrix4.M13 * matrix4.M34 - matrix4.M33 * matrix4.M14;
            float inJm = matrix4.M13 * matrix4.M24 - matrix4.M23 * matrix4.M14;

            float a11 = +(matrix4.M22 * kpLo - matrix4.M32 * jpLn + matrix4.M42 * joKn);
            float a12 = -(matrix4.M12 * kpLo - matrix4.M32 * ipLm + matrix4.M42 * ioKm);
            float a13 = +(matrix4.M12 * jpLn - matrix4.M22 * ipLm + matrix4.M42 * inJm);
            float a14 = -(matrix4.M12 * joKn - matrix4.M22 * ioKm + matrix4.M32 * inJm);

            float det = matrix4.M11 * a11 + matrix4.M21 * a12 + matrix4.M31 * a13 + matrix4.M41 * a14;

            if (MathF.Abs(det) < MathHelper.Epsilon)
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            
            float invDet = 1.0f / det;
            
            Matrix4 result = new Matrix4();
            result.Row0 = new Vector4(a11, a12, a13, a14) * invDet;

            result.Row1 = new Vector4(
                -(matrix4.M21 * kpLo - matrix4.M31 * jpLn + matrix4.M41 * joKn),
                +(matrix4.M11 * kpLo - matrix4.M31 * ipLm + matrix4.M41 * ioKm),
                -(matrix4.M11 * jpLn - matrix4.M21 * ipLm + matrix4.M41 * inJm),
                +(matrix4.M11 * joKn - matrix4.M21 * ioKm + matrix4.M31 * inJm)) * invDet;

            float gpHo = matrix4.M32 * matrix4.M44 - matrix4.M42 * matrix4.M34;
            float fpHn = matrix4.M22 * matrix4.M44 - matrix4.M42 * matrix4.M24;
            float foGn = matrix4.M22 * matrix4.M34 - matrix4.M32 * matrix4.M24;
            float epHm = matrix4.M12 * matrix4.M44 - matrix4.M42 * matrix4.M14;
            float eoGm = matrix4.M12 * matrix4.M34 - matrix4.M32 * matrix4.M14;
            float enFm = matrix4.M12 * matrix4.M24 - matrix4.M22 * matrix4.M14;

            result.Row2 = new Vector4(
                +(matrix4.M21 * gpHo - matrix4.M31 * fpHn + matrix4.M41 * foGn),
                -(matrix4.M11 * gpHo - matrix4.M31 * epHm + matrix4.M41 * eoGm),
                +(matrix4.M11 * fpHn - matrix4.M21 * epHm + matrix4.M41 * enFm),
                -(matrix4.M11 * foGn - matrix4.M21 * eoGm + matrix4.M31 * enFm)) * invDet;

            float glHk = matrix4.M32 * matrix4.M43 - matrix4.M42 * matrix4.M33;
            float flHj = matrix4.M22 * matrix4.M43 - matrix4.M42 * matrix4.M23;
            float fkGj = matrix4.M22 * matrix4.M33 - matrix4.M32 * matrix4.M23;
            float elHi = matrix4.M12 * matrix4.M43 - matrix4.M42 * matrix4.M13;
            float ekGi = matrix4.M12 * matrix4.M33 - matrix4.M32 * matrix4.M13;
            float ejFi = matrix4.M12 * matrix4.M23 - matrix4.M22 * matrix4.M13;

            result.Row3 = new Vector4(
                -(matrix4.M21 * glHk - matrix4.M31 * flHj + matrix4.M41 * fkGj),
                +(matrix4.M11 * glHk - matrix4.M31 * elHi + matrix4.M41 * ekGi),
                -(matrix4.M11 * flHj - matrix4.M21 * elHi + matrix4.M41 * ejFi),
                +(matrix4.M11 * fkGj - matrix4.M21 * ekGi + matrix4.M31 * ejFi)) * invDet;

            return result;
        }

        /// <summary>
        /// Create a new Matrix 4 from a Quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion use to create the matrix</param>
        /// <returns>A new Matrix4</returns>
        public static Matrix4 FromQuaternion(Quaternion quaternion)
        {
            return new Matrix4
            {
                M11 = 1f - (2f / quaternion.LengthSquared) * (quaternion.Yy + quaternion.Zz),
                M12 = (2f / quaternion.LengthSquared) * (quaternion.Xy + quaternion.Zw),
                M13 = (2f / quaternion.LengthSquared) * (quaternion.Xz - quaternion.Yw),
                M14 = 0f,
                M21 = (2f / quaternion.LengthSquared) * (quaternion.Xy - quaternion.Zw),
                M22 = 1f - (2f / quaternion.LengthSquared) * (quaternion.Xx+ quaternion.Zz),
                M23 = (2f / quaternion.LengthSquared) * (quaternion.Yz + quaternion.Xw),
                M24 = 0f,
                M31 = (2f / quaternion.LengthSquared) * (quaternion.Xz + quaternion.Yw),
                M32 = (2f / quaternion.LengthSquared) * (quaternion.Yz - quaternion.Xw),
                M33 = 1f - (2f / quaternion.LengthSquared) * (quaternion.Xx+ quaternion.Yy),
                M34 = 0f,
                
                Row3 = Vector4.UnitW
            };
        }

        public static Matrix4 Add(Matrix4 left, float scale)
        {
            left.Row0 += new Vector4(scale);
            left.Row1 += new Vector4(scale);
            left.Row2 += new Vector4(scale);
            left.Row3 += new Vector4(scale);
            return left;
        }
        
        public static Matrix4 Add(Matrix4 left, Matrix4 right)
        {
            left.Row0 += right.Row0;
            left.Row1 += right.Row1;
            left.Row2 += right.Row2;
            left.Row3 += right.Row3;
            return left;
        }
        
        public static Matrix4 Subtract(Matrix4 left, float scale)
        {
            left.Row0 -= new Vector4(scale);
            left.Row1 -= new Vector4(scale);
            left.Row2 -= new Vector4(scale);
            left.Row3 -= new Vector4(scale);
            return left;
        }
        
        public static Matrix4 Subtract(Matrix4 left, Matrix4 right)
        {
            left.Row0 -= right.Row0;
            left.Row1 -= right.Row1;
            left.Row2 -= right.Row2;
            left.Row3 -= right.Row3;
            return left;
        }
        
        public static Matrix4 Multiply(Matrix4 left, float scale)
        {
            left.Row0 *= scale;
            left.Row1 *= scale;
            left.Row2 *= scale;
            left.Row3 *= scale;
            return left;
        }
        
        public static Matrix4 Multiply(Matrix4 left, Matrix4 right)
        {
            Matrix4 result = new Matrix4
            {
                M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41,
                M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41,
                M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41,
                M41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41,
                M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42,
                M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42,
                M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42,
                M42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42,
                M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43,
                M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43,
                M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43,
                M43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43,
                M14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44,
                M24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44,
                M34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44,
                M44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44
            };
            return result;
        }
        
        public static Matrix4 Divide(Matrix4 left, float scale)
        {
            left.Row0 /= scale;
            left.Row1 /= scale;
            left.Row2 /= scale;
            left.Row3 /= scale;
            return left;
        }
        
        public static Matrix4 Divide(Matrix4 left, Matrix4 right)
        {
            left.Row0 /= right.Row0;
            left.Row1 /= right.Row1;
            left.Row2 /= right.Row2;
            left.Row3 /= right.Row3;
            return left;
        }

        /// <summary>
        /// Creates a matrix that uniformally scales along all three axis.
        /// </summary>
        /// <param name="scale">The uniform scale that is applied along all axis.</param>
        /// <returns>The created scaling matrix.</returns>
        public static Matrix4 Scale(float scale) => Scale(new Vector3(scale));
        /// <summary>
        /// Creates a matrix that uniformally scales along all three axis.
        /// </summary>
        /// <param name="scale">The uniform scale that is applied along all axis.</param>
        /// <returns>The created scaling matrix.</returns>
        public static Matrix4 Scale(Vector3 scale)
        {
            Matrix4 result = Identity;
            result.M11 = scale.X;
            result.M22 = scale.Y; 
            result.M33 = scale.Z;
            result.M44 = 1;
            return result;
        }

        /// <summary>
        /// Creates a translation matrix using the specified offsets.
        /// </summary>
        /// <param name="value">X, Y and Z coordinates as a <see cref="Vector3"/> offset.</param>
        /// <returns>The created translation matrix.</returns>
        public static Matrix4 Translation(Vector3 value) => Translation(value.X, value.Y, value.Z);
        /// <summary>
        /// Creates a translation matrix using the specified offsets.
        /// </summary>
        /// <param name="x">X-coordinate offset.</param>
        /// <param name="y">Y-coordinate offset.</param>
        /// <param name="z">Z-coordinate offset.</param>
        /// <returns>The created translation matrix.</returns>
        public static Matrix4 Translation(float x, float y, float z)
        {
            Matrix4 result = Identity;
            result.M14 = x;
            result.M24 = y;
            result.M34 = z;
            result.M44 = 1;
            
            return result;
        }

        /// <summary>
        /// Creates a matrix that rotates around the x-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>When the method completes, contains the created rotation matrix.</returns>
        public static Matrix4 RotationX(float angle)
        {
            Matrix4 matrix4 = Identity;
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            matrix4.M22 = cos;
            matrix4.M23 = sin;
            matrix4.M32 = -sin;
            matrix4.M33 = cos;
            return matrix4;
        }

        /// <summary>
        /// Creates a matrix that rotates around the y-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>When the method completes, contains the created rotation matrix.</returns>
        public static Matrix4 RotationY(float angle)
        {
            Matrix4 matrix4 = Identity;
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            matrix4.M11 = cos;
            matrix4.M13 = -sin;
            matrix4.M31 = sin;
            matrix4.M33 = cos;
            return matrix4;
        }
        
        /// <summary>
        /// Creates a matrix that rotates around the z-axis.
        /// </summary>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>When the method completes, contains the created rotation matrix.</returns>
        public static Matrix4 RotationZ(float angle)
        {
            Matrix4 matrix4 = Identity;
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            matrix4.M11 = cos;
            matrix4.M12 = sin;
            matrix4.M21 = -sin;
            matrix4.M22 = cos;
            return matrix4;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="quaternion">The quaternion to use to build the matrix.</param>
        /// <returns>The created rotation matrix.</returns>
        public static Matrix4 RotationQuaternion(Quaternion quaternion)
        {
            return new Matrix4()
            {
                M11 = 1 - (2f / quaternion.LengthSquared) * (quaternion.Yy + quaternion.Zz),
                M12 = (2f / quaternion.LengthSquared) * (quaternion.Xy - quaternion.Zw),
                M13 = (2f / quaternion.LengthSquared) * (quaternion.Xz + quaternion.Yw),
                M14 = 0.0f,

                M21 = (2f / quaternion.LengthSquared) * (quaternion.Xy + quaternion.Zw),
                M22 = 1 - (2f / quaternion.LengthSquared) * (quaternion.Xx + quaternion.Zz),
                M23 = (2f / quaternion.LengthSquared) * (quaternion.Yz - quaternion.Xw),
                M24 = 0.0f,

                M31 = (2f / quaternion.LengthSquared) * (quaternion.Xz - quaternion.Yw),
                M32 = (2f / quaternion.LengthSquared) * (quaternion.Yz + quaternion.Xw),
                M33 = 1 - (2f / quaternion.LengthSquared) * (quaternion.Xx + quaternion.Yy),
                M34 = 0.0f,

                Row3 = Vector4.UnitW
            };
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary axis.
        /// </summary>
        /// <param name="axis">The axis around which to rotate. This parameter is assumed to be normalized.</param>
        /// <param name="angle">Angle of rotation in radians. Angles are measured clockwise when looking along the rotation axis toward the origin.</param>
        /// <returns>When the method completes, contains the created rotation matrix.</returns>
        public static Matrix4 RotationAxis(ref Vector3 axis, float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);
            float xx = axis.X * axis.X;
            float yy = axis.Y * axis.Y;
            float zz = axis.Z * axis.Z;
            float xy = axis.X * axis.Y;
            float xz = axis.X * axis.Z;
            float yz = axis.Y * axis.Z;

            Matrix4 result = Identity;
            result.M11 = xx + cos * (1.0f - xx);
            result.M12 = xy - cos * xy + sin * axis.Z;
            result.M13 = xz - cos * xz - sin * axis.Y;
            result.M21 = xy - cos * xy - sin * axis.Z;
            result.M22 = yy + cos * (1.0f - yy);
            result.M23 = yz - cos * yz + sin * axis.X;
            result.M31 = xz - cos * xz + sin * axis.Y;
            result.M32 = yz - cos * yz - sin * axis.X;
            result.M33 = zz + cos * (1.0f - zz);
            return result;
        }

        /// <summary>
        /// Creates a perspective projection matrix based on a field of view.
        /// </summary>
        /// <param name="fov">Field of view in the y direction, in radians</param>
        /// <param name="aspect">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="nearPlane">Minimum z-value of the viewing volume.</param>
        /// <param name="farPlane">Maximum z-value of the viewing volume.</param>
        /// <returns></returns>
        public static Matrix4 PerspectiveProjection(float fov, float aspect, float nearPlane, float farPlane)
        {
            float yScale = (1.0f / MathF.Tan(fov * 0.5f));
            float xScale = yScale / aspect;

            float halfWidth = nearPlane / xScale;
            float halfHeight = nearPlane / yScale;

            return PerspectiveOffCenter(-halfWidth, halfWidth, -halfHeight, halfHeight, nearPlane, farPlane);
        }

        /// <summary>
        /// Create a customized perpective projection matrix.
        /// </summary>
        /// <param name="left">Minimum x-value of the viewing volume</param>
        /// <param name="right">Maximum x-value of the viewing volume</param>
        /// <param name="bottom">Minimum y-value of the viewing volume</param>
        /// <param name="top">Maximum y-value of the viewing volume</param>
        /// <param name="nearPlane">Minimum z-value of the viewing volume</param>
        /// <param name="farPlane">Maximum z-value of the viewing volume</param>
        /// <returns></returns>
        public static Matrix4 PerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlane, float farPlane)
        {
            float rangeZ = farPlane / (farPlane - nearPlane);

            return new Matrix4()
            {
                M11 = 2.0f * nearPlane / (right - left),
                M22 = 2.0f * nearPlane / (top - bottom),
                M31 = (left + right) / (left - right),
                M32 = (top + bottom) / (bottom - top),
                M33 = rangeZ,
                M34 = 1.0f,
                M43 = -nearPlane * rangeZ
            };
        }

        /// <summary>Creates an orthographic projection matrix.</summary>
        /// <param name="left">The left edge of the projection volume.</param>
        /// <param name="right">The right edge of the projection volume.</param>
        /// <param name="bottom">The bottom edge of the projection volume.</param>
        /// <param name="top">The top edge of the projection volume.</param>
        /// <param name="depthNear">The near edge of the projection volume.</param>
        /// <param name="depthFar">The far edge of the projection volume.</param>
        /// <returns>The resulting Matrix4 instance.</returns>
        public static Matrix4 OrthographicOffCenter(float left, float right, float bottom, float top, float depthNear,
            float depthFar)
        {
            return new Matrix4
            {
                M11 = 2 / (right - left),
                M14 = -(right + left) / (right - left),
                M22 = 2 / (top - bottom),
                M24 = -(top + bottom) / (top - bottom),
                M33 = -2 / (depthFar - depthNear),
                M34 = (depthFar + depthNear) / (depthFar - depthNear),
                M44 = 1f,
            };
        }

        /// <summary>Build a world space to camera space matrix </summary>
        /// <param name="eyeX">Eye X (camera) position in world space.</param>
        /// <param name="eyeY">Eye Y (camera) position in world space.</param>
        /// <param name="eyeZ">Eye Z (camera) position in world space.</param>
        /// <param name="targetX">Target X position in world space.</param>
        /// <param name="targetY">Target Y position in world space.</param>
        /// <param name="targetZ">Target Z position in world space.</param> 
        /// <param name="upX">Up X vector in world space</param>
        /// <param name="upY">Up Y vector in world space</param>
        /// <param name="upZ">Up Z vector in world space</param>
        /// <returns>A Matrix4 that transforms world space to camera space.</returns>
        public static Matrix4 LookAt(float eyeX, float eyeY, float eyeZ, float targetX, float targetY, float targetZ,
            float upX, float upY, float upZ)
        {
            return LookAt(new Vector3(eyeX, eyeY, eyeZ), new Vector3(targetX, targetY, targetZ),
                new Vector3(upX, upY, upZ));
        }

        /// <summary>Build a world space to camera space matrix.</summary>
        /// <param name="eye">Eye (camera) position in world space.</param>
        /// <param name="target">Target position in world space.</param>
        /// <param name="yUnit">Up vector in world space (should not be parallel to the camera direction, that is target - eye).</param>
        /// <returns>A Matrix4 that transforms world space to camera space.</returns>
        public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 yUnit)
        {
            Vector3 zAxis = Vector3.Normalize(eye - target);
            Vector3 xAxis = Vector3.Normalize(Vector3.Cross(yUnit, zAxis));
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);
            
            // Create identity matrix
            Matrix4 matrix = new Matrix4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW)
            {
                Row0 = new Vector4(xAxis.X, yAxis.X, zAxis.X, 0),
                Row1 = new Vector4(xAxis.Y, yAxis.Y,zAxis.Y, 0),
                Row2 = new Vector4(xAxis.Z, yAxis.Z, zAxis.Z, 0),
                Row3 = new Vector4(-xAxis.X * eye.X - xAxis.Y * eye.Y - xAxis.Z * eye.Z, 
                    -yAxis.X * eye.X - yAxis.Y * eye.Y - yAxis.Z * eye.Z, 
                    -zAxis.X * eye.X - zAxis.Y * eye.Y - zAxis.Z * eye.Z, 1)
            };
            return matrix;
        }

        public static Matrix4 operator +(Matrix4 left, Matrix4 right) => Add(left, right);
        public static Matrix4 operator -(Matrix4 left, Matrix4 right) => Subtract(left, right);
        public static Matrix4 operator *(Matrix4 matrix4, float scale) => Multiply(matrix4, scale);
        public static Matrix4 operator *(float scale, Matrix4 matrix4) => Multiply(matrix4, scale);
        public static Matrix4 operator *(Matrix4 left, Matrix4 right) => Multiply(left, right);
        public static Matrix4 operator /(Matrix4 matrix4, float scale) => Divide(matrix4, scale);
        public static Matrix4 operator /(Matrix4 left, Matrix4 right) => Divide(left, right);
        public static bool operator ==(Matrix4 left, Matrix4 right) => left!.Equals(right);
        public static bool operator !=(Matrix4 left, Matrix4 right) => !(left == right);

        public bool Equals(Matrix4 other)
        {
            return MathF.Abs(other!.M11 - M11) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M12 - M12) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M13 - M13) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M14 - M14) < MathHelper.ZeroTolerance &&

                   MathF.Abs(other.M21 - M21) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M22 - M22) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M23 - M23) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M24 - M24) < MathHelper.ZeroTolerance &&

                   MathF.Abs(other.M31 - M31) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M32 - M32) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M33 - M33) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M34 - M34) < MathHelper.ZeroTolerance &&

                   MathF.Abs(other.M41 - M41) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M42 - M42) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M43 - M43) < MathHelper.ZeroTolerance &&
                   MathF.Abs(other.M44 - M44) < MathHelper.ZeroTolerance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Matrix4)obj);
        }

        public override int GetHashCode()
        {
            return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode() +
                   M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode() +
                   M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode() +
                   M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
        }
    }
}
