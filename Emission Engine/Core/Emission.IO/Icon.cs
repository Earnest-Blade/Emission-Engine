using System;
using Emission.GLFW;
using StbImageSharp;

namespace Emission.IO
{
    public unsafe struct Icon
    {
        public int Width;
        public int Height;

        public byte[] Data;
        public IntPtr Pixels;

        public Icon(string path)
        {
            byte[] buff = File.ReadAllBytes(path);
            ImageResult image = ImageResult.FromMemory(buff, ColorComponents.RedGreenBlueAlpha);

            Width = image.Width;
            Height = image.Height;
            Data = image.Data;

            fixed (byte* ptr = &image.Data[0])
            {
                Pixels = new IntPtr(ptr);
            }
        }
    }
}
