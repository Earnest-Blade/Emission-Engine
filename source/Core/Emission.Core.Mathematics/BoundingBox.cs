using System.Runtime.InteropServices;

namespace Emission.Core.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct BoundingBox : IEquatable<BoundingBox>
    {
        public Vector3 Min;
        public Vector3 Max;

        public BoundingBox()
        {
            Min = Vector3.Zero;
            Max = Vector3.Zero;
        }

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public static bool operator ==(BoundingBox a, BoundingBox b) => a.Max == b.Max && a.Min == b.Min;
        public static bool operator !=(BoundingBox a, BoundingBox b) => a.Max != b.Max || a.Min != b.Min;

        public bool Equals(BoundingBox other)
        {
            return Min.Equals(other.Min) && Max.Equals(other.Max);
        }

        public override bool Equals(object? obj)
        {
            return obj is BoundingBox other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }
    }
}
