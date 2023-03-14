using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Emission.Natives.GL;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public static unsafe class ShaderLoader
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
                    if (line.Contains(DEFINE_KEY)) type = DEFINE_SHADER; 
                    if (line.Contains(VERTEX_SHADER_KEY)) type = VERTEX_SHADER; 
                    if (line.Contains(GEOMETRY_SHADER_KEY)) type = GEOMETRY_SHADER; 
                    if (line.Contains(FRAGMENT_SHADER_KEY)) type = FRAGMENT_SHADER; 
                    if (line.Contains(TCS_SHADER_KEY)) type = TESSELATION_CONTROL_SHADER;
                    if (line.Contains(TES_SHADER_KEY)) type = TESSELATION_EVAL_SHADER;
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
            uint shader = glCreateShader((uint)type);

            GlUtils.StrToByteArrayPtr(data, out byte** ptr, out byte[] buffer);
            int length = buffer.Length;
            glShaderSource(shader, 1, ptr, &length);

            glCompileShader(shader);

            int shaderStatus;
            glGetShaderiv(shader, GL_COMPILE_STATUS, &shaderStatus);

            if (shaderStatus != GL_TRUE)
            {
                string info = GetShaderInfo(shader);
                throw new EmissionException(EmissionErrors.EmissionOpenGlException, info);
            }
            
            return shader;
        }

        private static string GetShaderInfo(uint program, int bufferSize = 1064)
        {
            IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
            int length;
            byte* source = (byte*)buffer.ToPointer();
            glGetProgramInfoLog(program, bufferSize, &length, source);
            string info = Marshal.PtrToStringUTF8(buffer, length);
            
            Marshal.FreeHGlobal(buffer);
            return info;
        }

        
    }
}