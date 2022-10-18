using System.Security;

namespace System.Collections.Generic
{
    public class ByteArrayComparer : IEqualityComparer<List<byte>>
    {
        public bool Equals(List<byte>left, List<byte> right)
        {
            if (left == null || right == null)
                return false;
            return Compare(left.ToArray(), right.ToArray());
        }
        
        public unsafe int GetHashCode(List<byte> obj)
        {
            var obj1   = obj.ToArray();
            var cbSize = obj1.Length;
            var hash   = 0x811C9DC5;
            fixed (byte* pb = obj1)
            {
                var nb = pb;
                while (cbSize >= 4)
                {
                    hash   ^= *(uint*)nb;
                    hash   *= 0x1000193;
                    nb     += 4;
                    cbSize -= 4;
                }
                switch (cbSize & 3)
                {
                    case 3:
                        hash ^= *(uint*)(nb + 2);
                        hash *= 0x1000193;
                        goto case 2;
                    case 2:
                        hash ^= *(uint*)(nb + 1);
                        hash *= 0x1000193;
                        goto case 1;
                    case 1:
                        hash ^= *nb;
                        hash *= 0x1000193;
                        break;
                }
            }
            return (int)hash;
        }
        
        [SecuritySafeCritical]
        private static unsafe bool Compare(byte[] a1, byte[] a2)
        {
            if (a1 == null && a2 == null)
                return true;
            if (a1 == null || a2 == null || a1.Length != a2.Length)
                return false;
            fixed (byte* p1 = a1, p2 = a2)
            {
                var len = a1.Length;
                byte* x1  = p1, x2 = p2;
                while (len > 7)
                {
                    if (*(long*)x2 != *(long*)x1)
                        return false;
                    x1  += 8;
                    x2  += 8;
                    len -= 8;
                }
                switch (len % 8)
                {
                    case 0:
                        break;
                    case 7:
                        if (*(int*)x2 != *(int*)x1)
                            return false;
                        x1 += 4;
                        x2 += 4;
                        if (*(short*)x2 != *(short*)x1)
                            return false;
                        x1 += 2;
                        x2 += 2;
                        if (*x2 != *x1)
                            return false;
                        break;
                    case 6:
                        if (*(int*)x2 != *(int*)x1)
                            return false;
                        x1 += 4;
                        x2 += 4;
                        if (*(short*)x2 != *(short*)x1)
                            return false;
                        break;
                    case 5:
                        if (*(int*)x2 != *(int*)x1)
                            return false;
                        x1 += 4;
                        x2 += 4;
                        if (*x2 != *x1)
                            return false;
                        break;
                    case 4:
                        if (*(int*)x2 != *(int*)x1)
                            return false;
                        break;
                    case 3:
                        if (*(short*)x2 != *(short*)x1)
                            return false;
                        x1 += 2;
                        x2 += 2;
                        if (*x2 != *x1)
                            return false;
                        break;
                    case 2:
                        if (*(short*)x2 != *(short*)x1)
                            return false;
                        break;
                    case 1:
                        if (*x2 != *x1)
                            return false;
                        break;
                }
                return true;
            }
        }
    }
}
