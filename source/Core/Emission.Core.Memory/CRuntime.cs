using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Emission.Core.Memory
{
    public abstract unsafe class CRuntime
    {
		private const string NUMBERS = "0123456789";
		
		private static int _allocations;
		 
		public static int Allocations => _allocations;

		public static void* Malloc(ulong size)
		{
			return Malloc((long)size);
		}

		public static void* Malloc(long size)
		{
			var ptr = Marshal.AllocHGlobal((int)size);

			Interlocked.Increment(ref _allocations);

			return ptr.ToPointer();
		}

		public static void Free(void* a)
		{
			if (a == null)
				return;

			var ptr = new IntPtr(a);
			Marshal.FreeHGlobal(ptr);
			Interlocked.Decrement(ref _allocations);
		}

		public static void Memcpy(void* a, void* b, long size)
		{
			var ap = (byte*)a;
			var bp = (byte*)b;
			for (long i = 0; i < size; ++i)
				*ap++ = *bp++;
		}

		public static void Memcpy(void* a, void* b, ulong size)
		{
			Memcpy(a, b, (long)size);
		}

		public static void Memmove(void* a, void* b, long size)
		{
			void* temp = null;

			try
			{
				temp = Malloc(size);
				Memcpy(temp, b, size);
				Memcpy(a, temp, size);
			}

			finally
			{
				if (temp != null)
					Free(temp);
			}
		}

		public static void Memmove(void* a, void* b, ulong size)
		{
			Memmove(a, b, (long)size);
		}

		public static int Memcmp(void* a, void* b, long size)
		{
			var result = 0;
			var ap = (byte*)a;
			var bp = (byte*)b;
			for (long i = 0; i < size; ++i)
			{
				if (*ap != *bp)
					result += 1;

				ap++;
				bp++;
			}

			return result;
		}

		public static int Memcmp(void* a, void* b, ulong size)
		{
			return Memcmp(a, b, (long)size);
		}

		public static int Memcmp(byte* a, byte[] b, ulong size)
		{
			fixed (void* bptr = b)
			{
				return Memcmp(a, bptr, (long)size);
			}
		}

		public static void Memset(void* ptr, int value, long size)
		{
			var bptr = (byte*)ptr;
			var bval = (byte)value;
			for (long i = 0; i < size; ++i)
				*bptr++ = bval;
		}

		public static void Memset(void* ptr, int value, ulong size)
		{
			Memset(ptr, value, (long)size);
		}

		public static uint _lrotl(uint x, int y)
		{
			return (x << y) | (x >> (32 - y));
		}

		public static void* Realloc(void* a, long newSize)
		{
			if (a == null)
				return Malloc(newSize);

			var ptr = new IntPtr(a);
			var result = Marshal.ReAllocHGlobal(ptr, new IntPtr(newSize));

			return result.ToPointer();
		}

		public static void* Realloc(void* a, ulong newSize)
		{
			return Realloc(a, (long)newSize);
		}

		public static void SetArray<T>(T[] data, T value)
		{
			for (var i = 0; i < data.Length; ++i)
				data[i] = value;
		}

		public static double ldexp(double number, int exponent)
		{
			return number * Math.Pow(2, exponent);
		}

		public static int Strcmp(sbyte* src, string token)
		{
			var result = 0;

			for (var i = 0; i < token.Length; ++i)
			{
				if (src[i] != token[i])
				{
					++result;
				}
			}

			return result;
		}

		public static int Strncmp(sbyte* src, string token, ulong size)
		{
			var result = 0;

			for (var i = 0; i < Math.Min(token.Length, (int)size); ++i)
			{
				if (src[i] != token[i])
				{
					++result;
				}
			}

			return result;
		}

		public static long Strtol(sbyte* start, sbyte** end, int radix)
		{
			// First step - determine length
			var length = 0;
			sbyte* ptr = start;
			while (NUMBERS.IndexOf((char)*ptr) != -1)
			{
				++ptr;
				++length;
			}

			long result = 0;

			// Now build up the number
			ptr = start;
			while (length > 0)
			{
				long num = NUMBERS.IndexOf((char)*ptr);
				long pow = (long)Math.Pow(10, length - 1);
				result += num * pow;

				++ptr;
				--length;
			}

			if (end != null)
			{
				*end = ptr;
			}

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TDest* ReinterpretCast<TDest, TSource>(TSource source) 
			where TDest : unmanaged where TSource : unmanaged
		{
			return (TDest*)(void*)&source;
		}
    }
}
