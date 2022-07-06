using System;
using OpenTK.Graphics.OpenGL;

namespace Emission
{
    public class Texture : IDisposable
    {
        public string Path { get => _path; }
        public string Name { get => _name; }
        public int ID { get => _texID; }

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

        private string _path;
        private string _name;
        private int _texID;
        private int _texUnit;
        private int _pixelFormat;

        public Texture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            _path = path;
            _name = name;
            _texUnit = (int)unit;
            _pixelFormat = (int)PixelFormat.Rgba;
        }

        public void Bind()
        {
            _texID = GraphicAllocator.BindTexture2D(_path, format: (PixelFormat)_pixelFormat);
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
    }
}