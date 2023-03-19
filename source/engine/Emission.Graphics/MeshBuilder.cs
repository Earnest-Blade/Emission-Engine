using Emission.Mathematics;

namespace Emission.Graphics
{
    public static class MeshBuilder
    {
        public static void AddVertex(ref Mesh mesh, float x, float y, float z) => AddVertex(ref mesh, new Vertex((x, y, z), Vector3.Zero, Vector2.Zero));
        public static void AddVertex(ref Mesh mesh, Vector3 position) => AddVertex(ref mesh, new Vertex(position, Vector3.Zero, Vector2.Zero));
        public static void AddVertex(ref Mesh mesh, Vector3 position, Vector3 normal) => AddVertex(ref mesh, new Vertex(position, normal, Vector2.Zero));
        public static void AddVertex(ref Mesh mesh, Vector3 position, Vector2 textureCoords) => AddVertex(ref mesh, new Vertex(position, Vector3.Zero, textureCoords));

        public static void AddVertex(ref Mesh mesh, Vertex vertex)
        {
            mesh.Vertices.Add(vertex);
            mesh.Indices.Add((uint)mesh.Vertices.Count - 1);
        }

        public static void AddVertexAt(ref Mesh mesh, float x, float y, float z, uint indice) => AddVertexAt(ref mesh, new Vertex((x, y, z), Vector3.Zero, Vector2.Zero), indice);
        public static void AddVertexAt(ref Mesh mesh, Vector3 position, uint indice) => AddVertexAt(ref mesh, new Vertex(position, Vector3.Zero, Vector2.Zero), indice);
        public static void AddVertexAt(ref Mesh mesh, Vector3 position, Vector3 normal, uint indice) => AddVertexAt(ref mesh, new Vertex(position, normal, Vector2.Zero), indice);
        public static void AddVertexAt(ref Mesh mesh, Vector3 position, Vector2 textureCoords, uint indice) => AddVertexAt(ref mesh, new Vertex(position, Vector3.Zero, textureCoords), indice);

        public static void AddVertexAt(ref Mesh mesh, Vertex vertex, uint indice)
        {
            mesh.Vertices.Add(vertex);
            mesh.Indices.Add(indice);
        }
    }
}
