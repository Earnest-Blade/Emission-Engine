using System;

using OpenTK.Mathematics;

namespace Emission.Math
{
    class Transform
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float Scale;

        public Transform()
        {
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
            Scale = 1;
        }

        public void Move(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
        }

        public void MoveFrom(float x, float y, float z)
        {
            Position += new Vector3(x, y, z);
        }

        /// <summary>
        /// Rotate transform using Euler's Angles.
        /// 
        /// </summary>
        /// <param name="pitch">Rotation avant-arriere</param>
        /// <param name="yaw">Rotation sur lui meme, gauche-droite</param>
        /// <param name="roll">Rotation en tourbillon</param>
        public void Rotate(float pitch, float yaw, float roll)
        {
            Rotation = Quaternion.FromEulerAngles(pitch, yaw, roll);
        }

        public Matrix4 ToMatrix()
        {
            Matrix4 matrix = Matrix4.Identity;

            matrix *= Matrix4.CreateTranslation(Position);
            matrix *= Matrix4.CreateScale(Scale);
            matrix *= Matrix4.CreateFromQuaternion(Rotation);
            
            return matrix;
        }

    }
}
