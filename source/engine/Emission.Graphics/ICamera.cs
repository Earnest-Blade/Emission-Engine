using Emission;
using Emission.Mathematics;

namespace Emission.Graphics
{
    public interface ICamera
    {
        private static ICamera? Current { get; set; }

        public Transform Transform { get; }
        public Matrix4 Projection { get; }
        public Matrix4 View { get; }

        public void Rotate(Vector3 rotation);
        public void Translate(Vector3 position);
        public void Move(Vector3 position, Vector3 rotation);

        public void LookAt(Transform transform);

        public void Resize(Vector2 size);
        
        public static void SetCurrentCamera(ICamera camera) => Current = camera;
        public static ICamera GetCurrent() => Current!;
        public static bool Exists() => Current != null;
    }
}
