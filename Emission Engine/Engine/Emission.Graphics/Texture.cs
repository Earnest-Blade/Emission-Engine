using System;

using Assimp;

using Emission.IO;
using Emission.Mathematics;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe class Texture : IEquatable<Texture>, IDisposable
    {
        public string Path => Sprite.Path;
        public string Name => _name;

        public uint ID => _texId;
        public TextureSlot Slot => _slot;
        
        public Sprite Sprite { get; }

        public TextureUnit TextureUnit
        {
            get => (TextureUnit)_texUnit;
            set
            {
                _texUnit = (uint)value;
            }
        }

        public Rectangle Size => Sprite.Size;

        private string _name;
        private TextureSlot _slot;
        private uint _texId;
        private uint _texUnit;
        private bool _isBind;

        public Texture(TextureSlot slot, string path)
        {
            _slot = slot;
            _isBind = false;
            TextureUnit = TextureUnit.Texture0;
            Sprite = new Sprite(path);
        }

        public Texture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            _name = name;
            _isBind = false;
            Sprite = new Sprite(path);
            TextureUnit = unit;
            
            Bind();
        }

        public Texture(Sprite sprite, string name, TextureUnit textureUnit)
        {
            _name = name;
            _isBind = false;
            Sprite = sprite;
            TextureUnit = textureUnit;
            
            Bind();
        }

        /// <summary>
        /// Load texture data into OpenGl.
        /// </summary>
        public void Bind()
        {
            _texId = Renderer.BindTexture2D(Sprite.Bytes, Sprite.Width, Sprite.Height);
            _isBind = true;
        }
        
        /// <summary>
        /// Make texture active for rendering.
        /// </summary>
        public void Use()
        {
            if (!_isBind)
                throw new EmissionException(EmissionErrors.EmissionOpenGlException, 
                    "Trying to use a texture without binding it!");

            glActiveTexture((uint)_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
        }

        /// <summary>
        /// Make texture inactive for rendering.
        /// </summary>
        public void Close()
        {
            glActiveTexture((uint)_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
        }

        /// <summary>
        /// Delete texture from OpenGl.
        /// </summary>
        public void Dispose()
        {
            uint textureId = _texId;
            glDeleteTextures(1, &textureId);
        }
        
        /// <summary>
        /// Define texture's wrap mode.
        /// </summary>
        /// <param name="wrap">OpenGl wrap mode.</param>
        public void TextureWrapping(int wrap)
        {
            glActiveTexture((uint)_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, wrap);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, wrap);
            glActiveTexture(0);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        /// <summary>
        /// Define texture's border color.
        /// </summary>
        /// <param name="color">Color</param>
        public void TextureBorderColor(Vector4 color)
        {
            glActiveTexture((uint)_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            
            fixed(float* data = &color.ToArray()[0])
                glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, data);
            glActiveTexture(0);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        /// <summary>
        /// Define texture's filter.
        /// </summary>
        /// <param name="minFilter"></param>
        /// <param name="magFilter"></param>
        public void TextureFilter(int minFilter, int magFilter)
        {
            glActiveTexture((uint)_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, minFilter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, magFilter);
            glActiveTexture((uint)_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        public bool Equals(Texture other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _texId == other._texId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Texture)obj);
        }

        public override int GetHashCode()
        {
            return (int)_texId;
        }
    }
}