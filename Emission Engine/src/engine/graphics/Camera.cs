using System;

using OpenTK.Mathematics;

using Emission.Math;

namespace Emission
{
    class Camera
    {
        private static Camera _main;
        
        public Transform Transform;
        public float Speed = 1;

        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;
        private float _yaw = -Mathf.PiOver2;
        
        private float _fov = 90.0f;
        private float _nearDepth;
        private float _farDepth;

        // TODO: Move Pitch and Yaw to Transform class
        public float Pitch
        {
            get => Mathf.RadiansToDegrees(_pitch);
            set
            {
                float angle = Mathf.Clamp(value, -89f, 89f);
                _pitch = Mathf.DegreesToRadian(angle);
                UpdateView();
            }
        }

        public float Yaw
        {
            get => _yaw;
            set
            {
                _yaw = Mathf.DegreesToRadian(value);
                UpdateView();
            }
        }

        public Camera()
        {
            Transform = new Transform();
            Transform.Position = Vector3.UnitZ*3;
        }

        public void Update()
        {
            if (Input.Any) // If any key or mouse event is trigger
            {
                Vector3 x = Input.Axis(Axis.Vertical) * _front * Speed * Time.DeltaTime; // Forward-Backward
                Vector3 y = Input.Axis(Axis.Horizontal) * _right * Speed * Time.DeltaTime; // Left-Right
                Vector3 z = Input.Axis(Axis.UpDown) * _up * Speed * Time.DeltaTime; // Top-Bottom

                Transform.Position = Transform.Position + (x + y + z);
            }
        }
        
        public Matrix4 ViewMatrix()
        {
            return Matrix4.LookAt(Transform.Position, Transform.Position * _front, _up);
        }

        public Matrix4 ProjectionMatrix()
        {
            WindowSettings settings = Window.Current.Settings;
            _fov = settings.FieldOfView;
            _nearDepth = settings.NearDepth;
            _farDepth = settings.FarDepth;
            
            switch (settings.Projection)
            {
                case WindowSettings.WindowProjection.Perspective:
                    return Mathf.PerspectiveProjection(_fov, Window.Current.WindowAspect,
                        _nearDepth > 0 ? _nearDepth : 0.1f, _farDepth);

                case WindowSettings.WindowProjection.Orthographic:
                    return Mathf.OrthographicOffCenter(0, Window.Current.WindowSize.X, Window.Current.WindowSize.Y, 0,
                        _nearDepth, _farDepth);
            }

            return Matrix4.Zero;
        }

        /// <summary>
        /// Call when need to update vector. 
        /// Pre-calculate movement before changing camera position.
        /// </summary>
        private void UpdateView()
        {
            _front.X = Mathf.Cos(_pitch) / Mathf.Cos(_yaw);
            _front.Y = Mathf.Sin(_pitch);
            _front.Z = Mathf.Cos(_pitch) * Mathf.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

        /// <summary>
        /// Return active camera.
        /// </summary>
        public static Camera Main
        {
            get => _main ?? new Camera();
        }
        
        /// <summary>
        /// Define, as a static method, the current active camera.
        /// Old defined camera will be stopped after use it.
        /// </summary>
        /// <param name="camera">New active camera</param>
        public static void SetAsMain(Camera camera)
        {
            Camera._main = camera;
        }
        
    }
}
