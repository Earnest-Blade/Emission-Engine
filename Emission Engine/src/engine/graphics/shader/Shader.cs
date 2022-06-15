using System;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.Shading
{
    class Shader
    {
        public string Name { get; }

        private int _program;
        private int _vertex;
        private int _fragment;

        public Shader(string path)
        {
            ShaderFile file = ParseShader(path);

            _vertex = LoadShader(ShaderType.VertexShader, file.VertexString);
            _fragment = LoadShader(ShaderType.FragmentShader, file.FragmentString);

            _program = GL.CreateProgram();
            Name = "shader" + _program;

            GL.AttachShader(_program, _vertex);
            GL.AttachShader(_program, _fragment);

            GL.LinkProgram(_program);

            GL.DeleteShader(_vertex);
            GL.DeleteShader(_fragment);

            ApplicationConsole.Print("[INFO] " + Name + " initialized!");
        }

        public void Start()
        {
            GL.UseProgram(_program);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public void Destroy()
        {
            GL.DeleteProgram(_program);
        }

        public void UseUniform1f(string name, float value)
        {
            GL.Uniform1(GL.GetUniformLocation(_program, name), value);
        }

        public void UseUniformProjectionMat4(string name, Matrix4 value)
        {
            GL.UniformMatrix4(GL.GetUniformLocation(_program, name), false, ref value);
        }

        public void UseUniformMat4(string name, Matrix4 value)
        {
            GL.UniformMatrix4(GL.GetUniformLocation(_program, name), true, ref value);
        }

        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(_program, name);
        }

        protected int LoadShader(ShaderType type, string content)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, content);
            GL.CompileShader(shader);

            GL.GetShaderInfoLog(shader, out string shaderLogs);
            if (shaderLogs != "") ApplicationConsole.PrintError("[SHADER] " + shaderLogs);
            else ApplicationConsole.Print("[SHADER] Succefully compile shader!");

            return shader;
        }

        protected ShaderFile ParseShader(string path)
        {
            string[] lines = Resources.GetAllLines(path);
            string[] content = new string[2];
            ShaderType type = ShaderType.GeometryShader;
            
            foreach(string line in lines)
            {
                if (line.Contains(":") && !line.StartsWith("//"))
                {
                    // Define shader type by checking if line contain attribut

                    // Define type for vertex shader
                    if (line.Contains("vertex:")) type = ShaderType.VertexShader;

                    // Define type for fragment shader
                    else if (line.Contains("fragment:")) type = ShaderType.FragmentShader;
                }
                else
                {
                    // Normal reading

                    // Add line to vertex content at array position
                    // (content[0] -> vertex content)
                    if (type == ShaderType.VertexShader)
                    {
                        content[0] += line + "\n";
                    }

                    // Add line to fragment content at array position
                    // (content[1] -> fragment content)
                    else if (type == ShaderType.FragmentShader)
                    {
                        content[1] += line + "\n";
                    }
                }
            }

            // Retun a struct that contains all parsed content
            return new ShaderFile
            {
                VertexString = content[0],
                FragmentString = content[1]
            };
        }

    }

    struct ShaderFile
    {
        public string VertexString;
        public string FragmentString;
    }
}

