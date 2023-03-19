using System;
using Emission.Mathematics;

namespace Emission.Graphics.GeometricPrimitives
{
    public static partial class GeometricPrimitive
    {
        private const int CubeFaceCount = 6;

        private static readonly Vector3[] FaceNormals = new Vector3[CubeFaceCount]
        {
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
        };

        private static readonly Vector2[] TextureCoordinates = new Vector2[4]
        {
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
        };

        public static Model PrimitiveCube(Vector3 size) => PrimitiveCube(size, Array.Empty<Texture>());
        public static Model PrimitiveCube(Vector3 size, Texture texture) => PrimitiveCube(size, new[] { texture });
        public static Model PrimitiveCube(Vector3 size, Texture[] textures)
        {
            Vertex[] vertices = new Vertex[4 * CubeFaceCount];
            uint[] indices = new uint[6 * CubeFaceCount];

            Vector2[] textureCoords = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                textureCoords[i] = TextureCoordinates[i];
            }

            size /= 2;

            int vertexCount = 0, indexCount = 0;

            for (int i = 0; i < CubeFaceCount; i++)
            {
                Vector3 normal = FaceNormals[i];

                Vector3 basis = (i >= 4) ? Vector3.UnitZ : Vector3.UnitY;

                Vector3 side1 = Vector3.Cross(normal, basis);
                Vector3 side2 = Vector3.Cross(normal, side1);

                uint vbase = (uint)i * 4;
                indices[indexCount++] = (vbase + 0);
                indices[indexCount++] = (vbase + 1);
                indices[indexCount++] = (vbase + 2);

                indices[indexCount++] = (vbase + 0);
                indices[indexCount++] = (vbase + 2);
                indices[indexCount++] = (vbase + 3);

                vertices[vertexCount++] = new Vertex((normal - side1 - side2) * size, normal, textureCoords[0]);
                vertices[vertexCount++] = new Vertex((normal - side1 + side2) * size, normal, textureCoords[1]);
                vertices[vertexCount++] = new Vertex((normal + side1 + side2) * size, normal, textureCoords[2]);
                vertices[vertexCount++] = new Vertex((normal + side1 - side2) * size, normal, textureCoords[3]);
            }

            return ModelBuilder.FromMesh(new Mesh(vertices, indices, textures));
        }
    }
}
