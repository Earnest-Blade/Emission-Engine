using System.Collections.Generic;
using System.IO;
using static Emission.Graphics.GL.GL;

namespace Emission.Graphics.Shading
{
    public class ShaderLoader
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
        public static uint CompileShader(int type, ref string data)
        {
            uint shader = glCreateShader(type);

            glShaderSource(shader, data);
            glCompileShader(shader);

            string info = glGetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(info))
            {
                throw new EmissionException(Errors.EmissionOpenGlException, info);
            }

            return shader;
        }

        public struct ShaderStruct
        {
            public string VertexData;
            public string GeomertyData;
            public string FragmentData;

            public string TCSData;
            public string TESData;

            public bool HasVertexShader = false;
            public bool HasFragmentShader = false;
            public bool HasGeometryShade = false;
            public bool HasTesselationShader = false;

            public ShaderStruct()
            {
                VertexData = null;
                GeomertyData = null;
                FragmentData = null;
                TCSData = null;
                TESData = null;
            }

            public ShaderStruct(string vertexData, string fragmentData)
            {
                VertexData = vertexData;
                GeomertyData = null;
                FragmentData = fragmentData;
                TCSData = null;
                TESData = null;
            }

            public ShaderStruct(string vertexData, string geomertyData, string fragmentData)
            {
                VertexData = vertexData;
                GeomertyData = geomertyData;
                FragmentData = fragmentData;
                TCSData = null;
                TESData = null;
            }

            public void Write(int type, string line)
            {
                switch (type)
                {
                    case NONE:
                        return;

                    case DEFINE_SHADER:
                        VertexData += line + '\n';
                        GeomertyData += line + '\n';
                        FragmentData += line + '\n';
                        return;

                    case VERTEX_SHADER:
                        HasVertexShader = true;
                        VertexData += line + '\n';
                        return;

                    case GEOMETRY_SHADER:
                        HasGeometryShade = true;
                        GeomertyData += line + '\n';
                        return;

                    case FRAGMENT_SHADER:
                        HasFragmentShader = true;
                        FragmentData += line + '\n';
                        return;

                    case TESSELATION_CONTROL_SHADER:
                        HasTesselationShader = true;
                        TCSData = line + '\n';
                        return;

                    case TESSELATION_EVAL_SHADER:
                        HasTesselationShader = true;
                        TESData = line + '\n';
                        return;
                }
            }
        }
    }
}