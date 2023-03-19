using System;
using System.Text;
using System.Collections.Generic;

namespace Emission.IO.Compression
{
    public class LzwCompressor
    {
        private static int CompressedSize { get; set; }
        private static int DeCompressedSize { get; set; }
        
        public static double Ratio => (double)CompressedSize / DeCompressedSize * 100.0;

        public static byte[] Compress(string iBuf) => Compress(Encoding.ASCII.GetBytes(iBuf));
        public static byte[] Compress(byte[] iBuf)
        {
            DeCompressedSize = iBuf.Length;
            Dictionary<List<byte>, int> dictionary = new Dictionary<List<byte>, int>(new ByteArrayComparer());
            
            for (var i = 0; i < 256; i++)
                dictionary.Add(new List<byte>(new []{(byte)i}), i);

            List<byte> window = new List<byte>();
            List<int> oBuf   = new List<int>();
            foreach (var b in iBuf)
            {
                var windowChain = new List<byte>(window) { b };
                if (dictionary.ContainsKey(windowChain))
                {
                    window.Clear();
                    window.AddRange(windowChain);
                }
                else
                {
                    if (dictionary.ContainsKey(window))
                        oBuf.Add(dictionary[window]);
                    else
                        throw new EmissionException(EmissionException.ERR_IO, "Error while Encoding.");
                    CompressedSize = oBuf.Count;
                    dictionary.Add(windowChain, dictionary.Count);
                    window.Clear();
                    window.Add(b);
                }
            }

            if (window.Count == 0) return GetBytes(oBuf.ToArray());
            oBuf.Add(dictionary[window]);
            CompressedSize = oBuf.Count;
            return GetBytes(oBuf.ToArray());
        }

        public static string DecompressStr(byte[] Bufi) => Encoding.ASCII.GetString(Decompress(Bufi));
        public static byte[] Decompress(byte[] Bufi)
        {
            var iBufi = Ia(Bufi);
            var iBuf  = new List<int>(iBufi);
            
            CompressedSize = iBuf.Count;
            
            Dictionary<int, List<byte>> dictionary = new Dictionary<int, List<byte>>();
            for (var i = 0; i < 256; i++)
                dictionary.Add(i, new List<byte> { (byte)i });
            
            var window = dictionary[iBuf[0]];
            
            iBuf.RemoveAt(0);
            var oBuf = new List<byte>(window);
            
            foreach (var k in iBuf)
            {
                var entry = new List<byte>();
                if (dictionary.ContainsKey(k))
                    entry.AddRange(dictionary[k]);
                else if (k == dictionary.Count)
                    entry.AddRange(Add(window.ToArray(), new[] { window.ToArray()[0] }));
                if (entry.Count <= 0) continue;
                oBuf.AddRange(entry);
                DeCompressedSize = oBuf.Count;
                dictionary.Add(dictionary.Count, new List<byte>(Add(window.ToArray(), new[] { entry.ToArray()[0] })));
                window = entry;
            }
            return oBuf.ToArray();
        }
        
        private static byte[] GetBytes(int[] value)
        {
            if (value == null)
                throw new EmissionException(EmissionException.ERR_IO, "GetBytes (int[]) object cannot be null.");
            var numArray = new byte[value.Length * 4];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        
        private static byte[] Add(byte[] left, byte[] right)
        {
            var l1 = left.Length;
            var l2 = right.Length;
            var ret = new byte[l1 + l2];
            Buffer.BlockCopy(left,  0, ret, 0,  l1);
            Buffer.BlockCopy(right, 0, ret, l1, l2);
            return ret;
        }
        
        private static int[] Ia(byte[] ba)
        {
            var bal = ba.Length;
            var int32Count = bal / 4 + (bal % 4 == 0 ? 0 : 1);
            var arr = new int[int32Count];
            Buffer.BlockCopy(ba, 0, arr, 0, bal);
            return arr;
        }
    }
    
    
}
