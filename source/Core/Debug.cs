using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Emission.Core.IO;
using Emission.Core.Mathematics;
using Emission.Natives.Win32;
using JetBrains.Annotations;

using static Emission.Natives.Win32.User32;

namespace Emission.Core
{
    public class Debug : TextWriter, IDisposable
    {
        /// <summary>
        /// Define the default log file location.
        /// </summary>
        public const string LOG_FILE_DEFAULT_PATH = "EEngine.log";
        
        private const char ARRAY_SEPARATOR = ';';
        private const char PARAM_SEPARATOR = ' ';
        private const string MESSAGE_BOX_TITLE = "Fatal Error";
        private const string FALSE_STR = "false";
        private const string TRUE_STR = "true";

        /// <summary>
        /// Get or set developer console's title.
        /// </summary>
        public static string ConsoleTitle
        {
            get => Console.Title;
            set => Console.Title = value;
        }
        
        /// <summary>
        /// Define current text writer encoding.
        /// </summary>
        public override Encoding Encoding => _encoding;

        private const string LOG_FILE_TEMPLATE = "{0} | {1}";
        
        private Stream _stream;
        private string _logPath;
        private Encoding _encoding;
        private Encoder _encoder; 
        private byte[] _byteBuffer;
        private char[] _charBuffer;
        private int _charPos;
        private int _charLen;

        public Debug(string title) : this(title, LOG_FILE_DEFAULT_PATH) { }
        public Debug(string title, string path) : this(title, path, Encoding.UTF8) { }
        public Debug(string title, string path, Encoding encoding) : this(title, path, encoding, EFile.DEFAULT_BUFFER_SIZE) { }
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

            _logPath = path;
            _encoding = encoding;
            _encoder = encoding.GetEncoder();
            _stream = CreateStream(path, encoding, bufferSize);
            _byteBuffer = new byte[_encoding.GetMaxByteCount(bufferSize)];
            _charBuffer = new char[bufferSize];
        }

        /// <summary>
        /// Write a character to the stream text.
        /// </summary>
        /// <param name="value">Character to write</param>
        public override void Write(char value)
        {   
            if(_charPos == _charLen) Flush(true, true);

            _charBuffer[_charPos] = value;
            _charPos++;
            Flush(true, true);
        }

        /// <summary>
        /// Write a character to the stream text.
        /// </summary>
        /// <param name="value">Character to write</param>
        public override void Write(string? value)
        {
            value ??= "null";
            Write(value, appendNewLine:false);
        }

        
        /// <summary>
        /// Write a character on a new line.
        /// </summary>
        /// <param name="value">Character to write</param>
        public override void WriteLine(string? value)
        {
            value ??= "null";
            Write(value, appendNewLine:true);
        }

        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.
        /// </summary>
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
            {
                _stream.WriteAsync(_byteBuffer, 0, count);
            }

            if(flushStream) 
                _stream.Flush();
        }

        /// <summary>
        /// Closes the current writer and releases any system resources associated with the writer.
        /// </summary>
        public override void Close()
        {
            base.Close();
            _stream.Close();
            
            LogWarning("[WARNING]: Debugger Close!");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
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

        /// <summary>
        /// Assert a condition and logs an error message to the Unity console on failure.
        /// </summary>
        /// <param name="condition">Condition to check</param>
        public static void Assert(bool condition) => Assert(condition, String.Empty);
        
        /// <summary>
        /// Assert a condition and logs an error message to the Unity console on failure.
        /// </summary>
        /// <param name="condition">Condition to check</param>
        /// <param name="message">Message to send on failure.</param>
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                LogError(message);
                Application.Stop(-1);
            }
        }

        /// <summary>
        /// Log an array to the console
        /// </summary>
        public static void Log(Array array) => Log('[' + string.Join(ARRAY_SEPARATOR, array.Cast<object>()) + ']');
        
        /// <summary>
        /// Log a 2D Vector to the console
        /// </summary>
        public static void Log(Vector2 vector2) => Log($"{vector2}");

        public static void Log(bool b) => Log(b ? TRUE_STR : FALSE_STR);
        
        /// <summary>
        /// Log a 3D Vector to the console
        /// </summary>
        public static void Log(Vector3 vector3) => Log($"{vector3}");
        
        /// <summary>
        /// Log a 4D Vector to the console
        /// </summary>
        public static void Log(Vector4 vector4) => Log($"{vector4}");
        
        /// <summary>
        /// Log a Quaternion to the console
        /// </summary>
        public static void Log(Quaternion quaternion) => Log($"{quaternion}");
        
        /// <summary>
        /// Log a Matrix4 to the console
        /// </summary>
        public static void Log(Matrix3 mat3) => Log(mat3.ToArray());
        
        /// <summary>
        /// Log a Matrix4 to the console
        /// </summary>
        public static void Log(Matrix4 mat4) => Log(mat4.ToArray());
        
        /// <summary>
        /// Log a message to the console
        /// </summary>
        public static void Log(string str) => Log((object)str);

        public static unsafe void Log(void* ptr) => Log(new IntPtr(ptr).ToString());

        /// <summary>
        /// Log a message to the console. Multiple parameters can be entered. As white space will separate them.
        /// </summary>
        public static void Log(params object[] obj)
        {
            WriteLn(string.Join(PARAM_SEPARATOR, obj.Cast<object>()));
        }

        /// <summary>
        /// A variant of <see cref="Log(string)">Debug.Log</see> that logs a message if the condition is true.
        /// </summary>
        /// <param name="condition">Condition to check</param>
        /// <param name="obj">Content to log</param>
        public static void LogAssert(bool condition, params object[] obj) { if(condition) Log(obj); }
        
        /// <summary>
        /// A variant of <see cref="Log(string)">Debug.Log</see> that logs a message if the condition is true.
        /// </summary>
        /// <param name="condition">Condition to check</param>
        /// <param name="str">Message to log</param>
        public static void LogAssert(bool condition, string str) { if(condition) Log(str); }
        
        /// <summary>
        /// A variant of <see cref="Log(string)">Debug.Log</see> that logs a message if the condition is true.
        /// </summary>
        /// <param name="condition">Condition to check</param>
        /// <param name="array">Array to log</param>
        public static void LogAssert(bool condition, Array array) { if(condition) Log(array); }

        /// <summary>
        ///  A variant of <see cref="Log(string)">Debug.Log</see> that logs a warning message.
        /// </summary>
        /// <param name="array">Array to log.</param>
        public static void LogWarning(Array array) => LogWarning('[' + string.Join(ARRAY_SEPARATOR, array.Cast<object>()) + ']');
        
        /// <summary>
        ///  A variant of <see cref="Log(string)">Debug.Log</see> that logs a warning message.
        /// </summary>
        /// <param name="str">Message to log.</param>
        public static void LogWarning(string str) => LogWarning((object)str);

        /// <summary>
        ///  A variant of <see cref="Log(string)">Debug.Log</see> that logs a warning message.
        /// </summary>
        /// <param name="obj">Content to log.</param>
        public static void LogWarning(params object[] obj)
        {
            WriteLn(string.Join(PARAM_SEPARATOR, obj.Cast<object>()), ConsoleColor.Yellow);
        }

        /*
        /// <summary>
        /// A variant of <see cref="Log(string)">Debug.Log</see> that logs an error message.
        /// </summary>
        /// <param name="array">Array to log.</param>
        /// <param name="showTrace">If log the stacktrace after the error message</param>
        public static void LogError(Array array) => LogError('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        
        /// <summary>
        /// A variant of <see cref="Log(string)">Debug.Log</see> that logs an error message.
        /// </summary>
        /// <param name="str">Message to log.</param>
        /// <param name="showTrace">If log the stacktrace after the error message</param>
        public static void LogError(string str) => LogError((object)str);*/

        public static void LogError(string str)
        {
            WriteLn(str, ConsoleColor.Red);
        }
        
        /// <summary>
        /// A variant of <see cref="Log(string)">Debug.Log</see> that logs an error message.
        /// </summary>
        /// <param name="exception">Exception to print.</param>
        public static void LogError(EmissionException exception)
        {
            WriteLn(exception.Title + " : " + exception.ErrorType.FullName + ". " + exception.Message, ConsoleColor.Red);
            
            StackTrace trace = new StackTrace(exception);
            for (int i = 0; i < trace.FrameCount; i++)
            {
                StackFrame? frame = trace.GetFrame(i);
                if (frame == null) continue;
                
                WriteLn("\t(" + i + ") at " + frame.GetMethod() + " in " + frame.GetFileName() + ":line " + frame.GetFileLineNumber(), ConsoleColor.Red);
            }
        }
        
        /// <summary>
        /// Clear developer console.
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }
        
        /// <summary>
        /// Displays a modal dialog box that contains a system icon, a set of buttons, and a brief application-specific message, such as status or error information.
        /// The message box returns an integer value that indicates which button the user clicked.
        /// </summary>
        /// <param name="title">The dialog box title.</param>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="type">Content and behavior of the dialog box. (With 'MB_' prefix)</param>
        /// <returns></returns>
        public static int ShowMessageBox(string title, string message, uint type)
        {
            if (Platform.Type == PlatformType.Windows && HasInstance())
            {
                return User32.ShowMessageBox(IntPtr.Zero, message, title, type);
            }

            return 0;
        }
        
        internal static void FatalError(FatalEmissionException error)
        {
            LogError(error);
            ShowMessageBox(MESSAGE_BOX_TITLE, error.Message, MB_ICONERROR | MB_OK | MB_DEFBUTTON1 | MB_TOPMOST);
            
            Environment.Exit(-1);
        }
        
        private static void WriteLn(string? str, ConsoleColor color = ConsoleColor.White)
        {
            Task.Factory.StartNew(() =>
            {
                if (!HasInstance()) return;
                Console.ForegroundColor = color;
                
                Application.Instance?.Context.Debugger.WriteLine(str);
                Console.WriteLine(str);
            });
        }

        private static void WriteAssert(string stackTrace, [CanBeNull] string? message)
        {
            WriteLn(message + Environment.NewLine + stackTrace);
        }

        public static bool HasInstance() => Application.HasInstance() && Application.Instance?.Context.Debugger != null;
        
        #endregion
    }
}
