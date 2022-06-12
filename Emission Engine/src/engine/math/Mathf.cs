using System;

namespace Emission.Math
{
    static class Mathf
    {
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

        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }
    }
}
