using System;
using System.Collections.Generic;

using Emission.IO;
using Emission.Math;
using Emission.Shading;

using OpenTK.Graphics.OpenGL;

namespace Emission.GFX
{
    public class Mesh : IEngineRenderer, IDisposable
    {
        public string Infos => $"[[{string.Join(", ", VerticesArray)}]\n[{string.Join(", ", Indices)}]]";

        public Material Material { get; protected set; }

        public Vertex[] Vertices => _vertices;
        public int VerticesCount => _vertices.Length;
        public float[] VerticesArray => _verticesArray;

        public Face[] Faces => _faces.ToArray();
        public int FaceCount => _faces.Count;
        public int[] Indices => _indices;

        protected int _vaoID;
        protected int _vboID;
        protected int _eboID;

        protected Vertex[] _vertices;
        protected List<Face> _faces;

        protected float[] _verticesArray;
        protected int[] _indices;

        private int _subdivideCount;
        
        internal Mesh(){}

        public Mesh(Face[] faces) : this(new Material("material", "assets/internal/shader/default.glsl"), faces){}
        public Mesh(Model model) : this(new Material("material", "assets/internal/shader/default.glsl"), model){}
        
        public Mesh(Material material, Model model) : this(material, model.Faces) {}
        
        public Mesh(Material material, Face[] faces)
        {
            Material = material;

            _subdivideCount = 0;

            LoadGeometry(faces);
            
            Initialize();
        }

        /// <summary>
        /// Call after construct object, load VAOs, VBOs and EBOs to Ram and bind Textures.
        /// Also define a name to the mesh.
        /// </summary>
        public virtual void Initialize()
        {
            _vaoID = Renderer.BindVertexArray();
            _vboID = Renderer.BindVertexBuffer(0, VerticesArray);
            _eboID = Renderer.BindIndices(Indices);
            
            // enable normals
            Renderer.EnableVertexArray(2, Renderer.STRIDE, 5);
            
            Material.BindTextures();
            
            Debug.Log($"[INFO] New mesh successfully initialized!");
        }

        /// <summary>
        /// Call every frame, use to update state and variables of the object.
        /// </summary>
        public virtual void Update(ref Transform transform)
        {
            Material.Start();
            
            // Light
            Material.Update();

            // Transformation
            Material.Shader.UseUniformMat4("uTransform", transform);

            // Projection
            Material.Shader.UseUniformMat4("uView", Camera.Main.ViewMatrix);
            Material.Shader.UseUniformProjectionMat4("uProjection", Camera.Main.ProjectionMatrix);
            
            Material.Stop();
        }

        /// <summary>
        /// Bind and Start Shader and Material to getting ready for rendering object.
        /// </summary>
        public virtual void PreRender()
        {
            Material.Start();

            GL.BindVertexArray(_vaoID);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
                
            Material.UseTextures();
        }
        
        /// <summary>
        /// Call every frame, when object need to be render. Call Draw Element method in order to
        /// create element.
        /// Check if mesh is visible or not using transform.
        /// </summary>
        public virtual void Render()
        {
            PreRender();
            
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
                
            PostRender();
        }

        /// <summary>
        /// Call after transform.
        /// Disable Vertex Array 
        /// </summary>
        public virtual void PostRender()
        {
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.BindVertexArray(0);
            
            Material.Stop();
        }

        /// <summary>
        /// Destroy object and clear it.
        /// </summary> 
        public virtual void Dispose()
        {
            Material.Dispose();
            Renderer.Clear(_vaoID, _vboID, _eboID);
        }

        public void Subdivide()
        {
            _subdivideCount++;
            Face[] faces = new Face[_faces.Count * 4];
            for (int f = 0; f < _faces.Count; f++)
            {
                Face[] newFaces = _faces[f].Subdivide();
                faces[f * 4] = newFaces[0];
                faces[f * 4 + 1] = newFaces[1];
                faces[f * 4 + 2] = newFaces[2];
                faces[f * 4 + 3] = newFaces[3];
            }
            
            LoadGeometry(faces);
            UpdateBuffers();
        }

        public void Subdivide(int n)
        {
            for (int i = 0; i < n; i++) Subdivide();
        }

        public void LoadGeometry(Face[] faces)
        {
            _vertices = Vertex.LoadFace(faces);
            _verticesArray = Vertex.VerticesToArray(_vertices);
            _indices = Face.IndicesFromFaceArray(faces);
            _faces = new List<Face>(faces);

            Debug.Log("[INFO] New mesh geometry has been loaded!");
        }

        public void UpdateBuffers()
        {
            Renderer.WriteBuffer(_vboID, VerticesArray);
            Renderer.WriteIndices(_eboID, Indices);
        }
    }
}
