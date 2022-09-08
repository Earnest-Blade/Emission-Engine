using System;

using OpenTK.Mathematics;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Emission.IO
{
    public class Image
    {
        public int Width;
        public int Height;
    
        public Vector2 Size
        {
            get => (Width, Height);
        }
    
        public Image<Rgba32> SharpImage => _image;
    
        public string Path => _path;
        public byte[] Bytes => _bytes;
        
        private string _path;
        private byte[] _bytes;
        
        private Image<Rgba32> _image;
    
        public Image(string path)
        {
            _path = path;
            
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(path);
            //image.Mutate(x => x.Flip(SixLabors.ImageSharp.Processing.FlipMode.Vertical));

            _image = image;
    
            Width = _image.Width;
            Height = _image.Height;
    
            _bytes = GetBytes();
        }
    
        private byte[] GetBytes()
        {
            var pixels = new byte[4 * Width * Height];
            
            long pointer = 0;
            for (int y = 0; y < Height; y++)
            {
                Span<Rgba32> row = SharpImage.GetPixelRowSpan(y);
                for (int x = 0; x < Width; x++)
                {
                    pixels[pointer] = row[x].R;
                    pixels[pointer + 1] = row[x].G;
                    pixels[pointer + 2] = row[x].B;
                    pixels[pointer + 3] = row[x].A;
                    pointer += 4;
                }
            }
    
            return pixels;
        }
    }
    
    /// <summary>
    /// Provides enumeration over how a image should be flipped.
    /// </summary>
    public enum FlipMode
    {
        /// <summary>
        /// Don't flip the image.
        /// </summary>
        None,

        /// <summary>
        /// Flip the image horizontally.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Flip the image vertically.
        /// </summary>
        Vertical,
    } 
}


