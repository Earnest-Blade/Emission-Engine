using System;

namespace Emission.Mathematics
{
    public class MathHelper
    {
        public const float ZeroTolerance = 1e-6f;
        public const float Pi = (float)Math.PI;
        public const float Epsilon = float.Epsilon;

        public static float DegreesToRadians(float degrees) => degrees * Pi / 180;
        public static float RadiansToDegrees(float radian) => radian * 180 / Pi;
    }
}
