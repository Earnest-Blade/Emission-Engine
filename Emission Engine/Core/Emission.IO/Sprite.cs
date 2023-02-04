using System;
using System.IO;
using System.Runtime.InteropServices;
using Emission.Mathematics;

using StbImageSharp;

namespace Emission.IO
{
    public unsafe class Sprite : IDisposable
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

        /// <summary>
        /// Define color channel of the sprite.
        /// </summary>
        public ColorComponents ColorComponents => (ColorComponents)_colorComponent;
        
        private string _path;
        private byte[] _bytes;
        private int _colorComponent;

        /* Constructor */
        public Sprite(string path) : this(path, ColorComponents.Default) { }
        public Sprite(string path, ColorComponents colorComponents)
        {
            _colorComponent = (int)colorComponents;
            
            StbImage.stbi_set_flip_vertically_on_load(1);
            
            int width, height, comp;
            
            Stream stream = GameFile.Read(path);
            StbImage.stbi__context ctx = new StbImage.stbi__context(stream);
            
            byte* ptr = StbImage.stbi__load_and_postprocess_8bit(ctx, &width, &height, &comp, _colorComponent);
            
            if (ptr == (byte*)0)
                throw new EmissionException(EmissionErrors.EmissionIOException, $"Cannot load sprite from '{path}'!");
            
            Width = width;
            Height = height;
            _bytes = new byte[width * height * comp];
            
            Marshal.Copy((IntPtr)ptr, _bytes, 0, _bytes.Length);
            Marshal.FreeHGlobal((IntPtr)ptr);
        }

        public void Dispose()
        {
            _bytes = null;
            _path = null;
            _colorComponent = (int)ColorComponents.Default;
            Width = 0;
            Height = 0;
        }
    }
}
