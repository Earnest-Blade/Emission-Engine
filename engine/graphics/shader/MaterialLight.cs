using System.Collections.Generic;
using Emission.Lighting;

namespace Emission.Shading
{
    public class MaterialLight : Material
    {
        public float Ambient;
        public float Diffuse;
        public float Specular;
        public float Shininess;
        
        public MaterialLight(string name, Material mat) : this(name, mat.Shader, new List<Texture>(mat.Textures)) { }
        public MaterialLight(string name, string path) : this(name, new Shader(path), new List<Texture>()) { }
        public MaterialLight(string name, Shader shader) : this(name, shader, new List<Texture>()) { }

        public MaterialLight(string name, Shader shader, List<Texture> textures) : base(name, shader, textures)
        {
            Ambient = 0.1f;
            Diffuse = 0.5f;
            Specular = 0.1f;
            Shininess = 32f;
        }

        public override void Update()
        {
            base.Update();

            if(Light.HasLight) Light.Singleton.Update(Shader);

            Shader.UseUniform1f($"{Name}.ambient", Ambient);
            Shader.UseUniform1f($"{Name}.diffuse", Diffuse);
            Shader.UseUniform1f($"{Name}.specular", Specular);
            Shader.UseUniform1f($"{Name}.shininess", Shininess);
        }
    }
}
