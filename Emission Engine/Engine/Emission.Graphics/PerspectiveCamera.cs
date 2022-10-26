using System;
using Emission;
using Emission.Mathematics;

namespace Emission.Graphics
{
    public sealed class PerspectiveCamera : ICamera, IDisposable
    {
        private const float MIN_DEPTH = 0.01f;
    
        public float Fov
        {
            get => _fov;
            set
            {
                if (value < 0 && value >= 90) throw new ArgumentOutOfRangeException(nameof(value));
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

        public PerspectiveCamera(global::Emission.Window.Window window, float fov, float nearDepth, float farDepth) 
            : this(window.Viewport.Width, window.Viewport.Height, fov, nearDepth, farDepth) {}
        
        public PerspectiveCamera(float width, float height, float fov, float nearDepth, float farDepth)
            : this(new Viewport(0, 0, width, height, nearDepth, farDepth), fov) {}
        
        public PerspectiveCamera(Viewport viewport, float fov)
        {
            Viewport = viewport;
            Fov = fov;
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
            Transform.Rotation += Quaternion.FromEulerAngles(rotation);
            Transform.Position += position;

            Vector3 euler = Transform.EulerAngle;

            Vector3 front = new Vector3()
            {
                X = MathF.Cos(MathHelper.DegreesToRadians(euler.Z)) * MathF.Cos(MathHelper.DegreesToRadians(euler.Y)),
                Y = MathF.Sin(MathHelper.DegreesToRadians(euler.Y)),
                Z = MathF.Sin(MathHelper.DegreesToRadians(euler.Z)) * MathF.Cos(MathHelper.DegreesToRadians(euler.Y))
            };
            
            Transform.Forward = Vector3.Normalize(front);
            Transform.Right = Vector3.Normalize(Vector3.Cross(Transform.Forward, Vector3.UnitY));
            Transform.Up = Vector3.Normalize(Vector3.Cross(Transform.Right, Transform.Forward));
            
            _view = Matrix4.LookAt(Transform.Position, Transform.Position + Transform.Forward, Transform.Up);

            //_view = Matrix4.One;
        }
        
        public void Resize(Vector2 size)
        {
            Viewport.Width = size.X;
            Viewport.Height = size.Y;
            
            CalculateProjection();
        }

        public void Dispose()
        {
            Event.RemoveDelegate<Vector2>(Event.WindowResize, Resize);   
        }
        
        private void CalculateProjection()
        {
            _projection = Matrix4.PerspectiveProjection(MathHelper.DegreesToRadians(_fov), Viewport.Aspect, Viewport.NearDepth, Viewport.FarDepth);
        }
    }
}
