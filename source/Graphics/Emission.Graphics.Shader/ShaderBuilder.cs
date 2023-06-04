using System.Runtime.InteropServices;
using Emission.Core;
using Emission.Core.Memory;
using Emission.Natives.GL;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    internal static unsafe class ShaderBuilder
    {
        public const string COMMENT = "//";

        public const int NONE = GL_NONE;
        public const int DEFINE_SHADER = 0x8B2F;
        public const int VERTEX_SHADER = GL_VERTEX_SHADER;
        public const int GEOMETRY_SHADER = GL_GEOMETRY_SHADER;
        public const int FRAGMENT_SHADER = GL_FRAGMENT_SHADER;
        public const int TESSELATION_CONTROL_SHADER = 0x8E88;
        public const int TESSELATION_EVAL_SHADER = 0x8E87;

        public const string DEFINE_KEY = "define:";
        public const string VERTEX_SHADER_KEY = "vertex:";
        public const string GEOMETRY_SHADER_KEY = "geometry:";
        public const string FRAGMENT_SHADER_KEY = "fragment:";
        public const string TCS_SHADER_KEY = "tcs:";
        public const string TES_SHADER_KEY = "tes:";

        private const int COMPILE_SHADER_OUTPUT_BUFFER_SIZE = 1064;
        
        /// <summary>
        /// Create a <see cref="ShaderStruct"/> from an array of line.
        /// </summary>
        /// <param name="data">Data to parse.</param>
        /// <returns>Shader parsed content.</returns>
        public static ShaderStruct LoadShader(IEnumerable<string> data)
        {
            ShaderStruct shader = new ShaderStruct();
            int type = NONE;

            foreach (string line in data)
            {
                if (line.StartsWith(COMMENT)) continue;

                if (line.Contains(':'))
                {
                    if (line.Contains(DEFINE_KEY))
                    {
                        type = DEFINE_SHADER;
                        continue;
                    }

                    if (line.Contains(VERTEX_SHADER_KEY))
                    {
                        type = VERTEX_SHADER;
                        continue;
                    }

                    if (line.Contains(GEOMETRY_SHADER_KEY))
                    {
                        type = GEOMETRY_SHADER;
                        continue;
                    }

                    if (line.Contains(FRAGMENT_SHADER_KEY))
                    {
                        type = FRAGMENT_SHADER;
                        continue;
                    }

                    if (line.Contains(TCS_SHADER_KEY))
                    {
                        type = TESSELATION_CONTROL_SHADER;
                        continue;
                    }

                    if (line.Contains(TES_SHADER_KEY))
                    {
                        type = TESSELATION_EVAL_SHADER;
                    }
                }
                else
                {
                    shader.Write(type, line);
                }
            }

            return shader;
        }

        /// <summary>
        /// Compile Shader using OpenGL.
        /// </summary>
        /// <param name="type">Shader type to compile</param>
        /// <param name="data">Shader data to compile</param>
        /// <returns></returns>
        /// <exception cref="EmissionException"></exception>
        public static uint CompileShader(uint type, ref string data)
        {
            uint shader = glCreateShader(type);
            
            int l = data.Length;
            char** ptr = Memory.StrToCharArrayPtr(data, &l);
            glShaderSource(shader, 1, ptr, &l);

            glCompileShader(shader);
            
            int shaderStatus;
            glGetShaderiv(shader, GL_COMPILE_STATUS, &shaderStatus);
            
            if (shaderStatus != GL_TRUE)
            {
                int length;
                char* source = (char*)CRuntime.Malloc(COMPILE_SHADER_OUTPUT_BUFFER_SIZE);
                glGetShaderInfoLog(shader, COMPILE_SHADER_OUTPUT_BUFFER_SIZE, &length, source);
                string? info = Memory.PtrToStringUtf8(source, length);
                CRuntime.Free(source);
                
                if (info == null)
                    throw new ArgumentNullException(nameof(info), "Error while getting information from OpenGL GLSL compiler!");
                
                throw new EmissionException(EmissionException.ERR_SHADER, info);
            }
            
            return shader;
        }

        public static void LinkProgram(uint program)
        {
            glLinkProgram(program);

            int status;
            glGetProgramiv(program, GL_LINK_STATUS, &status);
            if (status != GL_TRUE)
            {
                int lenght;
                byte* source = (byte*)CRuntime.Malloc(COMPILE_SHADER_OUTPUT_BUFFER_SIZE);
                glGetProgramInfoLog(program, COMPILE_SHADER_OUTPUT_BUFFER_SIZE, &lenght, source);
                string? info = Memory.PtrToStringUtf8(source, lenght);
                CRuntime.Free(source);

                if (info == null)
                    throw new ArgumentNullException(nameof(info), "Error while getting information from OpenGL GLSL compiler!");
                
                throw new EmissionException(EmissionException.ERR_SHADER, info);
            }
        }
    }
}