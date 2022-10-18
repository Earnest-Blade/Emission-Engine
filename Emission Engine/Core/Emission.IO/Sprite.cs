using System;

using Emission.Mathematics.Numerics;

using StbImageSharp;

namespace Emission.IO
{
    public class Sprite : IDisposable
    {
        public int Width;
        public int Height;

        public Vector2 Size => (Width, Height);
        
        public string Path => _path;
        public byte[] Bytes => _bytes;
        
        private string _path;
        private byte[] _bytes;

        public Sprite(string path)
        {
            byte[] buffer = File.ReadAllBytes(path);
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromMemory(buffer, ColorComponents.RedGreenBlueAlpha);
            
            Width = image.Width;
            Height = image.Height;
            _bytes = image.Data;
        }

        public void Dispose()
        {
            _bytes = null;
            _path = null;
            Width = 0;
            Height = 0;
        }

    }
}
