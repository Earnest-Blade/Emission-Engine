using System;

using Emission.GFX;
using Emission.Math;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace Emission
{
    public class Camera
    {
        private static Camera _main;
        
        public Transform Transform;

        public WindowSettings.GraphicProjectionMode ProjectionMode { get => _projectionMode; }

        public float Fov
        {
            get => _fov;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _fov = value;
                UpdateProjectionMatrix();
            }
        }

        public float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                UpdateProjectionMatrix();
            }
        }

        public Vector2 Viewport
        {
            get => _viewport * Scale;
            set
            {
                _viewport = value;
                UpdateProjectionMatrix();
            }
        }

        public float AspectRatio
        {
            get => Viewport.X / Viewport.Y;
        }

        public Vector2 Depth
        {
            get => new Vector2(_nearDepth, _farDepth);
            set
            {
                _nearDepth = value.X;
                _farDepth = value.Y;
                UpdateProjectionMatrix();
            }
        }

        public Matrix4 ProjectionMatrix
        {
            get => _projection;
        }
        
        public Matrix4 ViewMatrix
        {
            get => _view;
        }

        public DrawMode DrawMode
        {
            get => _drawMode;
            set
            {
                _drawMode = value;
                GL.PolygonMode(MaterialFace.FrontAndBack, (PolygonMode)(int)value);
            }
        }
        
        private Matrix4 _projection;
        private Matrix4 _view;
        
        private DrawMode _drawMode;

        private WindowSettings.GraphicProjectionMode _projectionMode;
        private Vector2 _viewport;
        private float _scale;
        private float _fov = 90.0f;
        private float _nearDepth;
        private float _farDepth;

        private Vector2 _lastRotation;

        public Camera(WindowSettings.GraphicProjectionMode graphicProjectionMode, Vector2 viewport, float scale, float fov,
            float nearDepth, float farDepth)
        {
            Transform = new Transform();
            Transform.Position = Vector3.UnitZ*3;
            
            _projectionMode = graphicProjectionMode;
            _scale = scale;
            _viewport = viewport;
            _fov = fov;
            _nearDepth = nearDepth;
            _farDepth = farDepth;
            
            _lastRotation = Vector2.Zero;

            UpdateProjectionMatrix();
            Move(Vector3.Zero, Vector3.Zero);
        }

        public void Move(Vector3 position, Vector3 rotation)
        {
            var pitch = new Quaternion(new Vector3(-rotation.Y, 0, 0));
            var yaw = new Quaternion(new Vector3(0, -rotation.Z, 0));
            
            Transform.Rotation = Quaternion.Normalize(yaw * Transform.Rotation * pitch);
            Transform.Position += position;

            var rollDirection = Transform.Rotation * -Vector3.UnitZ;
            var pitchDirection = Transform.Rotation * -Vector3.UnitX;

            _view = Mathf.LookAt(Transform.Position, Transform.Position + rollDirection, Vector3.Cross(rollDirection, pitchDirection));
        }

        /// <summary>
        /// Call when update or change projection matrix value.
        /// Re calculate by using <see cref="WindowSettings"/> class, get from current window.
        /// Also define all needed values.
        /// Use a switch to be fast.
        /// </summary>
        private void UpdateProjectionMatrix()
        {
            switch (ProjectionMode)
            {
                case WindowSettings.GraphicProjectionMode.Perspective: 
                    _projection = Mathf.PerspectiveProjection(_fov, AspectRatio,
                        _nearDepth > 0 ? _nearDepth : 0.1f, _farDepth);
                    return;

                case WindowSettings.GraphicProjectionMode.Orthographic:
                    _projection = Mathf.OrthographicOffCenter(0, Viewport.X, Viewport.Y, 0,
                        _nearDepth, _farDepth);
                    return;
            }
        }

        /// <summary>
        /// Return active camera.
        /// </summary>
        public static Camera Main => _main;

        /// <summary>
        /// Create a new camera using Window Settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static Camera LoadFrom(WindowSettings settings)
        {
            return new Camera(settings.ProjectionMode, 
                new Vector2(settings.Width, settings.Height), 
                settings.Scale, 
                settings.FieldOfView, 
                settings.NearDepth, 
                settings.FarDepth);
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
