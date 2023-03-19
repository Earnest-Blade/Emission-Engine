using System;
using System.IO;
using System.Text;

using Emission.IO;

using JetBrains.Annotations;

namespace Emission.Graphics
{
    public partial class Shader
    {
        public static Shader FromFile([NotNull] FileInfo file) => FromFile(file, null);
        public static Shader FromFile([NotNull] FileInfo file, [CanBeNull] string name)
        {
            if (GameFile.Exists(file)) return null;

            return FromStream(file.OpenRead(), name);
        }
        
        public static Shader FromPath([CanBeNull] string? path) => FromPath(path, null);
        public static Shader FromPath([CanBeNull] string? path, [CanBeNull] string name)
        {
            if (path == null) return null;
            if (path.Length == 0) return null;

            StreamReader reader = GameFile.OpenText(path);
            return FromStream(reader.BaseStream, name);
        }

        public static Shader FromMemory(MemoryStream memoryStream) => FromMemory(memoryStream, null);
        public static Shader FromMemory(MemoryStream memoryStream, [CanBeNull] string name)
        {
            if (memoryStream == null || memoryStream.Length == 0) return null;

            return FromStream(memoryStream, name);
        }

        public static Shader FromStream([NotNull] Stream stream) => FromStream(stream, null);
        public static Shader FromStream([NotNull] Stream stream, [CanBeNull] string name)
        {
            if (stream == null || !stream.CanRead)
                throw new ArgumentNullException(nameof(stream));

            byte[] buffer = MemoryHelper.ReadStream(stream, GameFile.DEFAULT_BUFFER_SIZE);
            stream.Close();

            ShaderStruct shader = ShaderLoader.LoadShader(Encoding.UTF8.GetString(buffer).Split(Environment.NewLine));
            return new Shader(shader, name);
        }

        public static Shader FromVertexFragment([CanBeNull] string vertexShader, [CanBeNull] string fragmentShader) => FromVertexFragment(vertexShader, fragmentShader, null);
        public static Shader FromVertexFragment([CanBeNull] string vertexShader, [CanBeNull] string fragmentShader, [CanBeNull] string name)
        {
            if(string.IsNullOrEmpty(vertexShader))
                Debug.LogWarning("[WARNING] Trying to load an empty vertex shader!");
            
            if(string.IsNullOrEmpty(fragmentShader))
                Debug.LogWarning("[WARNING] Trying to load an empty fragment shader!");
            
            return new Shader(new ShaderStruct(vertexShader, fragmentShader), name);
        }

        public static Shader FromVertexGeomertyFragment([CanBeNull] string vertexShader, [CanBeNull] string geomertyShader, [CanBeNull] string fragmentShader) => FromVertexGeomertyFragment(vertexShader, geomertyShader, fragmentShader, null);
        public static Shader FromVertexGeomertyFragment([CanBeNull] string vertexShader, [CanBeNull] string geomertyShader, [CanBeNull] string fragmentShader, [CanBeNull] string name)
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
