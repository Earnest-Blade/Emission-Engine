using System;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe class VertexBufferObject
    {
        public readonly uint Id;
        public bool IsBind => _isBind;

        private bool _isBind;

        public VertexBufferObject()
        {
            uint id;
            glGenBuffers(1, &id);
            Id = id; 

            _isBind = false;
        }

        public void Bind()
        {
            glBindBuffer(GL_ARRAY_BUFFER, Id);
            _isBind = true;
        }   

        public void PushData(int size, IntPtr data, uint usage) => PushData(size, data.ToPointer(), usage);
        public void PushData(int size, void* data, uint usage)
        {
            if (!_isBind)
                throw new EmissionException(EmissionErrors.EmissionOpenGlException, $"Vertex Buffer {Id} is not bind but you're trying to push data on it!");

            glBindBuffer(GL_ARRAY_BUFFER, Id);
            glBufferData(GL_ARRAY_BUFFER, new IntPtr(size), data, (int)usage); 
        }

        public void Delete()
        {
            uint id = Id;
            glDeleteBuffers(1, &id);
        }
        
        public static implicit operator uint(VertexBufferObject vao)
        {
            return vao.Id;
        }
    }
}
