using System;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe class ElementBufferObject
    {
        public readonly uint Id;
        public bool IsBind => _isBind;

        private bool _isBind;

        public ElementBufferObject()
        {
            uint id;
            glGenBuffers(1, &id);
            Id = id;
            
            _isBind = false;
        }

        public void Bind()
        {
            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, Id);
            _isBind = true;
        }
        
        public void PushData(int size, IntPtr data, uint usage) => PushData(size, data.ToPointer(), usage);
        public void PushData(int size, void* data, uint usage)
        {
            if (!_isBind)
                throw new EmissionException(EmissionException.ERR_GL_BUFFER, $"Element Buffer {Id} is not bind but you're trying to push data!");

            glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, Id);
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, new IntPtr(size), data, (int)usage);
            //glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        public void Delete()
        {
            uint id = Id;
            glDeleteBuffers(1, &id);
        }
        
        public static implicit operator uint(ElementBufferObject vao)
        {
            return vao.Id;
        }
    }
}
