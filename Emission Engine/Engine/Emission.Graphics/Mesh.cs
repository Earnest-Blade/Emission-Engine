using System;

namespace Emission
{
    public abstract class Mesh : IEquatable<Mesh>, IDisposable
    {
        protected uint _vao;
        protected uint _vbo;
        protected uint _ebo;

        protected float[] _data;
        protected int[] _indices;

        public virtual void Initialize(float[] data, int[] indices)
        {
            if (data == null || indices == null)
                throw new EmissionException(EmissionException.EmissionOpenGlException, "Data or Indices are null");
            
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

        public bool Equals(Mesh other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _vao == other._vao && _vbo == other._vbo && _ebo == other._ebo;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Mesh)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_vao, _vbo, _ebo);
        }
    }
}
