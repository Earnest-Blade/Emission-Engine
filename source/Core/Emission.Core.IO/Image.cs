/*using System.Runtime.InteropServices;
using StbImageSharp;

namespace Emission.Core.IO
{
    public static unsafe class Image
    {
        public static byte[] LoadImageFromPath(string? path, int* width, int* height) => LoadImageFromPath(path, width, height, ColorFormat.Default);
        public static byte[] LoadImageFromPath(string? path, int* width, int* height, ColorFormat colorFormat) => LoadImageFromPath(path, width, height, (int*)0, colorFormat);
        public static byte[] LoadImageFromPath(string? path, int* width, int* height, int* comp, ColorFormat colorFormat)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            
            if (!EFile.Exists(path))
                throw new FileNotFoundException(path);

            return LoadImageFromStream(new StreamReader(path).BaseStream, width, height, comp, colorFormat);
        }
        
        public static byte[] LoadImageFromMemory(MemoryStream? memoryStream, int* width, int* height) => LoadImageFromMemory(memoryStream, width, height, (int*)0, ColorFormat.Default);
        public static byte[] LoadImageFromMemory(MemoryStream? memoryStream, int* width, int* height, int* comp, ColorFormat colorFormat)
        {
            return LoadImageFromStream(memoryStream, width, height, comp, colorFormat);
        }
        
        public static byte[] LoadImageFromStream(Stream? stream, int* width, int* height, int* comp, ColorFormat colorFormat)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead || stream.Length == 0)
                throw new ArgumentException(nameof(stream));
            
            //stb_image.stb_image.StbiSetFlipVerticallyOnLoad(1);
            StbImage.stbi_set_flip_vertically_on_load(1);

            byte* ptr = StbImage.stbi__load_and_postprocess_8bit(new StbImage.stbi__context(stream), width, height, comp, (int)colorFormat);
            //stb_image.stb_image.StbiLoadFromMemory(MemoryHelper.BytePtrFromByteArray(streamBuffer), streamBuffer.Length, ref w, ref h, ref c, 0); //stb__load_and_postprocess_16bit(&context, width, height, comp, (int)colorFormat);

            if (ptr == (void*)0)
                throw new EmissionException(EmissionException.ERR_IMAGE, "Cannot load image!");

            int w = *width;
            int h = *height;
            int c = (int)colorFormat;
            if (comp == (int*)0)
            {
                if (colorFormat == ColorFormat.Default)
                {
                    Marshal.FreeHGlobal((IntPtr)ptr);
                    throw new EmissionException(EmissionException.ERR_IMAGE,
                        "Cannot load an image because Emission cannot found image's color format!");
                }
            }
            else c = *comp;
            
            byte[] buffer = new byte[w * h * c];
            
            Marshal.Copy((IntPtr)ptr, buffer, 0, buffer.Length);

            Marshal.FreeHGlobal((IntPtr)ptr);
            stream.Dispose();
            
            return buffer;
        }
    }
    
    public enum ColorFormat
    {
        Default = 0x0,
        Grey = 0x1,
        GretAlpha = 0x2,
        RedGreenBlue = 0x3,
        RedGreenBlueAlpha = 0x4
    }
}*/
