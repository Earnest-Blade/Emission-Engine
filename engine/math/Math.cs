using System;
using OpenTK.Mathematics;

namespace Emission.Math
{
    /// <summary>
    /// Provides constants and static methods for common
    /// mathematical methods
    /// </summary>
    public static class Mathf
    {
        /// <summary>
        /// PI constant. Warped from <see cref="System.Math"/>.
        /// </summary>
        public const double Pi = System.Math.PI;
        
        // Pi Over constants
        public const float PiOver2 = (float)Pi / 2;
        public const float PiOver3 = (float)Pi / 3;
        public const float PiOver4 = (float)Pi / 4;

        public static Vector3 Front = new Vector3(0.0f, 0.0f, -1.0f);
        public static Vector3 Right = new Vector3(1.0f, 0.0f, 0.0f);
        public static Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);

        
        /// <summary>
        /// Constant of bits in color. Use to convert RGB color to OpenGL color.
        /// </summary>
        public const float ColorBit = 255.0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fov"></param>
        /// <param name="aspect"></param>
        /// <param name="nearPlane"></param>
        /// <param name="farPlane"></param>
        /// <returns></returns>
        public static Matrix4 PerspectiveProjection(float fov, float aspect, float nearPlane, float farPlane)
        {
            fov = Clamp(fov, 0.1f, 180f);
            float yScale = 1f / Tan(fov / 2);
            float frustrum = nearPlane - farPlane;
            Matrix4 matrix = new Matrix4()
            {
                M11 = yScale / aspect,
                M22 = yScale,
                M33 = (nearPlane + farPlane) / frustrum,
                M34 = (2 * nearPlane * farPlane) / frustrum,
                M43 = -1
            };
            return matrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <param name="depthNear"></param>
        /// <param name="depthFar"></param>
        /// <returns></returns>
        public static Matrix4 OrthographicOffCenter(float left, float right, float bottom, float top, float depthNear,
            float depthFar)
        {
            Matrix4 matrix = new Matrix4()
            {
                M11 = 2 / (right - left),
                M14 = -(right + left) / (right - left),
                M22 = 2 / (top - bottom),
                M24 = -(top + bottom) / (top - bottom),
                M33 = -2 / (depthFar - depthNear),
                M34 = (depthFar + depthNear) / (depthFar - depthNear),
                M44 = 1f
            };

            return matrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roll"></param>
        /// <param name="pitch"></param>
        /// <param name="yaw"></param>
        /// <returns></returns>
        public static Matrix4 RotationMatrix(float roll, float pitch, float yaw)
        {
            Matrix4 matrix = new Matrix4()
            {
                M11 = Cos(pitch) * Cos(yaw),
                M12 = Sin(roll) * Sin(pitch) * Cos(yaw) - Cos(roll) * Sin(yaw),
                M13 = Cos(roll) * Sin(pitch) * Cos(yaw) + Sin(roll) * Sin(yaw),
                M21 = Cos(pitch) * Sin(yaw),
                M22 = Sin(roll) * Sin(pitch) * Cos(yaw) + Cos(roll) * Sin(yaw),
                M23 = Cos(roll) * Sin(pitch) * Cos(yaw) - Sin(roll) * Sin(yaw),
                M31 = -Sin(pitch),
                M32 = Sin(roll) * Cos(pitch),
                M33 = Cos(roll) * Cos(pitch)
            };

            return matrix;
        }

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
                M11 = xAxis.X,
                M12 = yAxis.X,
                M13 = zAxis.X,
                M14 = 0,
                M21 = xAxis.Y,
                M22 = yAxis.Y,
                M23 = zAxis.Y,
                M24 = 0,
                M31 = xAxis.Z,
                M32 = yAxis.Z,
                M33 = zAxis.Z,
                M34 = 0,
                M41 = -xAxis.X * eye.X - xAxis.Y * eye.Y - xAxis.Z * eye.Z,
                M42 = -yAxis.X * eye.X - yAxis.Y * eye.Y - yAxis.Z * eye.Z,
                M43 = -zAxis.X * eye.X - zAxis.Y * eye.Y - zAxis.Z * eye.Z,
                M44 = 1
            };
            return matrix;
        }

        public static Matrix4 Translate(Vector3 position)
        {
            Matrix4 matrix = Matrix4.Identity;
            matrix.M41 = position.X;
            matrix.M42 = position.Y;
            matrix.M43 = position.Z;
            
            return matrix;
        }

        public static Matrix4 Scale(Vector3 size)
        {
            Matrix4 matrix4 = new Matrix4()
            {
                M11 = size.X,
                M22 = size.Y,
                M33 = size.Z,
                M44 = 1
            };

            return matrix4;
        }

        /// <summary>
        /// Create a linear interpolation between to points.
        /// </summary>
        /// <param name="value1">The first interpolated value</param>
        /// <param name="value2">The second interpolated value</param>
        /// <param name="t">Interpolation time on graph</param>
        /// <returns>Interpolated value</returns>
        public static float Lerp(float value1, float value2, float t)
        {
            return (1 - t) * value1 + t * value2;
        }

        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float t)
        {
            return (1 - t) * value1 + t * value2;
        }
        
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float t)
        {
            return (1 - t) * value1 + t * value2;
        }

        /// <summary>
        /// Calculate squared value of a number.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Square(float v)
        {
            return v * v;
        }

        /// <summary>
        /// Returns the value clamped to the inclusive range of min and max.
        /// </summary>
        /// <param name="value">Value to Clamp</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Clamped value</returns>
        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static Vector3 Clamp(Vector3 value, float min, float max)
        {
            value.Xzy = (Clamp(value.X, min, max), Clamp(value.Y, min, max), Clamp(value.Z, min, max));
            return value;
        }

        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            value.Xzy = (Clamp(value.X, min.X, max.X), Clamp(value.Y, min.Y, max.Y), Clamp(value.Z, min.Z, max.Z));
            return value;
        }
        
        /// <summary>
        /// Rounds a specified Decimal number to the closest integer toward negative infinity.
        /// </summary>
        /// <param name="x">The value to round</param>
        /// <returns>If d has a fractional part, the next whole Decimal number toward negative infinity that is less than d. -or-
        /// If d doesn't have a fractional part, d is returned unchanged. Note that the method returns an integral value of type
        /// </returns>
        public static decimal Floor(decimal x)
        {
            return decimal.Floor(x);
        }
        
        /// <summary>
        /// Rounds a specified Double number to the closest integer toward negative infinity.
        /// </summary>
        /// <param name="x">The value to round</param>
        /// <returns>If d has a fractional part, the next whole Double number toward negative infinity that is less than d. -or-
        /// If d doesn't have a fractional part, d is returned unchanged. Note that the method returns an integral value of type
        /// </returns>
        public static double Floor(double x)
        {
            return (double)decimal.Floor((decimal)x);
        }
        
        /// <summary>
        /// Return the bigger number of the parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float Max(float x, float y)
        {
            return (x >= y) ? x : y;
        }

        /// <summary>
        /// Return the bigger number of the parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double Max(double x, double y)
        {
            return (x >= y) ? x : y;
        }

        /// <summary>
        /// Return the bigger number of the parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int Max(int x, int y)
        {
            return (x >= y) ? x : y;
        }
        
        /// <summary>
        /// Return the bigger number of the parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static decimal Max(decimal x, decimal y)
        {
            return (x >= y) ? x : y;
        }

        /// <summary>
        /// Return the smaller number of the parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int Min(int x, int y)
        {
            return (x <= y) ? x : y;
        }

        /// <summary>
        /// Transform degrees value to radians.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float DegreesToRadian(float degrees)
        {
            return degrees * (float)Pi / 180;
        }

        /// <summary>
        /// Transform radians value to degrees.
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static float RadiansToDegrees(float radians)
        {
            return radians * 180 / (float)Pi;
        }
        
        /// <summary>
        /// Warper for <see cref="MathF.Cos"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>cosinus of parameter value</returns>
        public static float Cos(float x)
        {
            return MathF.Cos(x);
        }

        /// <summary>
        /// Warper for <see cref="MathF.Sin"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>sinus of parameter value</returns>
        public static float Sin(float x)
        {
            return MathF.Sin(x);
        }

        /// <summary>
        /// Warper for <see cref="MathF.Tan"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Tan of value</returns>
        public static float Tan(float x)
        {
            return MathF.Tan(x);
        }

        /// <summary>
        /// Invert of <see cref="Cos"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float ArcCos(float x)
        {
            return Cos(x) - 1;
        }

        /// <summary>
        /// Invert of <see cref="Sin"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float ArcSin(float x)
        {
            return Sin(x) - 1;
        }

        /// <summary>
        /// Invert of <see cref="Tan"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float ArcTan(float x)
        {
            return Tan(x) - 1;
        }
        
        /// <summary>
        /// Return 1 - Cos(x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float OneMinusCos(float x)
        {
            return 1 - Cos(x);
        }
    }
}
