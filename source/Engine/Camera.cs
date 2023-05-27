using Emission.Core;
using Emission.Graphics;
using Emission.Annotations;
using Emission.Engine.Page;
using Emission.Core.Mathematics;

namespace Emission.Engine
{
    [LonelyActor, DisableEngineBehaviorOnActor]
    public class Camera : Actor
    {
        private const float MinDepth = 0.01f;

        public float AspectRatio => (float)_width / _height;

        public Vector2 Size
        {
            get => new Vector2(_width, _height);
            set
            {
                if (value == Size) return;
                
                _width = (int)value.X;
                _height = (int)value.Y;
                    
                _shouldUpdateProjection = true;
            }
        }

        public float FieldOfView
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                if (value < 0 && value >= 90)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _shouldUpdateProjection = true;
                _fov = MathHelper.DegreesToRadians(value);
            }
        }

        public float NearClipPlane
        {
            get => _nearClipPlane;
            set
            {
                if (value < MinDepth && value > _farClipPlane)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _shouldUpdateProjection = true;
                _nearClipPlane = value;
            }
        }

        public float FarClipPlane
        {
            get => _farClipPlane;
            set
            {
                if (value < _nearClipPlane)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _shouldUpdateProjection = true;
                _farClipPlane = value;
            }
        }

        public CameraProjectionMode ProjectionMode
        {
            get => _projectionMode;
            set
            {
                _shouldUpdateProjection = true;
                _projectionMode = value;
            }
        }

        public override Vector3 Position
        {
            get => Transform.Position;
            set
            {
                if (value == Transform.Position) return;
                
                _shouldUpdateView = true;
                Transform.Position = value;
            }
        }

        public override Vector3 EulerAngles
        {
            get => Transform.EulerAngle;
            set
            {
                if (value == Transform.EulerAngle) return;

                _shouldUpdateView = true;
                Transform.EulerAngle = value;
            }
        }

        public override Quaternion Rotation
        {
            get => Transform.Rotation;
            set
            {
                if (value == Transform.Rotation) return;

                _shouldUpdateView = true;
                Transform.Rotation = value;
            }
        }

        public Vector3 Forward => Transform.Forward;
        public Vector3 Right => Transform.Right;
        public Vector3 Up => Transform.Up;

        public Matrix4 Projection => _projection;
        public Matrix4 View => _view;

        private float _fov; // in radian
        private int _width;
        private int _height;
        private float _nearClipPlane;
        private float _farClipPlane;

        private Matrix4 _projection;
        private Matrix4 _view;

        private CameraProjectionMode _projectionMode;
        
        private bool _shouldUpdateProjection;
        private bool _shouldUpdateView;

        private Camera(float fov, int width, int height, float nearClipPlane, float farClipPlane, CameraProjectionMode projectionMode)
        {
            _fov = fov;
            _width = width;
            _height = height;
            _nearClipPlane = nearClipPlane;
            _farClipPlane = farClipPlane;
            _projectionMode = projectionMode;

            _shouldUpdateProjection = true;
            _shouldUpdateView = true;

            Event.AddDelegate<Vector2>(Event.WINDOW_RESIZE, args => Size = args); 
            
            Debug.Log($"[INFO] A new {_projectionMode.ToString()} camera has been created!");
            if(PageManager.ActiveCamera == null) SetActive(true);
        }

        public override void Update()
        {
            base.Update();

            if (_shouldUpdateProjection)
            {
                // update projection
                _projection = _projectionMode switch
                {
                    CameraProjectionMode.Perspective => Matrix4.PerspectiveProjection(_fov, AspectRatio, _nearClipPlane, _farClipPlane),
                    CameraProjectionMode.Orthographic => Matrix4.Orthographic(_width, _height, _nearClipPlane, _farClipPlane),
                    _ => _projection
                };
                _shouldUpdateProjection = false;
            }

            if (_shouldUpdateView)
            {
                // update view
                Transform.Forward = Vector3.Negate(EulerAngles) * -Vector3.UnitZ;
                Transform.Up = Vector3.Negate(EulerAngles) * Vector3.UnitY;

                Vector3 euler = EulerAngles.ToRadians();
                Vector3 direction = new Vector3
                {
                    Z = MathF.Cos(euler.Z) * MathF.Cos(euler.Y),
                    Y = MathF.Sin(euler.Y),
                    X = MathF.Sin(euler.Z) * MathF.Sin(euler.Y)
                };
            
                _view = Matrix4.LookAt(Position, Position + Vector3.Normalize(direction), Vector3.UnitY);
                
                _shouldUpdateView = false;
            }
        }

        public sealed override void SetActive(bool value)
        {
            IsActive = value;
            
            Debug.Log("[INFO] Camera change!");
            ((Game)Application.Instance!).PageManager?.SetActiveCamera(value ? this : null);
        }

        public static Camera CreateOrthographicCamera(Viewport viewport, float nearClipPlane, float farClipPlane) => CreateOrthographicCamera((int)viewport.Width, (int)viewport.Height, nearClipPlane, farClipPlane);
        public static Camera CreateOrthographicCamera(int width, int height, float nearClipPlane, float farClipPlane)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height));
            
            if (nearClipPlane < MinDepth && nearClipPlane > farClipPlane)
                throw new ArgumentOutOfRangeException(nameof(nearClipPlane));
            
            if (farClipPlane < nearClipPlane)
                throw new ArgumentOutOfRangeException(nameof(farClipPlane));
            
            return new Camera(0.0f, width, height, nearClipPlane, farClipPlane, CameraProjectionMode.Orthographic);
        }

        public static Camera CreatePerspectiveCamera(float fov, Viewport viewport, float nearClipPlane, float farClipPlane) => CreatePerspectiveCamera(fov, (int)viewport.Width, (int)viewport.Height, nearClipPlane, farClipPlane);
        public static Camera CreatePerspectiveCamera(float fov, int width, int height, float nearClipPlane, float farClipPlane)
        {
            if (fov < 0 && fov > 90.0f)
                throw new ArgumentOutOfRangeException(nameof(fov));
            
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height));
            
            if (nearClipPlane < MinDepth && nearClipPlane > farClipPlane)
                throw new ArgumentOutOfRangeException(nameof(nearClipPlane));
            
            if (farClipPlane < nearClipPlane)
                throw new ArgumentOutOfRangeException(nameof(farClipPlane));

            return new Camera(MathHelper.DegreesToRadians(fov), width, height, nearClipPlane, farClipPlane, CameraProjectionMode.Perspective);
        }
    }
}
