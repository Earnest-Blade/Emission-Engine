using Emission.Mathematics;

namespace Emission.Graphics
{
    public class MeshBuilder
    {
        public static void AddVertex(ref Mesh mesh, float x, float y, float z)
        {
            mesh.Vertices.Add(new Vertex((x, y, z), Vector3.Zero, Vector2.Zero));
            mesh.Indices.Add((uint)mesh.Vertices.Count - 1);
        }
    }
}
