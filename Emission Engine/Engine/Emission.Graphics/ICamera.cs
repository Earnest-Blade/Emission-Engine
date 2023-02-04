using Emission;
using Emission.Mathematics;

namespace Emission.Graphics
{
    public interface ICamera
    {
        private static ICamera _current { get; set; }

        public Transform Transform { get; }
        public Matrix4 Projection { get; }
        public Matrix4 View { get; }

        public void Rotate(Vector3 rotation);
        public void Translate(Vector3 position);
        public void Move(Vector3 position, Vector3 rotation);

        public void LookAt(Transform transform);

        public void Resize(Vector2 size);
        
        public static void SetCurrentCamera(ICamera camera) => _current = camera;
        public static ICamera GetCurrent() => _current;
        public static bool Exists() => _current != null;
    }
}
