using Emission.Core.Mathematics;

namespace Emission.Graphics
{
    public struct MeshData
    {
        public static MeshData Empty => new MeshData();
        
        public readonly string Name;
        
        public readonly Vertex[] Vertices;
        public readonly uint[] Indices;
        public readonly Texture[] Textures;

        public readonly Bone[] Bones;
        public readonly PrimitiveType PrimitiveType;
        public readonly BoundingBox BoundingBox;

        public MeshData()
        {
            Name = string.Empty;
            Vertices = Array.Empty<Vertex>();
            Indices = Array.Empty<uint>();
            Textures = Array.Empty<Texture>();
            Bones = Array.Empty<Bone>();
            PrimitiveType = PrimitiveType.Triangle;
            BoundingBox = new BoundingBox();
        }

        public MeshData(string name, Vertex[] vertices, uint[] indices, Texture[] textures, Bone[] bones, PrimitiveType primitiveType, BoundingBox boundingBox)
        {
            Name = name;
            Vertices = vertices;
            Indices = indices;
            Textures = textures;
            Bones = bones;
            PrimitiveType = primitiveType;
            BoundingBox = boundingBox;
        }
        
        public MeshData(string name, Vertex[] vertices, uint[] indices, Texture[] textures, Bone[] bones, Assimp.PrimitiveType primitiveType, Assimp.BoundingBox boundingBox)
        {
            Name = name;
            Vertices = vertices;
            Indices = indices;
            Textures = textures;
            Bones = bones;
            PrimitiveType = primitiveType switch
            {
                Assimp.PrimitiveType.Line => PrimitiveType.Line,
                Assimp.PrimitiveType.Point => PrimitiveType.Point,
                Assimp.PrimitiveType.Triangle => PrimitiveType.Triangle,
                Assimp.PrimitiveType.Polygon => PrimitiveType.Quads,
                _ => PrimitiveType.Triangle
            };
            BoundingBox = new BoundingBox(new Vector3(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z),
                                          new Vector3(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Max.Z));
        }
    }
}
