using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Emission.GFX
{
    public struct Face
    {
        public static readonly int[] TRIANGLES_INDICES = {0, 1, 2};
        public static readonly int[] QUAD_INDICES = {0, 1, 2, 2, 3, 0};
        
        public FaceMode Mode
        {
            get => (FaceMode)_verticesCount;
            set => _verticesCount = (int)value;
        }

        public int[] Indices { get; private set; }

        public int VerticesCounts => Vertices.Length;

        public float[] VerticesArray
        {
            get
            {
                var list = new List<float>();
                for (int i = 0; i < _verticesCount; i++) list.AddRange(Vertices[i].ToArray());

                return list.ToArray();
            }
        }

        public Vertex[] Vertices;

        public Vector3 Center
        {
            get
            {
                return Vertices.Length switch
                {
                    3 => (Vertices[0].Position + Vertices[1].Position + Vertices[2].Position) / 3,
                    4 => (Vertices[0].Position + Vertices[2].Position) * 0.5f,
                    _ => Vector3.Zero
                };
            }
        }
        
        public Vertex CenterVertex => new Vertex(Center, Vertices[0]);

        private int _verticesCount;

        public Face(FaceMode mode, Vertex[] vertices, int[] indices) : this()
        {
            _verticesCount = (int)mode;
            Vertices = vertices;
            Indices = indices;
        }

        public Face(FaceMode mode, float[] vertices, int[] indices)
        {
            _verticesCount = (int)mode;
            Vertices = Vertex.LoadArray(vertices);
            Indices = indices;
        }
        
        public Face(int count, Vertex[] vertices, int[] indices) : this((FaceMode)count, vertices, indices) {}

        public Face[] Subdivide()
        {
            // Only Work with 4 vertices
            if (Vertices.Length != 4) return new []{this};
            
            // Create edge vertices
            Vertex[] edgeVertices = new Vertex[Vertices.Length];
            for (int v = 0; v < Vertices.Length; v++)
            {
                if (v + 1 <= Vertices.Length - 1)
                    edgeVertices[v] = Vertex.Lerp(Vertices[v], Vertices[v + 1], 0.5f);
                else
                    edgeVertices[v] = Vertex.Lerp(Vertices[v], Vertices[0], 0.5f);
            }

            return new[]
            {
                new Face(FaceMode.Triangle, new[] { Vertices[0], edgeVertices[0], CenterVertex, edgeVertices[3] },
                    QUAD_INDICES),
                new Face(FaceMode.Triangle, new[] { edgeVertices[0], Vertices[1], edgeVertices[1], CenterVertex },
                    QUAD_INDICES),
                new Face(FaceMode.Triangle, new[] { CenterVertex, edgeVertices[1], Vertices[2], edgeVertices[2] },
                    QUAD_INDICES),
                new Face(FaceMode.Triangle, new[] { edgeVertices[3], CenterVertex, edgeVertices[2], Vertices[3] },
                    QUAD_INDICES),
            };
        }
        
        // TODO: Optimize this shit
        public static int[] IndicesFromFaceArray(Face[] faces)
        {
            int pointer = 0;
            var list = new List<int>();

            foreach (var face in faces)
            {
                List<int> ind = new List<int>(face.Indices);
                for (int i = 0; i < ind.Count; i++) ind[i] += pointer;
                list.AddRange(ind);
                
                pointer += face.VerticesCounts;
            }
            
            return list.ToArray();
        }
    }

    public enum FaceMode { Triangle=3, Quad=4 }
}