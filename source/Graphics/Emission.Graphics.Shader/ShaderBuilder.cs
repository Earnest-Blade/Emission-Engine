using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Emission.Core;
using Emission.Core.IO;
using Emission.Core.Memory;

namespace Emission.Graphics
{
    public partial class Shader
    {
        public static Shader FromFile([NotNull] FileInfo file) => FromFile(file, null);
        public static Shader FromFile([NotNull] FileInfo file, string name)
        {
            if (!file.Exists) return null;

            return FromStream(file.OpenRead(), name);
        }
        
        public static Shader FromPath(string? path) => FromPath(path, null);
        public static Shader FromPath(string? path, string name)
        {
            if (path == null) return null;
            if (path.Length == 0) return null;

            StreamReader reader = EFile.OpenText(path);
            return FromStream(reader.BaseStream, name);
        }

        public static Shader FromMemory(MemoryStream memoryStream) => FromMemory(memoryStream, null);
        public static Shader FromMemory(MemoryStream memoryStream, string name)
        {
            if (memoryStream == null || memoryStream.Length == 0) return null;

            return FromStream(memoryStream, name);
        }

        public static Shader FromStream([NotNull] Stream stream) => FromStream(stream, null);
        public static Shader FromStream([NotNull] Stream stream, string name)
        {
            if (stream == null || !stream.CanRead)
                throw new ArgumentNullException(nameof(stream));

            byte[] buffer = MemoryHelper.ReadStream(stream, EFile.DEFAULT_BUFFER_SIZE);
            stream.Close();

            ShaderStruct shader = ShaderLoader.LoadShader(Encoding.UTF8.GetString(buffer).Split(Environment.NewLine));
            return new Shader(shader, name);
        }

        public static Shader FromVertexFragment(string vertexShader, string fragmentShader) => FromVertexFragment(vertexShader, fragmentShader, null);
        public static Shader FromVertexFragment(string vertexShader, string fragmentShader, string name)
        {
            if(string.IsNullOrEmpty(vertexShader))
                Debug.LogWarning("[WARNING] Trying to load an empty vertex shader!");
            
            if(string.IsNullOrEmpty(fragmentShader))
                Debug.LogWarning("[WARNING] Trying to load an empty fragment shader!");
            
            return new Shader(new ShaderStruct(vertexShader, fragmentShader), name);
        }

        public static Shader FromVertexGeomertyFragment(string vertexShader, string geomertyShader, string fragmentShader) => FromVertexGeomertyFragment(vertexShader, geomertyShader, fragmentShader, null);
        public static Shader FromVertexGeomertyFragment(string vertexShader, string geomertyShader, string fragmentShader, string name)
        {
            if(string.IsNullOrEmpty(vertexShader))
                Debug.LogWarning("[WARNING] Trying to load an empty vertex shader!");
            
            if(string.IsNullOrEmpty(geomertyShader))
                Debug.LogWarning("[WARNING] Trying to load an empty geomerty shader!");
            
            if(string.IsNullOrEmpty(fragmentShader))
                Debug.LogWarning("[WARNING] Trying to load an empty fragment shader!");
            
            return new Shader(new ShaderStruct(vertexShader, geomertyShader, fragmentShader), name);
        }
    }
}
