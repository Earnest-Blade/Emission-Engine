using System.Runtime.InteropServices;
using Emission.Core.Mathematics;

namespace Emission.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;

        public Vertex(Vector3 position, Vector3 normal, Vector2 textureCoords)
        {
            Position = position;
            Normal = normal;
            TextureCoords = textureCoords;
        }
    }
}
