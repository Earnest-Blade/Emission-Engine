using System.Collections.Generic;
using Emission.Lighting;
using OpenTK.Mathematics;

namespace Emission.Shading
{
    public class LightMaterial : Material
    {
        public float Ambient;
        public float Diffuse;
        public float Specular;
        public float Shininess;
        
        private Vector3 _ambient;
        private Vector3 _diffuse;
        private Vector3 _specular;
        
        public LightMaterial(Material mat) : this(mat.Shader, new List<Texture>(mat.Textures)) {}
        public LightMaterial(string path) : this(new Shader(path), new List<Texture>()) {}
        public LightMaterial(Shader shader) : this(shader, new List<Texture>()){}

        public LightMaterial(Shader shader, List<Texture> textures) : base(shader, textures)
        {
            Ambient = 0.1f;
            Specular = 0.8f;
            Shininess = 32;
            Diffuse = 1f;
        }

        public override void Update()
        {
            base.Update();

            Shader.UseUniformVec3("uLightColor", Light.Singleton.LightColor);
            Shader.UseUniformVec3("uLightPosition", Light.Singleton.LightPosition);
            Shader.UseUniformVec3("uCameraPosition", Camera.Main.Transform.Position);
            
            Shader.UseUniform1f("uAmbient", Ambient);
            Shader.UseUniform1f("uDiffuse", Diffuse);
            Shader.UseUniform1f("uSpecular", Specular);
            Shader.UseUniform1f("uShininess", Shininess);
        }
    }
}
