using Emission;
using Emission.Math;
using OpenTK.Mathematics;

public class CameraController : IEngineBehaviour
{
    [System.Obsolete]
    public IEngineBehaviour Behaviour { get; }

    public Transform Transform => _camera.Transform;
    
    private float _speed;
    private Camera _camera;

    public CameraController(Camera camera, float speed)
    {
        _camera = camera;
        _speed = speed;
    }

    public void Initialize(){}
    public void Start(){}

    public void Update()
    {
        Vector3 rotation = new Vector3();
        if (Input.IsMouseButtonDown(MouseButton.Button2))
        {
            rotation.Y += Input.DeltaMousePosition.Y * Input.Sensivity * Time.DeltaTime;
            rotation.Z += Input.DeltaMousePosition.X * Input.Sensivity * Time.DeltaTime;
        }

        var x = Input.Axis(Axis.Horizontal) * Transform.Right * _speed * Time.DeltaTime; // Left-Right
        var y = Input.Axis(Axis.UpDown) * Transform.Up * _speed * Time.DeltaTime; // Top-Bottom
        var z = Input.Axis(Axis.Vertical) * Transform.Forward * _speed * Time.DeltaTime; // Forward-Backward
        
        _camera.Move(x + y + z, rotation);
    }

    public void PreRender() {}
    public void Render() {}
    public void PostRender() {}
    public void Dispose(){}
}