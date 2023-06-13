using System.Text;
using System.Runtime.InteropServices;

namespace Emission.Core.Memory
{
    public unsafe partial class Memory
    {
        /// <summary>
        /// Return a string from an unmanaged pointer.
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="encoding">Encoder use to decode the string.</param>
        public static unsafe string? PtrToString(void* ptr, Encoding encoding)
        {
            if (ptr == null) return null;

            int length = 0;
            while (Marshal.ReadByte((IntPtr)ptr, length) != 0)
                length++;

            byte[] buffer = new byte[length];
            Marshal.Copy((IntPtr)ptr, buffer, 0, length);
            return encoding.GetString(buffer);
        }

        /// <summary>
        /// Return a string from an unmanaged pointer using a length.
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="len"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static unsafe string? PtrToString(void* ptr, int len, Encoding encoding)
        {
            if (len <= 0)
                throw new ArgumentOutOfRangeException(nameof(len));

            byte* buffer = (byte*)CRuntime.Malloc(len);
            CRuntime.Memcpy(buffer, ptr, len);
            string s = encoding.GetString(buffer, len);
            CRuntime.Free(buffer);
            return s;
        }

        /// <summary>
        /// Return a string formatted as UTF-8 from an unmanaged pointer.
        /// </summary>
        public static unsafe string? PtrToStringUtf8(void* ptr) => PtrToString(ptr, Encoding.UTF8);

        /// <summary>
        /// Return a string formatted as UTF-8 from an unmanaged pointer.
        /// </summary>
        public static unsafe string? PtrToStringUtf8(char* ptr) => PtrToString(ptr, Encoding.UTF8);

        public static unsafe string? PtrToStringUtf8(byte* ptr) => PtrToString(ptr, Encoding.UTF8);

        /// <summary>
        /// Return a string formatted as UTF-8 from an unmanaged pointer.
        /// </summary>
        public static unsafe string? PtrToStringUtf8(void* ptr, int lenght) => PtrToString(ptr, lenght, Encoding.UTF8);

        /// <summary>
        /// Return a string formatted as UTF-8 from an unmanaged pointer.
        /// </summary>
        public static unsafe string? PtrToStringUtf8(char* ptr, int lenght) => PtrToString(ptr, lenght, Encoding.UTF8);

        /// <summary>
        /// Return a byte pointer from a string.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        /// <param name="length">Pointer use to get byte array size</param>
        /// <param name="encoding">Encoding of the text</param>
        public static unsafe byte** StrToByteArrayPtr(string str, int* length, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str)) return (byte**)0;
            byte[] buffer = encoding.GetBytes(str);

            if (length != (int*)0)
                *length = buffer.Length;

            fixed (byte* bufferPtr = &buffer[0])
            {
                byte*[] bytePtrArray = { bufferPtr };
                fixed (byte** b = &bytePtrArray[0])
                    return b;
            }
        }

        /// <summary>
        /// Return a byte pointer from a string.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        /// <param name="encoding">Encoding of the text</param>
        public static unsafe byte** StrToByteArrayPtr(string str, Encoding encoding) =>
            StrToByteArrayPtr(str, (int*)0, encoding);

        /// <summary>
        /// Return a byte pointer from a string formatted as UTF-8.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        public static unsafe byte** StrUtf8ToByteArrayPtr(string str) => StrToByteArrayPtr(str, Encoding.UTF8);

        /// <summary>
        /// Return a byte pointer from a string formatted as UTF-8.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        /// <param name="length">Pointer use to get byte array size</param>
        public static unsafe byte** StrUtf8ToByteArrayPtr(string str, int* length) =>
            StrToByteArrayPtr(str, length, Encoding.UTF8);

        /// <summary>
        /// Return a byte pointer from a string.
        /// </summary>
        /// <param name="str">String to transform to a char pointer</param>
        /// <param name="length">Pointer use to get char array length</param>
        public static unsafe char** StrToCharArrayPtr(string str, int* length)
        {
            if (string.IsNullOrEmpty(str)) return (char**)0;
            char[] buffer = str.ToCharArray();

            if (length != (int*)0)
                *length = buffer.Length;

            fixed (char* bufferPtr = &buffer[0])
            {
                char*[] charPtrArray = { bufferPtr };
                fixed (char** c = &charPtrArray[0])
                    return c;
            }
        }

        /// <summary>
        /// Return a byte pointer from a string.
        /// </summary>
        /// <param name="str">String to transform to a char pointer</param>
        public static unsafe char** StrToCharArrayPtr(string str) => StrToCharArrayPtr(str, (int*)0);

        public static unsafe string? BytePtrToStr(byte** ptr, int length)
        {
            if (ptr == (byte**)0)
                throw new ArgumentNullException(nameof(ptr));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            byte*[] p1 = { *ptr };
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = p1[0][1];
            }

            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Create a char array from a char pointer array.
        /// </summary>
        /// <param name="ptr">Pointer to the char array pointer</param>
        /// <param name="length">Length of the string</param>
        public static unsafe char[] CharArrayPtrToChars(char** ptr, int length)
        {
            if (ptr == (char**)0)
                throw new ArgumentNullException(nameof(ptr));

            if (length == 0)
                throw new ArgumentNullException(nameof(length));

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            char*[] p1 = { *ptr };
            char[] chars = new char[length];

            for (int i = 0; i < length; i++) chars[i] = p1[0][i];

            return chars;
        }

        /// <summary>
        /// Create a string from a char pointer array.
        /// </summary>
        /// <param name="ptr">Pointer to the char array pointer</param>
        /// <param name="length">Length of the string</param>
        public static unsafe string CharArrayPtrToStr(char** ptr, int length) => new(CharArrayPtrToChars(ptr, length));

        /// <summary>
        /// Transform a string to a byte unmanaged pointer.
        /// </summary>
        /// <param name="str">Str to transform</param>
        /// <param name="encoding">Encoding use to transform the string</param>
        public static unsafe byte* StrToBytePtr(string str, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(str);
            fixed (byte* b = &bytes[0])
                return b;
        }

        /// <summary>
        /// Transform a string to a byte unmanaged pointer.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static unsafe byte* StrUtf8ToBytePtr(string str) => StrToBytePtr(str, Encoding.UTF8);

        
    }
}