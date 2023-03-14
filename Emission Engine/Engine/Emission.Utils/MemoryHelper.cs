using System;
using System.IO;

namespace Emission
{
    internal static class MemoryHelper
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
    }
}
