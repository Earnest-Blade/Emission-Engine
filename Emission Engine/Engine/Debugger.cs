using System;
using System.IO;
using System.Text;

using Emission.Mathematics;

namespace Emission
{
    public class Debug : TextWriter, IDisposable
    {
        public const string LOG_FILE_PATH = ".log";
        
        private const char ARRAY_SEPARATOR = ';';
        private const char PARAM_SEPARATOR = ' ';

        public override Encoding Encoding => _streamWriter != null ? _streamWriter.Encoding : Encoding.Default;

        private StreamWriter _streamWriter;
        private bool _useIoOutput = false;
        
        public Debug(string title)
        {
            Console.Title = title;
            
            SetOutput();
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);
            Write(value);
        }

        public override void Write(string str)
        {
            base.Write(str);
            
            Console.WriteLine(str);
            
            if(_useIoOutput)
                _streamWriter.WriteLine(str);
        }

        public override void Close()
        {
            base.Close();
            _streamWriter.Close();
            _useIoOutput = false;
            
            Warning("[WARNING]: Debugger Close!");
        }

        public new void Dispose()
        {
            base.Dispose();
            _streamWriter.Dispose();
        }

        private void SetOutput()
        {
            _streamWriter = new StreamWriter(LOG_FILE_PATH);
            _useIoOutput = true;
            
            _streamWriter.WriteLine($"[INFO] Define Console's log path as '{LOG_FILE_PATH}'!");
            Console.WriteLine($@"[INFO] Define Console's log path as '{LOG_FILE_PATH}'!");
        }

        #region Static Functions

        public static void Log(Array array)           => Log('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Log(Vector2 vector2)       => Log($"{vector2}");
        public static void Log(Vector3 vector3)       => Log($"{vector3}");
        public static void Log(Vector4 vector4)       => Log($"{vector4}");
        public static void Log(Quaternion quaternion) => Log($"{quaternion}");
        public static void Log(Matrix4 mat4)          => Log($"[{string.Join(ARRAY_SEPARATOR, mat4.ToArray())}]");
        public static void Log(string obj)            => Log((object)obj);

        public static void Log(params object[] str)
        {
            Print(string.Join(PARAM_SEPARATOR, str));
        }

        public static void Warning(Array array)       => Warning('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Warning(string str)        => Warning((object)str);

        public static void Warning(params object[] str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(string.Join(PARAM_SEPARATOR, str));
            Console.ResetColor();
        }

        public static void Error(Array array)         => Error('[' + string.Join(ARRAY_SEPARATOR, array) + ']');
        public static void Error(string str)          => Error((object)str);

        public static void Error(params object[] str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Print(string.Join(PARAM_SEPARATOR, str));
            Console.ResetColor();
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void Print(string str)
        {
            if (str == null) str = "null";
            if(HasInstance()) GameInstance.Debugger.Write(str);
            else Console.WriteLine(str);
        }

        public static bool HasInstance() => GameInstance.Debugger != null;
        
        #endregion
    }
}
