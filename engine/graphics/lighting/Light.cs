using Emission.Math;
using Emission.Shading;
using OpenTK.Mathematics;

namespace Emission.Lighting
{
    public abstract class Light : IGuiSubmitable
    {
        protected static Light _current;

        public Transform Transform;
        public Vector3 Color;
        public float Strength;

        public Light(Vector3 lightPosition, Vector3 color)
        {
            Transform = new Transform();
            Transform.Position = lightPosition;
            Color = color;
            Strength = 1;
            
            _current = this;
        }

        public abstract void Update(Shader shader);

        public virtual void SubmitImGui()
        {
            ImGuiNET.ImGui.TextColored(Math.Color.Red, "Light Transform");
            
            var color = new System.Numerics.Vector3(Color.X, Color.Y, Color.Z);
            ImGuiNET.ImGui.ColorEdit3("Light Color", ref color);
            Color = new Vector3(color.X, color.Y, color.Z);

            ImGuiNET.ImGui.SliderFloat("Light Position X", ref Transform.Position.X, -10, 10);
            ImGuiNET.ImGui.SliderFloat("Light Position Y", ref Transform.Position.Y, -10, 10);
            ImGuiNET.ImGui.SliderFloat("Light Position Z", ref Transform.Position.Z, -10, 10);
            
            Vector3 currentRotation = Transform.EulerAngle;
            ImGuiNET.ImGui.SliderFloat("Light Rotation X", ref currentRotation.X, -180, 180);
            ImGuiNET.ImGui.SliderFloat("Light Rotation Y", ref currentRotation.Y, -180, 180);
            ImGuiNET.ImGui.SliderFloat("Light Rotation Z", ref currentRotation.Z, -180, 180);
            Transform.EulerAngle = currentRotation;

            ImGuiNET.ImGui.TextColored(Math.Color.Blue, "Light Parameters");
            ImGuiNET.ImGui.SliderFloat("Light Strength", ref Strength, 0, 20);
        }

        public static Light Singleton => _current;
        public static bool HasLight => _current != null;
    }
}
