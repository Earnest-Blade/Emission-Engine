using Emission.Graphics;
using Emission.Mathematics.Numerics;

namespace Emission
{
    public interface ICamera
    {
        private static ICamera Main { get; set; }

        public Transform Transform { get; }

        public void Rotate(Vector3 rotation);
        public void Translate(Vector3 position);
        public void Move(Vector3 position, Vector3 rotation);

        public void Resize(Vector2 size);
        
        public static void SetMain(ICamera camera)
        {
            Main = camera;
        }

        public static ICamera GetMain() => Main;
        public static bool Exists() => Main != null;
    }
}
