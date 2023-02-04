using System;
using Emission.IO;
using Emission.Mathematics;
using Emission.Natives.GL;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics.Shading
{
    public unsafe class Shader : IEquatable<Shader>, IDisposable
    {
        public const string UNIFORM_TRANSFORM = "uTransform";
        public const string UNIFORM_VIEW = "uView";
        public const string UNIFORM_PROJECTION = "uProjection";

        // public variables
        public string Name => _name;
        public uint ID => _program;
        
        // private variables
        private string _name;
        private uint _program;
        private uint _vertex;
        private uint _fragment;
        private uint _geometry;

        private uint _tcs;
        private uint _tes;

        // constructor
        public Shader(string path) : this(ShaderLoader.LoadShader(GameFile.ReadLines(path)), null) {}
        public Shader(string path, string name) : this(ShaderLoader.LoadShader(GameFile.ReadLines(path)), name) {}
        
        // constructor
        public Shader(ShaderLoader.ShaderStruct shaderStruct) : this(shaderStruct, null) {}
        public Shader(ShaderLoader.ShaderStruct shaderStruct, string name)
        {
            _name = name;
            
            Initialize(shaderStruct);
        }

        /// <summary>
        /// Initialize a shader from a <seealso cref="ShaderLoader.ShaderStruct"/>.
        /// Create program ID, load vertex shader, fragment shader and geometry shader.
        /// </summary>
        /// <param name="shader"></param>
        public void Initialize(ShaderLoader.ShaderStruct shader)
        {
            // Catch when trying to re-init the shader. 
            if(_program != 0)
            {
                Debug.Warning($"[WARNING] Shader '{Name}' is already initialize!");
                return;
            }

            _program = glCreateProgram();

            if (shader.HasVertexShader)
            {
                _vertex = ShaderLoader.CompileShader(ShaderLoader.VERTEX_SHADER, ref shader.VertexData);
                glAttachShader(_program, _vertex);
            }

            if (shader.HasFragmentShader)
            { 
                _fragment = ShaderLoader.CompileShader(ShaderLoader.FRAGMENT_SHADER, ref shader.FragmentData);
                glAttachShader(_program, _fragment);
            }

            if (shader.HasGeometryShade)
            {
                _geometry = ShaderLoader.CompileShader(ShaderLoader.GEOMETRY_SHADER, ref shader.GeomertyData);
                glAttachShader(_program, _geometry);
            }

            if (shader.HasTesselationShader)
            {
                glPatchParameteri(GL_PATCH_VERTICES, 3);

                _tcs = ShaderLoader.CompileShader(ShaderLoader.TESSELATION_CONTROL_SHADER, ref shader.TCSData);
                glAttachShader(_program, _tcs);

                _tes = ShaderLoader.CompileShader(ShaderLoader.TESSELATION_EVAL_SHADER, ref shader.TESData);
                glAttachShader(_program, _tes);
            }

            glLinkProgram(_program);

            glDeleteShader(_vertex);
            glDeleteShader(_fragment);
            glDeleteShader(_geometry);
            glDeleteShader(_tcs);
            glDeleteShader(_tes);

            if (_name == null) _name = $"shader{_program}";
            else _name += _program.ToString();

            Debug.Log($"[INFO] Successfully compile shader '{Name}'");
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
            return glGetAttribLocation(_program, GlUtils.StrToBytePtr(name));
        }
        
        /// <summary>
        /// Return uniform location use a name.
        /// </summary>
        /// <param name="name">Uniform's name</param>
        /// <returns>Uniform's location</returns>
        public int GetUniformLocation(string name)
        {
            return glGetUniformLocation(_program, GlUtils.StrToBytePtr(name));
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
            fixed (float* v = &value.ToArray()[0])
                glUniformMatrix4fv(GetUniformLocation(name), 1, false, v);
        }

        /// <summary>
        /// Define a matrix 4 value to a uniform use to define a transposition.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformMat4(string name, Matrix4 value)
        {
            fixed (float* v = &value.ToArray()[0])
                glUniformMatrix4fv(GetUniformLocation(name), 1, true, v);
        }

        public bool Equals(Shader other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _program == other._program && _vertex == other._vertex && _fragment == other._fragment;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Shader)obj);
        }

        public override int GetHashCode()
        {
            return _program.GetHashCode() + _vertex.GetHashCode() + _fragment.GetHashCode();
        }
    }
}

