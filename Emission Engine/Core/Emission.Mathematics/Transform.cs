using Emission.Mathematics;
using Emission.Mathematics.Numerics;

namespace Emission
{
    public class Transform
    {
        public static readonly Transform Zero = new ();
        
        public Vector3 Position;
        public Vector3 Scale;

        public Quaternion Rotation;

        public Vector3 EulerAngle
        {
            get => Rotation.EulerAngles;
            set => Rotation.EulerAngles = value;
        }

        public Vector3 Forward => -Backward;
        public Vector3 Backward => Vector3.Normalize(Right);
        
        public Vector3 Up => new (0.0f, 1.0f, 0.0f);
        public Vector3 Down => new (0.0f, -1.0f, 0.0f);
        
        public Vector3 Right => new (1.0f, 0.0f, 0.0f);
        public Vector3 Left => new (-1.0f, 0.0f, 0.0f);

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
        }

        public Matrix4 ToMatrix()
        {
            Matrix4 matrix = Matrix4.Scale(Scale);
            matrix *= Matrix4.FromQuaternion(Rotation);
            matrix *= Matrix4.Translation(Position);
            return matrix;
        }
        
        public Matrix4 LookAt(Transform transform)
        {
            return Matrix4.LookAt(Position, transform.Position * new Vector3(-1, 1, 1), Vector3.UnitY);
        }
        
        public override string ToString() => $"[{Position}, {EulerAngle}]";

        public static Transform operator +(Transform t1, Transform t2)
            => new Transform(t1.Position + t2.Position, t1.Rotation + t1.Rotation, t1.Scale + t2.Scale);
        
        public static Transform operator -(Transform t1, Transform t2)
            => new Transform(t1.Position - t2.Position, t2.Rotation - t2.Rotation, t1.Scale - t2.Scale);
        
        public static Transform operator *(Transform t1, Transform t2) 
            => new Transform(t1.Position * t2.Position, t2.Rotation * t2.Rotation, t1.Scale * t2.Scale);
    }
}
