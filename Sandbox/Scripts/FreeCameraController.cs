using Emission;
using Emission.Mathematics;
using Emission.Graphics;

namespace Sandbox.Scripts
{
    public class FreeCameraController : IEngineBehaviour
    {
        public Transform Transform => _camera.Transform;
    
        public IEngineBehaviour Behaviour => this;
        public bool IsActive { get; set; }

        private float _speed;
        private ICamera _camera;

        public FreeCameraController(ICamera camera, float speed)
        {
            _camera = camera;
            _speed = speed;
        }
        
        public void Initialize() { }
        public void Start() { }

        public void Update()
        {
            Vector3 rotation = new Vector3();
            if (Input.IsMouseButtonDown(MouseButton.Button2))
            {
                rotation.Y = Input.DeltaMousePosition.Y * Time.DeltaTime;
                rotation.Z = Input.DeltaMousePosition.X * Time.DeltaTime;
            }

            Vector3 move = new Vector3();
            move += Input.Axis(Axis.Horizontal) * Transform.Right * _speed * Time.DeltaTime; // Left-Right
            move += Input.Axis(Axis.UpDown) * Transform.Up * _speed * Time.DeltaTime; // Top-Bottom
            move += Input.Axis(Axis.Vertical) * Transform.Forward * _speed * Time.DeltaTime; // Forward-Backward

            _camera.Move(move, rotation);
        }

        public void Render() { }
        public void Stop() { }
    }
}
