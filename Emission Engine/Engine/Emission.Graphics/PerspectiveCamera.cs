using System;

using Emission.Mathematics;
using Emission.Mathematics.Numerics;

namespace Emission
{
    public class PerspectiveCamera : ICamera, IDisposable
    {
        public const float MIN_DEPTH = 0.01f;
    
        public float Fov
        {
            get => _fov;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _fov = value;
                CalculateProjection();
            }
        }

        public float NearDepth
        {
            get => Viewport.NearDepth;
            set
            {
                if (value < MIN_DEPTH) throw new ArgumentOutOfRangeException(nameof(value));
                Viewport.NearDepth = value;
                CalculateProjection();
            }
        }
        
        public float FarDepth { 
            get => Viewport.FarDepth;
            set
            {
                if (value < MIN_DEPTH) throw new ArgumentOutOfRangeException(nameof(value));
                Viewport.FarDepth = value;
                CalculateProjection();
            }
        }
        
        public Transform Transform => _transform;
        public Viewport Viewport;
        
        public Matrix4 Projection => _projection;
        public Matrix4 View => _view;
        
        private Transform _transform;
        private Matrix4 _projection;
        private Matrix4 _view;

        private float _fov;

        public PerspectiveCamera(Window.Window window, float fov, float nearDepth, float farDepth) 
            : this(window.Viewport.Width, window.Viewport.Height, fov, nearDepth, farDepth) {}
        
        public PerspectiveCamera(float width, float height, float fov, float nearDepth, float farDepth)
            : this(new Viewport(0, 0, width, height, nearDepth, farDepth), fov) {}
        
        public PerspectiveCamera(Viewport viewport, float fov)
        {
            Viewport = viewport;
            _fov = fov;
            _transform = Transform.Zero;
            _transform.Scale = new Vector3(0.5f);
            
            Event.AddDelegate<Vector2>(Event.WindowResize, Resize);

            ICamera.SetMain(this);
            CalculateProjection();
            Move(Vector3.Zero, Vector3.Zero);
        }

        public void Rotate(Vector3 rotation) => Move(Vector3.Zero, rotation);
        public void Translate(Vector3 position) => Move(position, Vector3.Zero);
        public void Move(Vector3 position, Vector3 rotation)
        {
            Vector3 camRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, rotation));
            Vector3 camUp = Vector3.Cross(rotation, camRight);
        }

        public void RotateAround(Transform transform, float radius)
        {
            float x = MathF.Sin(Time.GlfwTime()) * radius;
            float z = MathF.Cos(Time.GlfwTime()) * radius;

            _view = Matrix4.LookAt((x, 0, z), Vector3.Zero, Vector3.UnitY);
        }
        
        public void Dispose()
        {
            Event.RemoveDelegate<Vector2>(Event.WindowResize, Resize);   
        }
        
        protected void CalculateProjection()
        {
            _projection = Matrix4.PerspectiveProjection(_fov, Viewport.Aspect, Viewport.NearDepth, Viewport.FarDepth);
        }

        public void Resize(Vector2 size)
        {
            Viewport.Width = size.X;
            Viewport.Height = size.Y;
            
            CalculateProjection();
        }
    }
}
