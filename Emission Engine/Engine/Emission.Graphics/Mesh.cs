using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Emission.Mathematics;
using Emission.Graphics.Shading;
using static Emission.Graphics.GL.GL;

namespace Emission.Graphics
{
    public unsafe struct Mesh : IDisposable
    {
        public List<Vertex> Vertices;
        public List<uint> Indices;
        public List<Texture> Textures;

        private uint _vao;
        private uint _vbo;
        private uint _ebo;
        
        public Mesh(List<Vertex> vertices, List<uint> indices, List<Texture> textures)
        {
            Vertices = vertices;
            Indices = indices;
            Textures = textures;

            _vao = 0;
            _vbo = 0;
            _ebo = 0;

            Initialize();
        }

        private void Initialize()
        {
            _vao = Renderer.BindVertexArray();
            _vbo = Renderer.BindStructBuffer(Vertices.ToArray());
            _ebo = Renderer.BindIndices(Indices.ToArray());
            
            glEnableVertexAttribArray(0);
            glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vertex), (void*)0);
            
            glEnableVertexAttribArray(1);
            glVertexAttribPointer(1, 3, GL_FLOAT, false, sizeof(Vertex), (void*)Marshal.OffsetOf(typeof(Vertex), "Normal"));
            
            glEnableVertexAttribArray(2);
            glVertexAttribPointer(2, 2, GL_FLOAT, false, sizeof(Vertex), (void*)Marshal.OffsetOf(typeof(Vertex), "TextureCoords"));

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);
        }
        
        public void Draw(Shader shader)
        {
            foreach (var t in Textures)
                t.Use();
            
            glBindVertexArray(_vao);
            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
            glEnableVertexAttribArray(2);

            glDrawElements(GL_TRIANGLES, Indices.Count, GL_UNSIGNED_INT, (void*)0);
            
            glDisableVertexAttribArray(0);
            glDisableVertexAttribArray(1);
            glDisableVertexAttribArray(2);
            glBindVertexArray(0);
        }

        public void Dispose()
        {
            Renderer.Clear(_vao, _vbo, _ebo);
        }
    }
}
