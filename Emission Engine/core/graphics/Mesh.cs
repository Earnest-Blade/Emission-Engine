using System;

namespace Emission
{
    public abstract class Mesh : IDisposable
    {
        protected int _vao;
        protected int _vbo;
        protected int _ebo;

        protected float[] _data;
        protected int[] _indices;

        public virtual void Initialize(float[] data, int[] indices)
        {
            _data = data;
            _indices = indices;
            _vao = Renderer.BindVertexArray();
            _vbo = Renderer.BindVertexBuffer(0, data);
            _ebo = Renderer.BindIndices(indices);
            
            Renderer.EnableVertexArray(0, Renderer.STRIDE, 0);
            Renderer.EnableVertexArray(1, Renderer.STRIDE, 3);
            Renderer.EnableVertexArray(2, Renderer.STRIDE, 5);
        }
        
        public abstract void Update();
        public abstract void Draw();

        public virtual void Dispose()
        {
            Renderer.Clear(_vao, _vbo, _ebo);
        }
    }
}
