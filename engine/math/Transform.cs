using System;

using OpenTK.Mathematics;

namespace Emission.Math
{
    public struct Transform
    {
        #region Statics Transforms
            public static Transform Zero = new (Vector3.Zero, Vector3.Zero, Vector3.One);
        #endregion
        
        public Vector3 Position;
        public Vector3 Scale;
        public bool IsVisible;

        public float FloatScale
        {
            set => Scale = new Vector3(value);
        }

        public float Roll
        {
            get => Mathf.RadiansToDegrees(_roll);
            set
            {
                _roll = Mathf.DegreesToRadian(value);
            }
        }
        
        public float Pitch
        {
            get => Mathf.RadiansToDegrees(_pitch);
            set
            {
                _pitch = Mathf.DegreesToRadian(value);
            }
        }
        
        public float Yaw
        {
            get => Mathf.RadiansToDegrees(_yaw);
            set
            {
                _yaw = Mathf.DegreesToRadian(value);
            }
        }
        
        public Quaternion Rotation
        {
            get => Quaternion.FromEulerAngles(_pitch, _yaw, _roll);
            set
            {
                value.ToEulerAngles(out Vector3 vec);
                _roll = vec.Z;
                _pitch = vec.X;
                _yaw = vec.Y;
            }
        }

        public Vector3 EulerAngle
        {
            get => new (Roll, Pitch, Yaw);
            set
            {
                Roll = value.X;
                Pitch = value.Y;
                Yaw = value.Z;
            }
        }

        public Vector3 Forward => -Backward;
        public Vector3 Backward => Vector3.Normalize(Matrix4.Invert(this).Column2.Xyz);
        
        public Vector3 Up => new (0.0f, 1.0f, 0.0f);
        public Vector3 Down => new (0.0f, -1.0f, 0.0f);
        
        public Vector3 Right => new (1.0f, 0.0f, 0.0f);
        public Vector3 Left => new (-1.0f, 0.0f, 0.0f);

        // private methods
        private float _alpha;

        private Quaternion _orientation;
        
        private float _pitch; // in radian
        private float _yaw; // in radian
        private float _roll; // in radian

        public Transform() : this(Vector3.Zero, Vector3.Zero, Vector3.One) {}
        public Transform(Vector3 position) : this(position, Vector3.Zero, Vector3.One) {}
        public Transform(Vector3 position, Vector3 eulerAngles) : this(position, eulerAngles, Vector3.One) {}
        public Transform(Vector3 position, Vector3 eulerAngles, float scale) : this(position, eulerAngles, new Vector3(scale)) {}

        public Transform(Vector3 position, Vector3 eulerAngles, Vector3 scale) : this(position, Quaternion.FromEulerAngles(eulerAngles), scale) {}
        public Transform(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            Position = position;
            Scale = scale;
            
            IsVisible = false;
            _alpha = 1;
            
            _orientation = Quaternion.Identity;
            _pitch = 0;
            _yaw = 0;
            _roll = 0;

            Rotation = quaternion;
        }

        public Matrix4 LookAt(Transform transform)
        {
            return Mathf.LookAt(Position, transform.Position * new Vector3(-1, 1, 1), Vector3.UnitY);
        }

        public override string ToString()
        {
            return $"[{Position}, {EulerAngle}]";
        }

        public static implicit operator Matrix4(Transform transform)
        {
            Matrix4 matrix = Matrix4.Identity;

            matrix *= Mathf.Translate(transform.Position);
            matrix *= Mathf.Scale(transform.Scale);

            matrix *= Matrix4.CreateFromQuaternion(transform.Rotation);
            
            return matrix;
        }
    }
}
