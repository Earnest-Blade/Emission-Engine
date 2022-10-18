using Emission;
using Emission.Mathematics.Numerics;

namespace Sandbox.Scripts
{
    public class FreeCameraController : IObjectBehaviour
    {
        public Transform Transform => _camera.Transform;
    
        private float _speed;
        private ICamera _camera;

        public FreeCameraController(ICamera camera, float speed)
        {
            _camera = camera;
            _speed = speed;
        }

        public void Update()
        {
            /*Vector3 rotation = new Vector3();
            if (Input.IsMouseButtonDown(MouseButton.Button2))
            {
                rotation.Y += Input.DeltaMousePosition.Y * Input.Sensivity * Time.DeltaTime;
                rotation.Z += Input.DeltaMousePosition.X * Input.Sensivity * Time.DeltaTime;
            }

            Vector3 move = new Vector3();
            move += Input.Axis(Axis.Horizontal) * Transform.Right * _speed * Time.DeltaTime; // Left-Right
            move += Input.Axis(Axis.UpDown) * Transform.Up * _speed * Time.DeltaTime; // Top-Bottom
            move += Input.Axis(Axis.Vertical) * Transform.Forward * _speed * Time.DeltaTime; // Forward-Backward
            
            _camera.Translate(move);*/
            ((PerspectiveCamera)_camera).RotateAround(new Transform(Vector3.Zero), 10);
        }
    }
}
