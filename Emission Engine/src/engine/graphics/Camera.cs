using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

using Emission.Math;

namespace Emission
{
    class Camera
    {
        public Transform Transform;
        public float Speed = 1.5f;

        public Matrix4 CameraView
        {
            get
            {
                return _viewMatrix;
            }
        }

        private Matrix4 _viewMatrix = Matrix4.Identity;

        public Camera()
        {
            Transform = new Transform();
        }

        public void Update()
        {
            int xAxis = Input.Axis(Axis.Horizontal);
            int yAxis = Input.Axis(Axis.UpDown);
            int zAxis = Input.Axis(Axis.Vertical);

            Transform.MoveFrom(xAxis * Speed, yAxis, zAxis);
            _viewMatrix = Matrix4.Invert(Transform.ToMatrix());
        }

        public static Camera Current
        {
            get => Application.Current.Camera;
        }
    }
}
