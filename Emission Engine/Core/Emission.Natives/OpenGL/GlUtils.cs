using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Emission.Natives.GL
{
    public static unsafe class GlUtils
    {
        public static string PtrToStringUTF8(byte* ptr) => PtrToStringUTF8(new IntPtr(ptr));
        public static string PtrToStringUTF8(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return "";
            
            int length = 0;
            while (Marshal.ReadByte(ptr, length) != 0)
                length++;

            byte[] buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }

        public static string PtrToStringUTF8(char* ptr)
        {
            return Marshal.PtrToStringUTF8(new IntPtr(ptr));
        }

        public static void StrToByteArrayPtr(string str, out byte** ptr, out byte[] buffer)
        {
            buffer = Encoding.UTF8.GetBytes(str);
            fixed (byte* a = &buffer[0])
            {
                byte*[] ptrs = { a };
                fixed (byte** b = &ptrs[0])
                {
                    ptr = b;
                }
            }
        }

        public static byte* StrToBytePtr(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            fixed (byte* b = &bytes[0])
                return b;
        }
    }
}
