using Emission.Math;
using Emission.Shading;
using OpenTK.Mathematics;

namespace Emission.Lighting
{
    public class SpotLight : Light
    {
        public float CutOff
        {
            get => Mathf.RadiansToDegrees(_cutOff);
            set => _cutOff = Mathf.DegreesToRadian(value);
        }

        private float _cutOff;
        
        public SpotLight(Vector3 lightPosition, Vector3 color) : base(lightPosition, color)
        {
            CutOff = 1;
            _current = this;
        }

        public override void Update(Shader shader)
        {
            shader.UseUniformVec3("light.color", Color);
            shader.UseUniformVec3("light.position", Transform.Position);
            shader.UseUniformVec3("light.rotation", Transform.EulerAngle);
            shader.UseUniform1f("light.strength", Strength);

            shader.UseUniform1f("light.cutoff", Mathf.Cos(_cutOff));
        }

        public override void SubmitImGui()
        {
            base.SubmitImGui();
            float x = CutOff;
            ImGuiNET.ImGui.SliderFloat("CutOff", ref x, 0, 100);
            CutOff = x;
        }
    }
}
