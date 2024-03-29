﻿using System;
using System.Runtime.InteropServices;

namespace Emission.IO
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Icon
    {
        /// <summary>
        /// The height, in pixels, of this image.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The width, in pixels, of this image.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Pointer to the RGBA pixel data of this image, arranged left-to-right, top-to-bottom.
        /// </summary>
        public readonly IntPtr Pixels;

        /// <summary>
        /// Array of byte that represent the RGBA image.
        /// </summary>
        public readonly byte[] Data;

        public Icon(int width, int height, IntPtr pixels)
        {
            Width = width;
            Height = height;
            Pixels = pixels;
            Data = null;
        }

        public unsafe Icon(string path)
        {
            Image image = new Image(path);

            Width = image.Width;
            Height = image.Height;
            Data = image.Bytes;

            fixed (byte* ptr = &Data[0])
            {
                Pixels = new IntPtr(ptr);
            }
        }
    }
}
