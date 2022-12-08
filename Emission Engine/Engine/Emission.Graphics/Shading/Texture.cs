using System;

using Assimp;

using Emission.IO;
using Emission.Mathematics;
using static Emission.Graphics.GL.GL;

namespace Emission.Graphics.Shading
{
    public class Texture : IEquatable<Texture>, IDisposable
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
                _texUnit = (int)value;
            }
        }

        public Rectangle Size => Sprite.Size;

        private string _name;
        private TextureSlot _slot;
        private uint _texId;
        private int _texUnit;

        public Texture(TextureSlot slot, string path)
        {
            _slot = slot;
            Sprite = new Sprite(path);
        }

        public Texture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            _name = name;
            _texUnit = (int)unit;
            Sprite = new Sprite(path);
        }

        public Texture(Sprite sprite, string name, TextureUnit textureUnit)
        {
            Sprite = sprite;
            TextureUnit = textureUnit;
            _name = name;
        }

        /// <summary>
        /// Load texture data into OpenGl.
        /// </summary>
        public void Bind()
        {
            _texId = Renderer.BindTexture2D(Sprite.Bytes, Sprite.Width, Sprite.Height);
        }
        
        /// <summary>
        /// Make texture active for rendering.
        /// </summary>
        public void Use()
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
        }

        /// <summary>
        /// Make texture inactive for rendering.
        /// </summary>
        public void Close()
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
        }

        /// <summary>
        /// Delete texture from OpenGl.
        /// </summary>
        public void Dispose()
        {
            glDeleteTexture(_texId);
        }
        
        /// <summary>
        /// Define texture's wrap mode.
        /// </summary>
        /// <param name="wrap">OpenGl wrap mode.</param>
        public void TextureWrapping(int wrap)
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, wrap);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, wrap);
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        /// <summary>
        /// Define texture's border color.
        /// </summary>
        /// <param name="color">Color</param>
        public void TextureBorderColor(Vector4 color)
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, color.ToArray());
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        /// <summary>
        /// Define texture's filter.
        /// </summary>
        /// <param name="minFilter"></param>
        /// <param name="magFilter"></param>
        public void TextureFilter(int minFilter, int magFilter)
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, minFilter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, magFilter);
            glActiveTexture(_texUnit);
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