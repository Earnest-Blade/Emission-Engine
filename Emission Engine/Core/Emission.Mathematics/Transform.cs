using Emission.Annotations;
using Newtonsoft.Json;
using System;

namespace Emission.Mathematics
{
    [Serializable]
    [PageSerializable]
    public class Transform : IEquatable<Transform>
    {
        public static Transform Zero => new Transform(Vector3.Zero, Vector3.Zero, 1);
        
        public Vector3 Position;
        public Vector3 Scale;

        [JsonIgnore]
        public Quaternion Rotation;

        public Vector3 EulerAngle
        {
            get => Rotation.ToEulerAngles().ToDegrees();
            set => Rotation = Quaternion.FromEulerAngles(value);
        }

        [JsonIgnore]
        public Vector3 Forward;
        [JsonIgnore]
        public Vector3 Backward;

        [JsonIgnore]
        public Vector3 Right;
        [JsonIgnore]
        public Vector3 Left;

        [JsonIgnore]
        public Vector3 Up;
        [JsonIgnore]
        public Vector3 Down;

        public Transform() : this(Vector3.Zero, Vector3.Zero, Vector3.One) {}
        public Transform(Vector3 position) : this(position, Vector3.Zero, Vector3.One) {}
        public Transform(Vector3 position, Vector3 eulerAngles) : this(position, eulerAngles, Vector3.One) {}
        public Transform(Vector3 position, Vector3 eulerAngles, float scale) : this(position, eulerAngles, new Vector3(scale)) {}

        public Transform(Vector3 position, Vector3 eulerAngles, Vector3 scale) : this(position, Quaternion.FromEulerAngles(eulerAngles), scale) {}
        public Transform(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            Position = position;
            Scale = scale;
            Rotation = Quaternion.Identity;
            
            Forward = Vector3.UnitZ;
            Backward = -Forward;
            
            Up = Vector3.UnitY;
            Down = -Up;
            
            Right = Vector3.UnitX;
            Left = -Right;
        }

        public Matrix4 ToMatrix()
        {
            Matrix4 matrix = Matrix4.Scale(Scale);

            matrix *= Matrix4.Translation(Position);
            matrix *= Matrix4.RotationQuaternion(Rotation);
            
            return matrix;
        }

        public Matrix4 LookAt(Transform transform) => LookAt(transform, Vector3.UnitY);
        public Matrix4 LookAt(Transform transform, Vector3 up)
        {
            return Matrix4.LookAt(Position, transform.Position * new Vector3(-1, 1, 1), up);
        }

        public static Transform operator +(Transform t1, Transform t2)
            => new Transform(t1.Position + t2.Position, t1.Rotation + t1.Rotation, t1.Scale + t2.Scale);
        
        public static Transform operator -(Transform t1, Transform t2)
            => new Transform(t1.Position - t2.Position, t2.Rotation - t2.Rotation, t1.Scale - t2.Scale);
        
        public static Transform operator *(Transform t1, Transform t2) 
            => new Transform(t1.Position * t2.Position, t2.Rotation * t2.Rotation, t1.Scale * t2.Scale);

        public static bool operator ==(Transform left, Transform right) => left!.Equals(right);
        public static bool operator !=(Transform left, Transform right) => !(left == right);

        public bool Equals(Transform other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Position.Equals(other.Position) && Scale.Equals(other.Scale) && Rotation.Equals(other.Rotation);
        }

        public override bool Equals(object obj)
        {
            return obj is Transform t && Equals(t);
        }
        
        public override string ToString() => $"[{Position}, {EulerAngle}]";

        public override int GetHashCode()
        {
            return Position.GetHashCode() + Rotation.GetHashCode() + Scale.GetHashCode();
        }
    }
}
