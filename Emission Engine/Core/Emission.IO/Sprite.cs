using System;

using Emission.Mathematics;

using StbImageSharp;

namespace Emission.IO
{
    public class Sprite : IDisposable
    {
        /// <summary>
        /// The height, in pixels, of this image.
        /// </summary>
        public int Width;
        
        /// <summary>
        /// The width, in pixels, of this image.
        /// </summary>
        public int Height;

        /// <summary>
        /// Create a rectangle from <see cref="Width"/> and <see cref="Height"/> of the sprite.
        /// </summary>
        public Rectangle Size => new Rectangle(0, 0, Width, Height);
        
        /// <summary>
        /// Path use to load the sprite.
        /// </summary>
        public string Path => _path;
        
        /// <summary>
        /// Byte array in RGBA that define the sprite.
        /// </summary>
        public byte[] Bytes => _bytes;
        
        private string _path;
        private byte[] _bytes;

        /* Constructor */
        public Sprite(string path)
        {
            byte[] buffer = GameFile.ReadAllBytes(path);
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
