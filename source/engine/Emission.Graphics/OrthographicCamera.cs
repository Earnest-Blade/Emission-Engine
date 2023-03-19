using Emission.Mathematics;
using Emission.Window;
using System;

namespace Emission.Graphics
{
    public class OrthographicCamera : ICamera, IDisposable
    {
        private const float MIN_DEPTH = 0.01f;

        public float NearDepth
        {
            get => Viewport.NearDepth;
            set
            {
                if (value < MIN_DEPTH) throw new ArgumentOutOfRangeException(nameof(value));
                Viewport.NearDepth = value;
                UpdateProjection();
            }
        }

        public float FarDepth
        {
            get => Viewport.FarDepth;
            set
            {
                if (value < MIN_DEPTH) throw new ArgumentOutOfRangeException(nameof(value));
                Viewport.FarDepth = value;
                UpdateProjection();
            }
        }

        public Transform Transform => _transform;
        public Viewport Viewport;

        public Matrix4 Projection => _projection;
        public Matrix4 View => _view;

        private Transform _transform;
        private Matrix4 _projection;
        private Matrix4 _view;

        public OrthographicCamera(Window.Window window, float nearDepth, float farDepth)
            : this(window.Viewport.Width, window.Viewport.Height, nearDepth, farDepth) { }

        public OrthographicCamera(float width, float height, float nearDepth, float farDepth)
            : this(new Viewport(width, height, nearDepth, farDepth)) { }

        public OrthographicCamera(Viewport viewport)
        {
            Viewport = viewport;
            _transform = Transform.Zero;

            Event.AddDelegate<Vector2>(Event.WINDOW_RESIZE, Resize);

            ICamera.SetCurrentCamera(this);
            UpdateProjection();
            Move(Vector3.Zero, Vector3.Zero);
        }

        public void Rotate(Vector3 rotation) => Move(Vector3.Zero, rotation);
        public void Translate(Vector3 position) => Move(position, Vector3.Zero);
        public void Move(Vector3 position, Vector3 rotation)
        {
            if (rotation != Vector3.Zero)
            {
                Transform.Rotation += Quaternion.FromAxis(Vector3.UnitX, rotation.X);
                Transform.Rotation += Quaternion.FromAxis(Vector3.UnitY, rotation.Y);
                Transform.Rotation += Quaternion.FromAxis(Vector3.UnitZ, rotation.Z);
            }

            Transform.Position += position;

            Vector3 euler = Transform.Rotation.ToEulerAngles();
            Vector3 direction = new Vector3
            {
                Z = MathF.Cos(euler.Z) * MathF.Cos(euler.Y),
                Y = MathF.Sin(euler.Y),
                X = MathF.Sin(euler.Z) * MathF.Sin(euler.Y)
            };

            _view = Matrix4.LookAt(Transform.Position, Transform.Position + Vector3.Normalize(direction), Vector3.UnitY);
        }

        public void LookAt(Transform transform)
        {
            _view = Matrix4.LookAt(Transform.Position, transform.Position, Vector3.UnitY);
        }

        public void Resize(Vector2 size)
        {
            Viewport.Size = size;

            UpdateProjection();
        }

        public void Dispose()
        {
            Event.RemoveDelegate<Vector2>(Event.WINDOW_RESIZE, Resize);
        }

        private void UpdateProjection()
        {
            _projection = Matrix4.Orthographic(Viewport.Width, Viewport.Height, Viewport.NearDepth, Viewport.FarDepth);
        }
    }
}