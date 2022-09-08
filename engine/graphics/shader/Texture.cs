using System;

using Emission.IO;
using Emission.GFX;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.Shading
{
    public class Texture : IDisposable
    {
        public string Path { get => _path; }
        public string Name { get => _name; }
        public int ID { get => _texID; }
        
        public Image Image { get; }

        public TextureUnit TextureUnit
        {
            get => (TextureUnit)_texUnit;
            set
            {
                _texUnit = (int)value;
            }
        }

        public PixelFormat PixelFormat
        {
            get => (PixelFormat)_pixelFormat;
            set
            {
                _pixelFormat = (int)value;
                Bind();
            }
        }

        public Vector2 Size
        {
            get => Image.Size;
        }

        private string _path;
        private string _name;
        private int _texID;
        private int _texUnit;
        private int _pixelFormat;
        
        public Texture(string name, Image image, TextureUnit unit = TextureUnit.Texture0) : this(image.Path, name, image, unit){}

        public Texture(string path, string name, Image data, TextureUnit unit = TextureUnit.Texture0)
        {
            Image = data;
            _path = path;
            _name = name;
            _texUnit = (int)unit;
            _pixelFormat = (int)PixelFormat.Rgba;
        }

        public Texture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            _path = path;
            _name = name;
            _texUnit = (int)unit;
            _pixelFormat = (int)PixelFormat.Rgba;
            Image = new Image(path);
        }

        public void Bind()
        {
            _texID = Renderer.BindTexture2D(Image.Bytes, Image.Width, Image.Height, format: (PixelFormat)_pixelFormat);
        }
        
        public void Use()
        {
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, _texID);
        }

        public void Dispose()
        {
            GL.DeleteTexture(_texID);
        }
        
        public void TextureWrapping(TextureWrapMode wrap)
        {
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, _texID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrap);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrap);
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void TextureBorderColor(Vector4 color)
        {
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, _texID);
            GL.TextureParameter((int)TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, new [] { color.X, color.Y, color.Z, color.W });
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void TextureFilter(TextureMinFilter minFilter, TextureMagFilter magFilter)
        {
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, _texID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.ActiveTexture((TextureUnit)_texUnit);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}