namespace Emission.Core.Memory
{
    public static unsafe partial class Memory
    {
        /// <summary>
        /// Reads a stream until the end is reached into a byte array.
        /// </summary>
        /// <param name="stream">Stream to read</param>
        /// <param name="size">Initial buffer size</param>
        public static byte[] ReadStream(Stream? stream, int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            if (stream == null || stream.CanRead == false)
                throw new ArgumentNullException(nameof(stream));

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

        public static T[][] Create2DArray<T>(int d1, int d2)
        {
            T[][] result = new T[d1][];
            for (int i = 0; i < d1; i++)
            {
                result[i] = new T[d2];
            }

            return result;
        }


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