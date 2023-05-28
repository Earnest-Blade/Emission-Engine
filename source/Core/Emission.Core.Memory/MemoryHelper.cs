using System.Text;
using System.Runtime.InteropServices;

namespace Emission.Core.Memory
{
    public static unsafe class MemoryHelper
    {

        /// <summary>
        /// Reads a stream until the end is reached into a byte array.
        /// </summary>
        /// <param name="stream">Stream to read</param>
        /// <param name="size">Initial buffer size</param>
        public static byte[] ReadStream(Stream stream, int size)
        {
            if (size < 1) size = 0x8000;

            int length = 0, bytesRead;
            byte[] buffer = new byte[size];

            while ((bytesRead = stream.Read(buffer, length, buffer.Length - length)) > 0)
            {
                length += bytesRead;
                if (length == buffer.Length)
                {
                    int streamByte = stream.ReadByte();
                    if (streamByte == -1)
                        return buffer;

                    byte[] destArray = new byte[buffer.Length * 2];
                    Array.Copy(buffer, destArray, buffer.Length);
                    
                    destArray[length] = (byte)streamByte;
                    length++;
                }
            }

            byte[] returnBuffer = new byte[length];
            Array.Copy(buffer, returnBuffer, length);
            
            return returnBuffer;
        }
        
        public static T[][] CreateArray<T>(int d1, int d2)
        {
            var result = new T[d1][];
            for (int i = 0; i < d1; i++)
            {
                result[i] = new T[d2];
            }

            return result;
        }

        /// <summary>
        /// Return a string from an unmanaged pointer.
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="encoding">Encoder use to decode the string.</param>
        public static string? PtrToString(void* ptr, Encoding encoding)
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
        /// Return a string formatted as UTF-8 from an unmanaged pointer.
        /// </summary>
        public static string? PtrToStringUtf8(void* ptr) => PtrToString(ptr, Encoding.UTF8);
        
        /// <summary>
        /// Return a string formatted as UTF-8 from an unmanaged pointer.
        /// </summary>
        public static string? PtrToStringUtf8(char* ptr) => PtrToString(ptr, Encoding.UTF8);

        /// <summary>
        /// Return a byte pointer from a string.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        /// <param name="length">Pointer use to get byte array size</param>
        /// <param name="encoding">Encoding of the text</param>
        public static byte** StrToByteArrayPtr(string str, int* length, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str)) return (byte**)0;
            byte[] buffer = encoding.GetBytes(str);
            
            if(length != (int*)0)
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
        public static byte** StrToByteArrayPtr(string str, Encoding encoding) => StrToByteArrayPtr(str, (int*)0, encoding);

        /// <summary>
        /// Return a byte pointer from a string formatted as UTF-8.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        public static byte** StrUtf8ToByteArrayPtr(string str) => StrToByteArrayPtr(str, Encoding.UTF8);

        
        /// <summary>
        /// Return a byte pointer from a string formatted as UTF-8.
        /// </summary>
        /// <param name="str">String to transform to a byte ptr</param>
        /// <param name="length">Pointer use to get byte array size</param>
        public static byte** StrUtf8ToByteArrayPtr(string str, int* length) => StrToByteArrayPtr(str, length, Encoding.UTF8);

        /// <summary>
        /// Return a byte pointer from a string.
        /// </summary>
        /// <param name="str">String to transform to a char pointer</param>
        /// <param name="length">Pointer use to get char array length</param>
        public static char** StrToCharArrayPtr(string str, int* length)
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
        public static char** StrToCharArrayPtr(string str) => StrToCharArrayPtr(str, (int*)0);

        /// <summary>
        /// Create a char array from a char pointer array.
        /// </summary>
        /// <param name="ptr">Pointer to the char array pointer</param>
        /// <param name="length">Length of the string</param>
        public static char[] CharArrayPtrToChars(char** ptr, int length)
        {
            if (ptr == (char**)0)
                throw new ArgumentNullException(nameof(ptr));

            if (length == 0)
                throw new ArgumentNullException(nameof(length));

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            char*[] p1 = { *ptr };
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)  chars[i] = p1[0][i];
            
            return chars;
        }

        /// <summary>
        /// Create a string from a char pointer array.
        /// </summary>
        /// <param name="ptr">Pointer to the char array pointer</param>
        /// <param name="length">Length of the string</param>
        public static string CharArrayPtrToStr(char** ptr, int length) => new (CharArrayPtrToChars(ptr, length));
        
        /// <summary>
        /// Transform a string to a byte unmanaged pointer.
        /// </summary>
        /// <param name="str">Str to transform</param>
        /// <param name="encoding">Encoding use to transform the string</param>
        public static byte* StrToBytePtr(string str, Encoding encoding)
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
        public static byte* StrUtf8ToBytePtr(string str) => StrToBytePtr(str, Encoding.UTF8);

        /// <summary>
        /// Transform an unsigned byte array to a unsigned byte pointer.
        /// </summary>
        /// <param name="array">Array to transform</param>
        public static byte* BytePtrFromByteArray(byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            
            fixed (byte* ptr = &array[0])
                return ptr;
        }

        /// <summary>
        /// Transform a signed byte array to a signed byte pointer.
        /// </summary>
        /// <param name="array">Array to transform</param>
        public static sbyte* SbytePtrFromSbyteArray(sbyte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            fixed (sbyte* ptr = &array[0])
                return ptr;
        }
    }
}
