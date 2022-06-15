using System;
using OpenTK.Mathematics;

namespace Emission.Math
{
    // Provides constants and static methods for common
    // mathematical methods
    static class Mathf
    {
        /// <summary>
        /// PI constant. Warped from <see cref="System.Math"/>.
        /// </summary>
        public const double Pi = System.Math.PI;
        
        // Pi Over constants
        public const float PiOver2 = (float)Pi / 2;
        public const float PiOver3 = (float)Pi / 3;
        public const float PiOver4 = (float)Pi / 4;
        
        /// <summary>
        /// Constant of bits in color. Use to convert RGB color to OpenGL color.
        /// </summary>
        public const int ColorBit = 255;

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
            float yScale = (1f / Tan(DegreesToRadian(fov / 2))) * aspect;
            float xScale = yScale / aspect;
            float frustrum = farPlane - nearPlane;

            Matrix4 matrix = new Matrix4
            {
                M11 = xScale,
                M22 = yScale,
                M33 = -((farPlane + nearPlane) / frustrum),
                M34 = -1f,
                M43 = -((2 * nearPlane * farPlane) / frustrum),
                M44 = 0
            };
            return matrix;
        }

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

        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float t)
        {
            return (1 - t) * value1 + t * value2;
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
    }
}
