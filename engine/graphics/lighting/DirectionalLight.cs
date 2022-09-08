using Emission.Shading;
using OpenTK.Mathematics;

namespace Emission.Lighting
{
    public class DirectionalLight : Light
    {
        public DirectionalLight(Vector3 lightPosition, Vector3 color) : base(lightPosition, color)
        {
            _current = this;
        }
        
        public override void Update(Shader shader)
        {
            shader.UseUniformVec3("light.color", Color);
            shader.UseUniform1f("light.strength", Strength);
            shader.UseUniformVec3("light.position", Transform.Position);
            shader.UseUniformVec3("light.rotation", Transform.EulerAngle);
        }
    }
}
