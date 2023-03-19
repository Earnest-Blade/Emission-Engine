
namespace Emission.Mathematics
{
    public class MathHelper
    {
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        public const float Pi = 3.1415926535897931f;

        /// <summary>
        /// Represents the smallest positive value that is greater than zero.
        /// </summary>
        public const float Epsilon = 1.401298E-45F;

        /// <summary>
        /// The value for which all absolute numbers smaller than are considered equal to zero.
        /// </summary>
        public const float ZeroTolerance = 1e-6f;

        public static float DegreesToRadians(float degrees) => degrees * Pi / 180;
        public static float RadiansToDegrees(float radian) => radian * 180 / Pi;
    }
}
