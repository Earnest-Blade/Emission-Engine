
namespace Emission.Core.Mathematics
{
    public static class MathHelper
    {
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        public const float PI = 3.1415926535897931f;

        /// <summary>
        /// Represents the smallest positive value that is greater than zero.
        /// </summary>
        public const float EPSILON = 1.401298E-45F;

        /// <summary>
        /// Convert a degrees angle into a radians angle.
        /// </summary>
        /// <param name="degrees">Value in degrees to convert</param>
        public static float DegreesToRadians(float degrees) => degrees * PI / 180;

        /// <summary>
        /// Convert a radians angle into a degrees angle.
        /// </summary>
        /// <param name="radians">Value in radians to convert</param>
        public static float RadiansToDegrees(float radians) => radians * 180 / PI;
    }
}
