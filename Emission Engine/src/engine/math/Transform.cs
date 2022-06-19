using System;

using OpenTK.Mathematics;

namespace Emission.Math
{
    public class Transform
    {
        public Vector3 Position;
        public float Scale;

        public Vector3 Front => _front;
        public Vector3 Up => _up;
        public Vector3 Right => _right;

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
        
        public float Roll
        {
            get => Mathf.RadiansToDegrees(_roll);
            set
            {
                _roll = Mathf.DegreesToRadian(value);
            }
        }

        // private methods
        private Vector3 _front = -Vector3.UnitY;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;
        
        private float _pitch; // in radian
        private float _yaw; // in radian
        private float _roll; // in radian

        public Transform()
        {
            Position = Vector3.Zero;
            Scale = 1;
        }

        public void Move(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
        }

        public void MoveFrom(Vector3 v)
        {
            Position += v;
        }
        
        public void MoveFromCurrent(float x, float y, float z)
        {
            Position += new Vector3(x, y, z);
        }

        public void Rotate(float roll, float pitch, float yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }
        
        public void RotateFromCurrent(float roll, float pitch, float yaw)
        {
            Roll += roll;
            Pitch += pitch;
            Yaw += yaw;
        }

        public Matrix4 ToMatrix()
        {
            Matrix4 matrix = Matrix4.Identity;

            matrix *= Matrix4.CreateTranslation(Position);
            matrix *= Matrix4.CreateScale(Scale);
            
            matrix *= Matrix4.CreateRotationZ(_yaw);
            matrix *= Matrix4.CreateRotationY(_pitch);
            matrix *= Matrix4.CreateRotationX(_roll);
            
            return matrix;
        }

    }
}
