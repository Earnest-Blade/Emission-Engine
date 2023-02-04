using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Emission.IO;
using Emission.Mathematics;
using JetBrains.Annotations;

namespace Emission
{
    public class Debug : TextWriter, IDisposable
    {
        public static string LOG_DEFAULT_PATH = "EEngine.log";
        
        private const char ARRAY_SEPARATOR = ';';
        private const char PARAM_SEPARATOR = ' ';

        public override Encoding Encoding => _encoding;

        private Stream _stream;
        private Encoding _encoding;
        private Encoder _encoder; 
        private byte[] _byteBuffer;
        private char[] _charBuffer;
        private int _charPos;
        private int _charLen;

        public Debug(string title) : this(title, LOG_DEFAULT_PATH) { }
        public Debug(string title, string path) : this(title, path, Encoding.UTF8) { }
        public Debug(string title, string path, Encoding encoding) : this(title, path, encoding, GameFile.DEFAULT_BUFFER_SIZE) { }
        public Debug(string title, string path, Encoding encoding, int bufferSize)
        {
            /* Handle exceptions */
            if (title == null)
                throw new ArgumentNullException(nameof(title));
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Trim() == "")
                throw new ArgumentException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (bufferSize == 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            
            Console.Title = title;

            _encoding = encoding;
            _encoder = encoding.GetEncoder();
            _stream = CreateStream(path, encoding, bufferSize);
            _byteBuffer = new byte[_encoding.GetMaxByteCount(bufferSize)];
            _charBuffer = new char[bufferSize];
            
            Write($"{DateTime.Now.ToString("F")}\n{Environment.OSVersion.Platform} - {Environment.OSVersion.Version}\n{GameInstance.EngineSettings.ToString()}\n\n");
        }

        public override void Write(char value)
        {   
            if(_charPos == _charLen) Flush(true, true);

            _charBuffer[_charPos] = value;
            _charPos++;
            Flush(true, true);
        }

        public override void Write(string value)
        {
            value ??= "null";
            Write(value, appendNewLine:false);
        }

        public override void WriteLine(string value)
        {
            value ??= "null";
            Write(value, appendNewLine:true);
        }

        public override void Flush()
        {
            Flush(true, true);
        }

        private void Flush(bool flushStream, bool flushEncoder)
        {
            if (_charPos == 0 & !flushStream && !flushEncoder) return;

            int count = _encoder.GetBytes(_charBuffer, 0, _charPos, _byteBuffer, 0, flushEncoder);
            _charPos = 0;
            
            if (count > 0)
                _stream.Write(_byteBuffer, 0, count);

            if(flushStream) 
                _stream.Flush();
        }

        public override void Close()
        {
            base.Close();
            _stream.Close();
            
            Warning("[WARNING]: Debugger Close!");
        }

        public new void Dispose()
        {
            base.Dispose();
            _stream.Dispose();
        }

        private Stream CreateStream(string path, Encoding encoding, int bufferSize)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentException(nameof(encoding));
            if (path.Length == 0)
                throw new ArgumentException(Strings.InvalidValue);
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            return new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, bufferSize, FileOptions.SequentialScan);
        }

        private unsafe void Write(ReadOnlySpan<char> buffer, bool appendNewLine)
        {
            if (buffer.Length <= 4 && buffer.Length <= _charLen - _charPos)
            {
                foreach (var t in buffer)
                    _charBuffer[_charPos++] = t;
            }
            else
            {
                char[] charBuffer = _charBuffer;
                
                fixed (char* bufferPtr = &MemoryMarshal.GetReference(buffer))
                fixed (char* dstPtr = &charBuffer[0])
                {
                    char* srcPtr = bufferPtr;
                    int count = buffer.Length;
                    int dstPos = _charPos; 
                    while (count > 0)
                    {
                        if (dstPos == charBuffer.Length)
                        {
                            Flush(false, false);
                            dstPos = 0;
                        }

                        int n = Math.Min(charBuffer.Length - dstPos, count);
                        int bytesToCopy = n * sizeof(char);

                        Buffer.MemoryCopy(srcPtr, dstPtr + dstPos, bytesToCopy, bytesToCopy);

                        _charPos += n;
                        dstPos += n;
                        srcPtr += n;
                        count -= n;
                    }
                }
            }
            
            if (appendNewLine)
            {
                char[] coreNewLine = CoreNewLine;
                foreach (var t in coreNewLine)
                {
                    if (_charPos == _charLen)
                    {
                        Flush(false, false);
                    }

                    _charBuffer[_charPos] = t;
                    _charPos++;
                }
            }
            
            Flush(true, true);
        }

        #region Static Functions

        public static void Assert(bool condition) => Assert(condition, string.Empty);
        public static void Assert(bool condition, [CanBeNull] string message)
        {
            if(!condition) Error(message);
        }
        
        public static void Log(Array array) => Log('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Log(Vector2 vector2) => Log($"{vector2}");
        public static void Log(Vector3 vector3) => Log($"{vector3}");
        public static void Log(Vector4 vector4) => Log($"{vector4}");
        public static void Log(Quaternion quaternion) => Log($"{quaternion}");
        public static void Log(Matrix4 mat4) => Log($"[{string.Join(ARRAY_SEPARATOR, mat4.ToArray())}]");
        public static void Log([CanBeNull] string str) => Log((object)str);

        public static void Log(params object[] obj)
        {
            WriteLn(string.Join(PARAM_SEPARATOR, obj));
        }

        public static void LogIf(bool condition, params object[] obj) { if(condition) Log(obj); }
        public static void LogIf(bool condition, string str) { if(condition) Log(str); }
        public static void LogIf(bool condition, Array array) { if(condition) Log(array); }

        public static void Warning(Array array) => Warning('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Warning(string str) => Warning((object)str);

        public static void Warning(params object[] str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLn(string.Join(PARAM_SEPARATOR, str));
            Console.ResetColor();
        }

        public static void Error(Array array) => Error('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Error(string str) => Error((object)str);

        public static void Error(params object[] str)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            string stackTrace;
            try
            {
                stackTrace = new StackTrace(0, true).ToString();
            }
            catch
            {
                stackTrace = "";
            }
            
            WriteAssert(stackTrace, string.Join(PARAM_SEPARATOR, str));
            
            Console.ResetColor();
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void SetTitle(string title)
        {
            Console.Title = title;
        }

        private static void WriteLn(string str)
        {
            if(HasInstance()) GameInstance.Debugger.WriteLine(str);
            Console.WriteLine(str);
        }

        private static void WriteAssert(string stackTrace, [CanBeNull] string message)
        {
            WriteLn(message + Environment.NewLine 
                            + stackTrace);
        }

        public static bool HasInstance() => GameInstance.Debugger != null;
        
        #endregion
    }
}
