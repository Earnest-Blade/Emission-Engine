using System;

using Emission.Toolbox;
using OpenTK.Mathematics;

namespace Emission.Lighting
{
    public class Light : IGuiSubmitable
    {
        private static Light _current;
        
        public Vector3 LightPosition;
        public Vector3 LightColor;
            
        public Light(Vector3 lightPosition, Vector3 lightColor)
        {
            LightPosition = lightPosition;
            LightColor = lightColor;
            
            _current = this;
        }
        
        public void SubmitImGui()
        {
            ImGuiNET.ImGui.TextColored(ColorHelper.RGB(255, 236, 0), "Current Light");
            
            var color = new System.Numerics.Vector3(LightColor.X, LightColor.Y, LightColor.Z);
            ImGuiNET.ImGui.ColorPicker3("Light Color", ref color);
            LightColor = new Vector3(color.X, color.Y, color.Z);

            var pos = NumericsHelper.Vector3(LightPosition);
            ImGuiNET.ImGui.SliderFloat3("Position", ref pos, -10, 10);
            LightPosition = NumericsHelper.Vector3(pos);
        }

        public static Light Singleton
        {
            get => _current;
        }
    }
}
