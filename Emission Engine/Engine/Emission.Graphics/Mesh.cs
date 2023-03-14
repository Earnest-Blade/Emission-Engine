using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Emission.Mathematics;
using Emission.Graphics;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mesh : IEquatable<Mesh>, IDisposable
    {
        public List<Vertex> Vertices;
        public List<uint> Indices;
        public List<Texture> Textures;

        private VertexArrayBuffer _vao;
        private VertexBufferObject _vbo;
        private ElementBufferObject _ebo;

        public Mesh()
        {
            Vertices = new List<Vertex>();
            Indices = new List<uint>();
            Textures = new List<Texture>();
            
            _vao = null;
            _vbo = null;
            _ebo = null;
        }
        
        public Mesh(List<Vertex> vertices, List<uint> indices, List<Texture> textures)
        {
            Vertices = vertices;
            Indices = indices;
            Textures = textures;
            
            _vao = null;
            _vbo = null;
            _ebo = null;
            
            Initialize();
            Debug.Log($"[INFO] Successfully initialize mesh{_vao.Id}!");
        }

        public Mesh(Vertex[] vertices, uint[] indices, Texture[] textures)
        {
            Vertices = new List<Vertex>(vertices);
            Indices = new List<uint>(indices);
            Textures = new List<Texture>(textures);

            _vao = null;
            _vbo = null;
            _ebo = null;
            
            Initialize();
            Debug.Log($"[INFO] Successfully initialize mesh{_vao.Id}!");
        }

        private void Initialize()
        {
            _vao = Renderer.BindVertexArray();
            _vbo = Renderer.BindStructBuffer(Vertices.ToArray());
            _ebo = Renderer.BindIndices(Indices.ToArray());
            
            glBindVertexArray(_vao);
            glBindBuffer(GL_ARRAY_BUFFER, _vbo);
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ebo);
            
            glEnableVertexAttribArray(0);
            glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vertex), (void*)0);
            
            glEnableVertexAttribArray(1);
            glVertexAttribPointer(1, 3, GL_FLOAT, false, sizeof(Vertex), (void*)Marshal.OffsetOf(typeof(Vertex), "Normal"));
            
            glEnableVertexAttribArray(2);
            glVertexAttribPointer(2, 2, GL_FLOAT, false, sizeof(Vertex), (void*)Marshal.OffsetOf(typeof(Vertex), "TextureCoords"));

            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        public void UpdateGeometry()
        {
            glBindVertexArray(_vao);
            
            Renderer.WriteBuffer(_vbo, Vertices.ToArray());
            Renderer.WriteIndices(_ebo, Indices.ToArray());

            glBindVertexArray(0);
            
            Debug.Log($"[INFO] Successfully updated mesh{_vao.Id}!");
        }
        
        public void Draw()
        {
            foreach (var t in Textures)
                t.Use();
            
            glBindVertexArray(_vao);
            glBindBuffer(GL_ARRAY_BUFFER, _vbo);
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ebo);
            
            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
            glEnableVertexAttribArray(2);

            glDrawElements(GL_TRIANGLES, Indices.Count, GL_UNSIGNED_INT, NULL);
            
            glDisableVertexAttribArray(0);
            glDisableVertexAttribArray(1);
            glDisableVertexAttribArray(2);

            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }

        public void Dispose()
        {
            Renderer.Clear(_vao, _vbo, _ebo);
        }

        public bool Equals(Mesh other)
        {
            return _vao.Id.Equals(other._vao.Id) && _vbo.Id.Equals(other._vbo.Id) && _ebo.Id.Equals(other._ebo.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is Mesh other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_vao, _vbo, _ebo);
        }
    }
}
