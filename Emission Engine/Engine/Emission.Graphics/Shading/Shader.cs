using System;

using Emission.IO;
using static Emission.Graphics.GL;
using Emission.Mathematics.Numerics;

namespace Emission.Shading
{
    public class Shader : IDisposable
    {
        public const string UNIFORM_TRANSFORM = "uTransform";
        public const string UNIFORM_VIEW = "uView";
        public const string UNIFORM_PROJECTION = "uProjection";
    
        // public variables
        public string Name { get; private set; }
        public uint Program {  get => _program; }

        // private variables
        private uint _program;
        private uint _vertex;
        private uint _fragment;

        // constructor
        public Shader(string path)
        {
            (string vertex, string fragment) file = ParseShader(path);
            Load(file.vertex, file.fragment);
        }
        
        // constructor
        public Shader(string vertex, string fragment)
        {
            Load(vertex, fragment);
        }

        /// <summary>
        /// Enable shader to be use.
        /// </summary>
        public void Start()
        {
            glUseProgram(_program);
        }

        /// <summary>
        /// Disable shader to be use.
        /// </summary>
        public void Stop()
        {
            glUseProgram(0);
        }

        /// <summary>
        /// Destroy Shader.
        /// </summary>
        public void Dispose()
        {
            glDeleteProgram(_program);
        }

        /// <summary>
        /// Return attribute location use a name.
        /// </summary>
        /// <param name="name">Attribute's name</param>
        /// <returns>Attribute's location</returns>
        public int GetAttributeLocation(string name)
        {
            return glGetAttribLocation(_program, name);
        }
        
        /// <summary>
        /// Return uniform location use a name.
        /// </summary>
        /// <param name="name">Uniform's name</param>
        /// <returns>Uniform's location</returns>
        public int GetUniformLocation(string name)
        {
            int location = glGetUniformLocation(_program, name);
            //if (location == -1) Debug.LogColor($"[WARNING] '{name}' does not exists.", ConsoleColor.Yellow);
            return location;
        }
        
        /// <summary>
        /// Define a float value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniform1f(string name, float value)
        {
            glUniform1f(GetUniformLocation(name), value);
        }
        
        /// <summary>
        /// Define an int value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniform1(string name, int value)
        {
            glUniform1i(GetUniformLocation(name), value);
        }
        
        /// <summary>
        /// Define a vector 2D value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformVec2(string name, Vector2 value)
        {
            glUniform2f(GetUniformLocation(name), value.X, value.Y);
        }
        
        /// <summary>
        /// Define a vector 3D value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformVec3(string name, Vector3 value)
        {
            glUniform3f(GetUniformLocation(name), value.X, value.Y, value.Z);
        }
        
        /// <summary>
        /// Define a vector 3D value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformVec4(string name, Vector4 value)
        {
            glUniform4f(GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
        }

        /// <summary>
        /// Define a matrix 4 value to a uniform use to define a projection.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformProjectionMat4(string name, Matrix4 value)
        {
            glUniformMatrix4fv(GetUniformLocation(name), 1, false, value.ToArray());
        }

        /// <summary>
        /// Define a matrix 4 value to a uniform use to define a transposition.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformMat4(string name, Matrix4 value)
        {
            glUniformMatrix4fv(GetUniformLocation(name), 1, true, value.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="fragment"></param>
        protected void Load(string vertex, string fragment)
        {
            _vertex = LoadShader(GL_VERTEX_SHADER, ShaderType.Vertex, vertex);
            _fragment = LoadShader(GL_FRAGMENT_SHADER, ShaderType.Fragment, fragment);

            _program = glCreateProgram();
            Name = "shader" + _program;
            
            glAttachShader(_program, _vertex);
            glAttachShader(_program, _fragment);

            glLinkProgram(_program);

            glDeleteShader(_vertex);
            glDeleteShader(_fragment);
            
            Debug.Log($"[INFO] {Name} initialized!");
        }

        /// <summary>
        /// OpenGL Loading of a shader.
        /// Type of shade is defined using <see cref="ShaderType"/>.
        /// </summary>
        /// <param name="type">type of loading shader</param>
        /// <param name="content">shader string content</param>
        /// <returns></returns>
        protected uint LoadShader(int type, ShaderType enumType, string content)
        {
            uint shader = glCreateShader((int)type);
            glShaderSource(shader, content);
            glCompileShader(shader);

            string shaderLogs = glGetShaderInfoLog(shader);
            if (shaderLogs != "") throw new EmissionException(EmissionException.EmissionOpenGlException, shaderLogs);
            
            Debug.Log($"[SHADER] Successfully compile {enumType} shader!");

            return shader;
        }

        /// <summary>
        /// Recreate full shader file using splited file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected static (string vertex, string fragment) ParseShader(string path)
        {
            string[] data = SplitShader(path);
            return (data[2] + data[0], data[2] + data[1]);
        }

        /// <summary>
        /// Parse shader file in order to separate vertex part and fragment part use keywords
        /// 'vertex:' and 'fragment:' or 'define:'.
        /// Read file using path.
        /// </summary>
        /// <param name="path">Path to shader file</param>
        /// <returns>Vertex and fragment shader content</returns>
        protected static string[] SplitShader(string path)
        {
            string[] lines = File.ReadLines(path);
            string[] content = new string[3];
            var type = ShaderType.None;

            void WriteToShader(string line, ShaderType shaderType)
            {
                switch (shaderType)
                {
                    // Add line to vertex content at array position
                    case ShaderType.Vertex:
                        content[0] += line + "\n";
                        break;
                    case ShaderType.Fragment:
                        content[1] += line + "\n";
                        break;
                    case ShaderType.Define:
                        content[2] += line + "\n";
                        break;
                }
            }
            
            foreach(string line in lines)
            {
                if (line.Contains(":") && !line.StartsWith("//"))
                {
                    // Define shader type by checking if line contain attribute

                    // Define type for vertex shader
                    if (line.Contains("vertex:")) type = ShaderType.Vertex;

                    // Define type for fragment shader
                    else if (line.Contains("fragment:")) type = ShaderType.Fragment;
                    
                    // Define header statement
                    else if (line.Contains("define:")) type = ShaderType.Define;
                }
                else if (line.Contains("#"))
                {
                    string[] data = line.Replace("#", "").Trim().Split(' ');
                    // check special calls
                    switch (data[0])
                    {
                        // import code from other file
                        case "include":
                            var include = SplitShader(data[1]);
                            content[2] += include[2] + '\n';
                            WriteToShader(type == ShaderType.Vertex ? include[0] : include[1], type);
                            break;
                        
                        // define shader version
                        case "version":
                            WriteToShader(line, type);
                            break;
                    }
                }
                else
                {
                    WriteToShader(line, type);
                }
            }

            // Retun a struct that contains all parsed content
            return (content);
        }
    }
}

