using System;
using System.Runtime.InteropServices;

namespace Emission.Core.Mathematics
{
    /// <summary>
    /// Defines a 3D ray with a point of origin and a direction.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Ray
    {
        /// <summary>
        /// Origin of the Ray in space.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// Direction of the Ray.
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// Constructs a new Ray.
        /// </summary>
        /// <param name="position">Origin of the ray.</param>
        /// <param name="direction">Direction of the ray.</param>
        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
