using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.Shading
{
    public class Material : IDisposable
    {
        // public variables
        public Shader Shader { get; }
        public string Name;

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = Math.Clamp(value, 0, 1);
            }
        }
        
        public Texture DiffuseMap
        {
            get => _diffuseMap;
            set
            {
                _diffuseMap = value;
                _diffuseMap.Bind();
            }
        }
        public Texture SpecularMap
        {
            get => _specularMap;
            set
            {
                _specularMap = value;
                _specularMap.Bind();
            }
        }
        public Texture[] Textures { get => _textures.ToArray(); }

        // privates variables
        private List<Texture> _textures;
        private Texture _diffuseMap;
        private Texture _specularMap;

        private float _alpha;

        // constructors
        public Material(string name, Material mat) : this(name, mat.Shader, mat._textures)  { }
        public Material(string name, string path) : this(name, new Shader(path), new List<Texture>()) { }
        public Material(string name, Shader shader) : this(name,shader, new List<Texture>()) { }

        public Material(string name, Shader shader, List<Texture> textures)
        {
            Name = name;
            Shader = shader;
            Alpha = 1;
            _textures = textures;
        }

        public virtual void Start()
        {
            Shader.Start();
        }

        public virtual void Update()
        {
            /*Transform cameraTransform = Camera.Main.Transform;
            Shader.UseUniformVec3("camera.position", cameraTransform.Position);
            Shader.UseUniformVec3("camera.rotation", cameraTransform.EulerAngle);*/
            Shader.UseUniform1f("alpha", Alpha);
        }

        public virtual void Stop()
        {
            Shader.Stop();
        }

        public virtual void Dispose()
        {
            foreach (var t in _textures) t.Dispose();
            Shader.Dispose();
        }

        /// <summary>
        /// Add texture to shader by using an image loaded by the relative path.
        /// Apply texture to a specific named texture sampler and with a unit.
        /// </summary>
        /// <param name="path">Path to image</param>
        /// <param name="name">Location name</param>
        /// <param name="unit">Texture Unit</param>
        public Material BindTexture(Texture texture)
        {
            texture.Use();
            _textures.Add(texture);
            return this;
        }
        
        /// <summary>
        /// Add texture to shader by using an image loaded by the relative path.
        /// Apply texture to a specific named texture sampler and with a unit.
        /// </summary>
        /// <param name="path">Path to image</param>
        /// <param name="name">Location name</param>
        /// <param name="unit">Texture Unit</param>
        public Material BindTexture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            BindTexture(new Texture(path, name, unit));
            return this;
        }

        /// <summary>
        /// Apply texture to object.
        /// </summary>
        public void BindTextures()
        {
            foreach (Texture texture in _textures) texture.Bind();
        }

        /// <summary>
        /// Start and load texture in order to use them.
        /// </summary>
        public void UseTextures()
        {
            foreach (Texture texture in _textures)
            {
                texture.Use();
                Shader.UseUniform1(texture.Name, _textures.IndexOf(texture));
            }
            
            try
            {
                _diffuseMap.Use();
                _specularMap.Use();
            }
            catch(NullReferenceException){}
        }

        /// <summary>
        /// Define an image that define how light is diffuse on the object.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="unit"></param>
        public Material BindDiffuseMap(string path, TextureUnit unit = TextureUnit.Texture1)
        {
            DiffuseMap = new Texture(path, "material.diffuseMap", unit);
            DiffuseMap.Bind();
            return this;
        }
        
        /// <summary>
        /// Define an image that define how light is reflect on the object.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="unit"></param>
        public Material BindSpecularMap(string path, TextureUnit unit = TextureUnit.Texture2)
        {
            SpecularMap = new Texture(path, "material.specularMap", unit);
            SpecularMap.Bind();
            return this;
        }

        /// <summary>
        /// Define a float value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniform1f(string name, float value)
        {
            Shader.Start();
            Shader.UseUniform1f(name, value);
            Shader.Stop();
        }
        
        /// <summary>
        /// Define an int value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniform1(string name, int value)
        {
            Shader.Start();
            Shader.UseUniform1(name, value);
            Shader.Stop();
        }
        
        /// <summary>
        /// Define a vector 2D value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformVec2(string name, Vector2 value)
        {
            Shader.Start();
            Shader.UseUniformVec2(name, value);
            Shader.Stop();
        }
        
        /// <summary>
        /// Define a vector 3D value to a uniform.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformVec3(string name, Vector3 value)
        {
            Shader.Start();
            Shader.UseUniformVec3(name, value);
            Shader.Stop();
        }
        
        /// <summary>
        /// Define a matrix 3 value to a uniform use to define a transposition.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformMat3(string name, Matrix3 value)
        {
            Shader.Start();
            Shader.UseUniformMat3(name, value);
            Shader.Stop();
        }

        /// <summary>
        /// Define a matrix 4 value to a uniform use to define a projection.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformProjectionMat4(string name, Matrix4 value)
        {
            Shader.Start();
            Shader.UseUniformProjectionMat4(name, value);
            Shader.Stop();
        }

        /// <summary>
        /// Define a matrix 4 value to a uniform use to define a transposition.
        /// </summary>
        /// <param name="name">Name of uniform</param>
        /// <param name="value">New value of uniform</param>
        public void UseUniformMat4(string name, Matrix4 value)
        {
            Shader.Start();
            Shader.UseUniformMat4(name, value);
            Shader.Stop();
        }
    }
}