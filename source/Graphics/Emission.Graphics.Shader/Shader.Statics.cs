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
        /// <summary>
        /// Create a new glsl shader from a filestream.
        /// </summary>
        public static Shader FromFile(FileStream? file) => FromFile(file, null);
        
        /// <summary>
        /// Create a new shader from a filestream.
        /// </summary>
        public static Shader FromFile(FileStream? file, string? name)
        {
            if (!file.CanRead && file.Length <= 0)
                throw new ArgumentException("Cannot use file stream.", nameof(file));

            return FromStream(file, name);
        }
        
        /// <summary>
        /// Create a new glsl shader from shader's path.
        /// </summary>
        public static Shader FromPath(string? path) => FromPath(path, null);
        
        /// <summary>
        /// Create a new glsl shader from shader's path.
        /// </summary>
        public static Shader FromPath(string? path, string? name)
        {
            if (path == null) 
                throw new ArgumentNullException(nameof(path));

            if (path == null)
                throw new ArgumentException("Path cannot be empty.", nameof(path));

            StreamReader reader = EFile.OpenText(path);
            return FromStream(reader.BaseStream, name);
        }

        public static Shader FromMemory(MemoryStream? memoryStream) => FromMemory(memoryStream, null);
        public static Shader FromMemory(MemoryStream? memoryStream, string? name)
        {
            if (memoryStream == null || memoryStream.Length == 0) 
                throw new ArgumentNullException(nameof(memoryStream));

            return FromStream(memoryStream, name);
        }

        public static Shader FromStream(Stream? stream) => FromStream(stream, null);
        public static Shader FromStream(Stream? stream, string? name)
        {
            if (stream == null || !stream.CanRead)
                throw new ArgumentNullException(nameof(stream));

            byte[] buffer = Memory.ReadStream(stream, EFile.DEFAULT_BUFFER_SIZE);
            stream.Close();

            ShaderStruct shader = ShaderBuilder.LoadShader(Encoding.UTF8.GetString(buffer).Split(Environment.NewLine));
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

        /// <summary>
        /// Return active shader program.
        /// </summary>
        /// <returns></returns>
        //public static uint GetActiveProgram() => _activeShader;
    }
}
