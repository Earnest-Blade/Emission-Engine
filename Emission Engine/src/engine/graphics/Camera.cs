using System;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;

using Emission.Math;

namespace Emission
{
    unsafe class Camera
    {
        private static Camera _main;
        
        public Transform Transform;
        public float Speed = 1;

        public Vector3 Front => _front;
        public Vector3 Up => _up;
        public Vector3 Right => _right;
        
        public WindowSettings.GraphicProjectionMode ProjectionMode { get => _projectionMode; }

        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private Matrix4 _projection;

        private WindowSettings.GraphicProjectionMode _projectionMode;
        private float _fov = 90.0f;
        private float _nearDepth;
        private float _farDepth;

        public float Fov
        {
            get => _fov;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _fov = value;
                UpdateMatrix();
            }
        }

        public Camera(WindowSettings.GraphicProjectionMode graphicProjectionMode, float fov, float nearDepth, float farDepth)
        {
            Transform = new Transform();
            Transform.Position = Vector3.UnitZ*3;
            
            _projectionMode = graphicProjectionMode;
            _fov = fov;
            _nearDepth = nearDepth;
            _farDepth = farDepth;
            
            UpdateMatrix();
        }

        public void Update()
        {
            UpdateView();
            UpdateMatrix();
        }
        
        public Matrix4 ViewMatrix()
        {
            return Matrix4.LookAt(Transform.Position, Transform.Position * _front, _up);
        }

        public Matrix4 ProjectionMatrix()
        {
            return _projection;
        }

        /// <summary>
        /// Call when need to update vector. 
        /// Pre-calculate movement before changing camera position.
        /// </summary>
        private void UpdateView()
        {
            _front.X = Mathf.Cos(Mathf.DegreesToRadian(Transform.Pitch)) / Mathf.Cos(Mathf.DegreesToRadian(Transform.Yaw));
            _front.Y = Mathf.Sin(Mathf.DegreesToRadian(Transform.Pitch));
            _front.Z = Mathf.Cos(Mathf.DegreesToRadian(Transform.Pitch)) * Mathf.Sin(Mathf.DegreesToRadian(Transform.Yaw));

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

        /// <summary>
        /// Call when update or change projection matrix value.
        /// Re calculate by using <see cref="WindowSettings"/> class, get from current window.
        /// Also define all needed values.
        /// Use a switch to be fast.
        /// </summary>
        private void UpdateMatrix()
        {
            Window window = Window.Current;
            
            switch (ProjectionMode)
            {
                case WindowSettings.GraphicProjectionMode.Perspective:
                    _projection = Mathf.PerspectiveProjection(_fov, window.WindowAspect,
                        _nearDepth > 0 ? _nearDepth : 0.1f, _farDepth);
                    return;

                case WindowSettings.GraphicProjectionMode.Orthographic:
                    _projection = Mathf.OrthographicOffCenter(0, window.WindowSize.X, window.WindowSize.Y, 0,
                        _nearDepth, _farDepth);
                    return;
            }
        }

        /// <summary>
        /// Return active camera.
        /// </summary>
        public static Camera Main
        {
            get => _main;
        }

        /// <summary>
        /// Create a new camera using Window 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static Camera LoadFrom(WindowSettings settings)
        {
            return new Camera(settings.ProjectionMode, settings.FieldOfView, settings.NearDepth, settings.FieldOfView);
        }

        /// <summary>
        /// Define, as a static method, the current active camera.
        /// Old defined camera will be stopped after use it.
        /// </summary>
        /// <param name="camera">New active camera</param>
        public static void SetAsMain(Camera camera)
        {
            _main = camera;
        }
        
    }
}
