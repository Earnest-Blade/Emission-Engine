using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.Shading
{
    public class Material : IDisposable
    {
        public Shader Shader { get; }
        public Texture[] Textures { get => _textures.ToArray(); }

        private List<Texture> _textures;

        public Material(Material mat) : this(mat.Shader, mat._textures)  { }
        public Material(string path) : this(new Shader(path), new List<Texture>()) { }
        
        public Material(Shader shader) : this(shader, new List<Texture>()) { }

        public Material(Shader shader, List<Texture> textures)
        {
            Shader = shader;
            
            _textures = textures;
        }

        public virtual void Start()
        {
            Shader.Start();
        }

        public virtual void Update()
        {
            
        }

        public virtual void Stop()
        {
            Shader.Stop();
        }

        public virtual void Dispose()
        {
            Shader.Dispose();
            foreach (Texture t in _textures) t.Dispose();
        }

        public void BindTexture(Texture texture)
        {
            texture.Use();
            _textures.Add(texture);
        }

        public void BindTexture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            BindTexture(new Texture(path, name, unit));
        }

        public void BindTextures()
        {
            foreach (Texture texture in _textures) texture.Bind(); 
        }

        public void UseTextures()
        {
            foreach (Texture texture in _textures)
            {
                texture.Use();
                Shader.UseUniform1(texture.Name, _textures.IndexOf(texture));
            }
        }
    }
}