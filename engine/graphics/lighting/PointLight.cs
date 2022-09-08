using Emission.Math;
using Emission.Shading;
using OpenTK.Mathematics;

namespace Emission.Lighting
{
    public class PointLight : Light
    {
        public const int MAX_LIGHT_RANGE = 3250;
        
        public float Range
        {
            set
            {
                value = Mathf.Clamp(value, 0, MAX_LIGHT_RANGE);
                _constant = 1.0f;
                _linear = 4.5f / value;
                _quadratic = 75.0f / (value * value);
                _range = value;
            }

            get => _range;
        }
        
        private float _range;
        private float _constant;
        private float _linear;
        private float _quadratic;

        private float _distance;
        private float _radius;
        
        public PointLight(Vector3 lightPosition, Vector3 color) : base(lightPosition, color)
        {
            Range = 1;
            
            _radius = 4;
            _distance = 4;
            _current = this;
        }
        
        public override void Update(Shader shader)
        {
            shader.UseUniformVec3("light.color", Color);
            shader.UseUniformVec3("light.position", Transform.Position);
            shader.UseUniformVec3("light.rotation", Transform.EulerAngle);
            shader.UseUniform1f("light.strength", Strength);

            /*shader.UseUniform1f("light.constant", _constant);
            shader.UseUniform1f("light.linear", _linear);
            shader.UseUniform1f("light.quadratic", _quadratic);*/
            
            //shader.UseUniform1f("light.distance", _distance);
            shader.UseUniform1f("light.radius", _radius);
        }

        public override void SubmitImGui()
        {
            base.SubmitImGui();
            ImGuiNET.ImGui.SliderFloat("Distance", ref _distance, 0, 50);
            ImGuiNET.ImGui.SliderFloat("Radius", ref _radius, 0, 50);
        }
    }
}
