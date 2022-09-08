using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Emission.Build
{
    public class CompressionStream : IDisposable
    {
        public StreamReader Reader;
        
        private string _path;

        public CompressionStream(string path)
        {
            _path = path;
            Reader = File.OpenText(path);
        }

        public string Encode()
        {
            var str = Encode(Reader.ReadToEnd());
            return str;
        }
        
        public void Dispose()
        {
            Reader?.Dispose();
        }

        public static string Encode(string str)
        {
            string output = "";

            // Initialize table with single char str
            var table = new Dictionary<string, int>();
            for (int i = 0; i < 255; i++)
            {
                string ch = "";
                ch += (char)i;
                table[ch] = i;
            }

            string p = str[0].ToString(), c = "";
            int code = 256;
            for (int i = 0; i < str.Length; i++)
            {
                if (i != str.Length - 1) c += str[i + 1];
                if (table.ContainsKey(p + c))
                {
                    p += c;
                }
                else
                {
                    output += table[p] + " ";
                    table[p + c] = code;
                    code++;
                    p = c;
                }
                c = "";
            }

            output += table[p] + " ";
            return output;
        }

        public static string Decode(string bytes)
        {
            string output = "";

            var table = new Dictionary<int, string>();
            
            return output;
        }

        
    }
}

