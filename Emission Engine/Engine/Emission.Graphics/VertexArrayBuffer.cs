
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe class VertexArrayBuffer
    {
        public readonly uint Id;

        public VertexArrayBuffer()
        {
            uint id;
            glGenVertexArrays(1, &id);
            Id = id;
        }

        public void Bind()
        {
            glBindVertexArray(Id);
        }

        public void Delete()
        {
            uint id = Id;
            glDeleteVertexArrays(1, &id);
        }

        public static implicit operator uint(VertexArrayBuffer vao)
        {
            return vao.Id;
        }
    }
}
