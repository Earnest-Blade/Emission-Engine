using Emission.GFX;

namespace Emission.IO
{
    /// <summary>
    /// Represent a 3D model.
    /// </summary>
    public struct Model
    {
        public Face[] Faces => _faces;
        
        private Face[] _faces;
        
        private int _pointer;
        private int _indicesCounts;
        
        public Model(int faceCount)
        {
            _faces = new Face[faceCount];
            _pointer = 0;
            _indicesCounts = 0;
        }

        /// <summary>
        /// Define a new face to the current model.
        /// Defined face is in the end of the pointer.
        /// </summary>
        /// <param name="vertices">Vertices array</param>
        /// <param name="indices">Element buffer array</param>
        public void AddFace(Vertex[] vertices, int[] indices)
        {
            _faces[_pointer] = new Face(FaceMode.Triangle, vertices, indices);
            
            _indicesCounts += vertices.Length;
            _pointer++;
        }
        
        /// <summary>
        /// Define a new face to the current model.
        /// Defined face is in the end of the pointer.
        /// </summary>
        /// <param name="vertices">Vertices array</param>
        /// <param name="indices">Element buffer array</param>
        public void AddFace(float[] vertices, int[] indices) => AddFace(Vertex.LoadArray(vertices), indices);
    }
}
