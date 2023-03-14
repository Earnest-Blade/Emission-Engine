using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Emission.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix3 : IEquatable<Matrix3>
    {
        public static Matrix3 Identity => new Matrix3(Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ);
        public static Matrix3 Zero => new Matrix3(Vector3.Zero, Vector3.Zero, Vector3.Zero);
        
        public Vector3 Row0;
        public Vector3 Row1;
        public Vector3 Row2;

        public float this[int x, int y]
        {
            get
            {
                if (x > 2 || x < 0) throw new IndexOutOfRangeException();

                return y switch
                {
                    0 => Row0.ToArray()[x],
                    1 => Row1.ToArray()[x],
                    2 => Row2.ToArray()[x],
                    _ => 0
                };
            }
            set
            {
                if (x > 2 || x < 0) throw new IndexOutOfRangeException();

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
                }
            }
        }

        [IgnoreDataMember]
        public Vector3 Column0
        {
            get => new Vector3(Row0.X, Row1.X, Row2.X);
            set
            {
                Row0.X = value.X;
                Row1.X = value.Y;
                Row2.X = value.Z;
            }
        }

        [IgnoreDataMember]
        public Vector3 Column1
        {
            get => new Vector3(Row0.Y, Row1.Y, Row2.Y);
            set
            {
                Row0.Y = value.X;
                Row1.Y = value.Y;
                Row2.Y = value.Z;
            }
        }

        [IgnoreDataMember]
        public Vector3 Column2
        {
            get => new Vector3(Row0.Z, Row1.Z, Row2.Z);
            set
            {
                Row0.Z = value.X;
                Row1.Z = value.Y;
                Row2.Z = value.Z;
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

        public Matrix3() : this(Vector3.Zero, Vector3.Zero, Vector3.Zero) {}
        public Matrix3(Vector3 row0, Vector3 row1, Vector3 row2)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
        }
        
        public Matrix3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
        {
            Row0 = new Vector3(m00, m01, m02);
            Row1 = new Vector3(m10, m11, m12);
            Row2 = new Vector3(m20, m21, m22);
        }

        public float Determinant()
        {
            return M11 * M21 * M33 - M11 * M23 * M32 + M12 * M23 * M31 - M12 * M21 * M33 + M13 * M21 * M32 - M13 * M22 * M31;
        }
        
        public void Transpose()
        {
            Matrix3 m = this;

            M12 = m.M21;
            M13 = m.M31;

            M21 = m.M12;
            M23 = m.M32;

            M31 = m.M13;
            M32 = m.M23;
        }

        public void Inverse()
        {
            float det = Determinant();
            if(det == 0.0f)
            {
                // Matrix not invertible. Setting all elements to NaN is not really
                // correct in a mathematical sense but it is easy to debug for the
                // programmer.
                M11 = float.NaN;
                M12 = float.NaN;
                M13 = float.NaN;

                M21 = float.NaN;
                M22 = float.NaN;
                M23 = float.NaN;

                M31 = float.NaN;
                M32 = float.NaN;
                M33 = float.NaN;
            }

            float invDet = 1.0f / det;

            float _a1 = invDet * (M22 * M33 - M23 * M32);
            float _M12 = -invDet * (M12 * M33 - M13 * M32);
            float _M13 = invDet * (M12 * M23 - M13 * M22);

            float _M21 = -invDet * (M21 * M33 - M23 * M31);
            float _M22 = invDet * (M11 * M33 - M13 * M31);
            float _M23 = -invDet * (M11 * M23 - M13 * M21);

            float _M31 = invDet * (M21 * M32 - M22 * M31);
            float _M32 = -invDet * (M11 * M32 - M12 * M31);
            float _M33 = invDet * (M11 * M22 - M12 * M21);

            M11 = M11;
            M12 = _M12;
            M13 = _M13;

            M21 = _M21;
            M22 = _M22;
            M23 = _M23;

            M31 = _M31;
            M32 = _M32;
            M33 = _M33;
        }

        /// <summary>
        /// Creates a rotation matrix from a set of euler angles.
        /// </summary>
        /// <param name="x">Rotation angle about the x-axis, in radians.</param>
        /// <param name="y">Rotation angle about the y-axis, in radians.</param>
        /// <param name="z">Rotation angle about the z-axis, in radians.</param>
        /// <returns>The rotation matrix</returns>
        public static Matrix3 FromEulerAnglesXYZ(float x, float y, float z)
        {
            float cr = (float) Math.Cos(x);
            float sr = (float) Math.Sin(x);
            float cp = (float) Math.Cos(y);
            float sp = (float) Math.Sin(y);
            float cy = (float) Math.Cos(z);
            float sy = (float) Math.Sin(z);

            float srsp = sr * sp;
            float crsp = cr * sp;

            Matrix3 m = new Matrix3();
            m.M11 = cp * cy;
            m.M12 = cp * sy;
            m.M13 = -sp;

            m.M21 = srsp * cy - cr * sy;
            m.M22 = srsp * sy + cr * cy;
            m.M23 = sr * cp;

            m.M31 = crsp * cy + sr * sy;
            m.M32 = crsp * sy - sr * cy;
            m.M33 = cr * cp;

            return m;
        }

        /// <summary>
        /// Creates a rotation matrix from a set of euler angles.
        /// </summary>
        /// <param name="angles">Vector containing the rotation angles about the x, y, z axes, in radians.</param>
        /// <returns>The rotation matrix</returns>
        public static Matrix3 FromEulerAnglesXYZ(Vector3 angles)
        {
            return Matrix3.FromEulerAnglesXYZ(angles.X, angles.Y, angles.Z);
        }

        /// <summary>
        /// Creates a rotation matrix for a rotation about the x-axis.
        /// </summary>
        /// <param name="radians">Rotation angle in radians.</param>
        /// <returns>The rotation matrix</returns>
        public static Matrix3 FromRotationX(float radians)
        {
            Matrix3 m = Identity;
            m.M22 = m.M33 = (float) Math.Cos(radians);
            m.M32 = (float) Math.Sin(radians);
            m.M23 = -m.M32;
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix for a rotation about the y-axis.
        /// </summary>
        /// <param name="radians">Rotation angle in radians.</param>
        /// <returns>The rotation matrix</returns>
        public static Matrix3 FromRotationY(float radians)
        {
            Matrix3 m = Identity;
            m.M11 = m.M33 = (float) Math.Cos(radians);
            m.M13 = (float) Math.Sin(radians);
            m.M31 = -m.M13;
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix for a rotation about the z-axis.
        /// </summary>
        /// <param name="radians">Rotation angle in radians.</param>
        /// <returns>The rotation matrix</returns>
        public static Matrix3 FromRotationZ(float radians)
        {
            Matrix3 m = Identity;
            m.M11 = m.M22 = (float) Math.Cos(radians);
            m.M21 = (float) Math.Sin(radians);
            m.M12 = -m.M21;
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix for a rotation about an arbitrary axis.
        /// </summary>
        /// <param name="radians">Rotation angle, in radians</param>
        /// <param name="axis">Rotation axis, which should be a normalized vector.</param>
        /// <returns>The rotation matrix</returns>
        public static Matrix3 FromAngleAxis(float radians, Vector3 axis)
        {
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;

            float sin = (float) Math.Sin(radians);
            float cos = (float) Math.Cos(radians);

            float xx = x * x;
            float yy = y * y;
            float zz = z * z;
            float xy = x * y;
            float xz = x * z;
            float yz = y * z;

            Matrix3 m = new Matrix3();
            m.M11 = xx + (cos * (1.0f - xx));
            m.M21 = (xy - (cos * xy)) + (sin * z);
            m.M31 = (xz - (cos * xz)) - (sin * y);

            m.M12 = (xy - (cos * xy)) - (sin * z);
            m.M22 = yy + (cos * (1.0f - yy));
            m.M32 = (yz - (cos * yz)) + (sin * x);

            m.M13 = (xz - (cos * xz)) + (sin * y);
            m.M23 = (yz - (cos * yz)) - (sin * x);
            m.M33 = zz + (cos * (1.0f - zz));

            return m;
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scaling">Scaling vector</param>
        /// <returns>The scaling vector</returns>
        public static Matrix3 FromScaling(Vector3 scaling)
        {
            Matrix3 m = Identity;
            m.M11 = scaling.X;
            m.M22 = scaling.Y;
            m.M33 = scaling.Z;
            return m;
        }

        /// <summary>
        /// Creates a rotation matrix that rotates a vector called "from" into another
        /// vector called "to". Based on an algorithm by Tomas Moller and John Hudges:
        /// <para>
        /// "Efficiently Building a Matrix to Rotate One Vector to Another"         
        /// Journal of Graphics Tools, 4(4):1-4, 1999
        /// </para>
        /// </summary>
        /// <param name="from">Starting vector</param>
        /// <param name="to">Ending vector</param>
        /// <returns>Rotation matrix to rotate from the start to end.</returns>
        public static Matrix3 FromToMatrix(Vector3 from, Vector3 to)
        {
            float e = Vector3.Dot(from, to);
            float f = (e < 0) ? -e : e;

            Matrix3 m = Identity;

            //"from" and "to" vectors almost parallel
            if(f > 1.0f - 0.00001f)
            {
                Vector3 u, v; //Temp variables
                Vector3 x; //Vector almost orthogonal to "from"

                x.X = (from.X > 0.0f) ? from.X : -from.X;
                x.Y = (from.Y > 0.0f) ? from.Y : -from.Y;
                x.Z = (from.Z > 0.0f) ? from.Z : -from.Z;

                if(x.X < x.Y)
                {
                    if(x.X < x.Z)
                    {
                        x.X = 1.0f;
                        x.Y = 0.0f;
                        x.Z = 0.0f;
                    }
                    else
                    {
                        x.X = 0.0f;
                        x.Y = 0.0f;
                        x.Z = 1.0f;
                    }
                }
                else
                {
                    if(x.Y < x.Z)
                    {
                        x.X = 0.0f;
                        x.Y = 1.0f;
                        x.Z = 0.0f;
                    }
                    else
                    {
                        x.X = 0.0f;
                        x.Y = 0.0f;
                        x.Z = 1.0f;
                    }
                }

                u.X = x.X - from.X;
                u.Y = x.Y - from.Y;
                u.Z = x.Z - from.Z;

                v.X = x.X - to.X;
                v.Y = x.Y - to.Y;
                v.Z = x.Z - to.Z;

                float M31 = 2.0f / Vector3.Dot(u, u);
                float M32 = 2.0f / Vector3.Dot(v, v);
                float M33 = M31 * M32 * Vector3.Dot(u, v);

                for(int i = 1; i < 4; i++)
                {
                    for(int j = 1; j < 4; j++)
                    {
                        //This is somewhat unreadable, but the indices for u, v vectors are "zero-based" while
                        //matrix indices are "one-based" always subtract by one to index those
                        m[i, j] = -M31 * u[i - 1] * u[j - 1] - M32 * v[i - 1] * v[j - 1] + M33 * v[i - 1] * u[j - 1];
                    }
                    m[i, i] += 1.0f;
                }

            }
            else
            {
                //Most common case, unless "from" = "to" or "from" =- "to"
                Vector3 v = Vector3.Cross(from, to);

                //Hand optimized version (9 mults less) by Gottfried Chen
                float h = 1.0f / (1.0f + e);
                float hvx = h * v.X;
                float hvz = h * v.Z;
                float hvxy = hvx * v.Y;
                float hvxz = hvx * v.Z;
                float hvyz = hvz * v.Y;

                m.M11 = e + hvx * v.X;
                m.M12 = hvxy - v.Z;
                m.M13 = hvxz + v.Y;

                m.M21 = hvxy + v.Z;
                m.M22 = e + h * v.Y * v.Y;
                m.M23 = hvyz - v.X;

                m.M31 = hvxz - v.Y;
                m.M32 = hvyz + v.X;
                m.M33 = e + hvz * v.Z;
            }

            return m;
        }

        /// <summary>
        /// Tests equality between two matrices.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>True if the matrices are equal, false otherwise</returns>
        public static bool operator ==(Matrix3 a, Matrix3 b)
        {
            return (((a.M11 == b.M11) && (a.M12 == b.M12) && (a.M13 == b.M13))
                && ((a.M21 == b.M21) && (a.M22 == b.M22) && (a.M23 == b.M23))
                && ((a.M31 == b.M31) && (a.M32 == b.M32) && (a.M33 == b.M33)));
        }

        /// <summary>
        /// Tests inequality between two matrices.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>True if the matrices are not equal, false otherwise</returns>
        public static bool operator !=(Matrix3 a, Matrix3 b)
        {
            return (((a.M11 != b.M11) || (a.M12 != b.M12) || (a.M13 != b.M13))
                || ((a.M21 != b.M21) || (a.M22 != b.M22) || (a.M23 != b.M23))
                || ((a.M31 != b.M31) || (a.M32 != b.M32) || (a.M33 != b.M33)));
        }


        /// <summary>
        /// Performs matrix multiplication.Multiplication order is B x A. That way, SRT concatenations
        /// are left to right.
        /// </summary>
        /// <param name="a">First matrix</param>
        /// <param name="b">Second matrix</param>
        /// <returns>Multiplied matrix</returns>
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(a.M11 * b.M11 + a.M21 * b.M12 + a.M31 * b.M13,
                                 a.M12 * b.M11 + a.M22 * b.M12 + a.M32 * b.M13,
                                 a.M13 * b.M11 + a.M23 * b.M12 + a.M33 * b.M13,
                                 a.M11 * b.M21 + a.M21 * b.M22 + a.M31 * b.M23,
                                 a.M12 * b.M21 + a.M22 * b.M22 + a.M32 * b.M23,
                                 a.M13 * b.M21 + a.M23 * b.M22 + a.M33 * b.M23,
                                 a.M11 * b.M31 + a.M21 * b.M32 + a.M31 * b.M33,
                                 a.M12 * b.M31 + a.M22 * b.M32 + a.M32 * b.M33,
                                 a.M13 * b.M31 + a.M23 * b.M32 + a.M33 * b.M33);
        }

        /// <summary>
        /// Implicit conversion from a 4x4 matrix to a 3x3 matrix.
        /// </summary>
        /// <param name="mat">4x4 matrix</param>
        /// <returns>3x3 matrix</returns>
        public static implicit operator Matrix3(Matrix4 mat)
        {
            return new Matrix3
            {
                Row0 = mat.Row0.Xyz,
                Row1 = mat.Row1.Xyz,
                Row2 = mat.Row2.Xyz
            };
        }

        
        public bool Equals(Matrix3 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Row0.Equals(other.Row0) && Row1.Equals(other.Row1) && Row2.Equals(other.Row2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix3)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row0, Row1, Row2);
        }
    }
}
