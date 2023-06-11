using Emission.Core;
using Emission.Core.Memory;
using Emission.Core.Mathematics;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe partial class Shader : IEquatable<Shader>, IDisposable
    {
        /// <summary>
        /// Conventional name use to represent actor's transformation matrix in the shader. 'uTransform'
        /// </summary>
        public const string UNIFORM_TRANSFORM = "uTransform";
        
        /// <summary>
        /// Conventional name use to represent camera's view matrix in the shader. 'uView'
        /// </summary>
        public const string UNIFORM_VIEW = "uView";
        
        /// <summary>
        /// Conventional name use to represent camera's projection matrix in the shader. 'uProjection'
        /// </summary>
        public const string UNIFORM_PROJECTION = "uProjection";
        
        /// <summary>
        /// Conventional name use to represent screen size in the shader. 'uWindowResolution'
        /// </summary>
        public const string UNIFORM_WINDOW_RESOLUTION = "uWindowResolution";

        // public variables
        public string? Name => _name;
        public uint ID => _program;
        
        // private variables
        private string? _name;
        private uint _program;
        private uint _vertex;
        private uint _fragment;
        private uint _geometry;

#if EMISSION_ENABLE_TESSELATION
        private uint _tcs;
        private uint _tes;
#endif

        private static uint _activeShader;
        
        // constructor
        private Shader(ShaderStruct shaderStruct) : this(shaderStruct, null) {}
        private Shader(ShaderStruct shaderStruct, string? name)
        {
            _name = name;
            
            Initialize(shaderStruct);
        }

        /// <summary>
        /// Initialize a shader from a <seealso cref="ShaderStruct"/>.
        /// Create program ID, load vertex shader, fragment shader and geometry shader.
        /// </summary>
        /// <param name="shader"></param>
        private void Initialize(ShaderStruct shader)
        {
            // Catch if we trying to re-init the shader. 
            if(_program != 0)
            {
                Debug.LogWarning($"[WARNING] Shader '{Name}' is already initialize!");
                return;
            }

            // Create a new program
            _program = glCreateProgram();
            _activeShader = _program;
            
            if (_name == null) _name = $"shader{_program}";
            else _name += _program.ToString();
            
            Debug.Log($"[INFO] Started created {_name}");

            if (shader.HasVertexShader)
            {
                Debug.Log("[INFO] Compiling a new vertex shader!");
                _vertex = ShaderBuilder.CompileShader(ShaderBuilder.VERTEX_SHADER, shader.VertexData);
                glAttachShader(_program, _vertex);
            }

            if (shader.HasFragmentShader)
            { 
                Debug.Log("[INFO] Compiling a new fragment shader!");
                _fragment = ShaderBuilder.CompileShader(ShaderBuilder.FRAGMENT_SHADER, shader.FragmentData);
                glAttachShader(_program, _fragment);
            }

            if (shader.HasGeometryShade)
            {
                Debug.Log("[INFO] Compiling a new geometry shader!");
                _geometry = ShaderBuilder.CompileShader(ShaderBuilder.GEOMETRY_SHADER, shader.GeomertyData);
                glAttachShader(_program, _geometry);
            }
            
#if EMISSION_ENABLE_TESSELATION
            if (shader.HasTesselationShader)
            {
                Debug.Log("[INFO] Compiling a new tesselation shader!");
                
                glPatchParameteri(GL_PATCH_VERTICES, 3);

                _tcs = ShaderBuilder.CompileShader(ShaderBuilder.TESSELATION_CONTROL_SHADER, shader.TCSData);
                glAttachShader(_program, _tcs);

                _tes = ShaderBuilder.CompileShader(ShaderBuilder.TESSELATION_EVAL_SHADER, shader.TESData);
                glAttachShader(_program, _tes);
            }
#endif
            ShaderBuilder.LinkProgram(_program);
            
            glDeleteShader(_vertex);
            glDeleteShader(_fragment);
            glDeleteShader(_geometry);
            
#if EMISSION_ENABLE_TESSELATION
            glDeleteShader(_tcs);
            glDeleteShader(_tes);
#endif

            _activeShader = 0;
            glUseProgram(0);
            Debug.Log($"[INFO] Successfully compile shader '{Name}'");
        }

        /// <summary>
        /// Enable shader to be use.
        /// </summary>
        public void Start()
        {
            _activeShader = _program;
            glUseProgram(_program);
        }

        /// <summary>
        /// Disable shader to be use.
        /// </summary>
        public void Stop()
        {
            glUseProgram(0);
            _activeShader = 0;
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
            return glGetAttribLocation(_program, Memory.StrUtf8ToBytePtr(name));
        }
        
        /// <summary>
        /// Return uniform location use a name.
        /// </summary>
        /// <param name="name">Uniform's name</param>
        /// <returns>Uniform's location</returns>
        public int GetUniformLocation(string name)
        {
            return glGetUniformLocation(_program, Memory.StrUtf8ToBytePtr(name));
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
        /// Define a vector 2D to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        public void UseUniformVec2(string name, float x, float y)
        {
            glUniform2f(GetUniformLocation(name), x, y);
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