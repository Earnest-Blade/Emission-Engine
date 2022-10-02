using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Mathematics;

namespace Emission.IO
{
    public class Image : IDisposable
    {
        public int Width;
        public int Height;

        public Vector2 Size => (Width, Height);
        
        public string Path => _path;
        public byte[] Bytes => _bytes;
        
        private string _path;
        private byte[] _bytes;

        public Image(string path)
        {
            Bitmap map = new Bitmap(System.Drawing.Image.FromFile(path));
            Width = map.Width;
            Height = map.Height;
            _bytes = ImageToByteArray(map, map.RawFormat);

            map.Dispose();
        }
        
        public Image(string path, ImageFormat format)
        {
            Bitmap map = new Bitmap(System.Drawing.Image.FromFile(path));
            Width = map.Width;
            Height = map.Height;
            _bytes = ImageToByteArray(map, format);
            
            map.Dispose();
        }

        public void Dispose()
        {
            _bytes = null;
            _path = null;
            Width = 0;
            Height = 0;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image image, ImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }
    }
}
