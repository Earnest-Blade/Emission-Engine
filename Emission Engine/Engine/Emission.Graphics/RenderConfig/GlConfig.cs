using Emission.Mathematics;

namespace Emission.Graphics.RenderConfig
{
    public class GlConfig : RenderConfig
    {
        public GlConfig() { }

        public override RenderConfig GetDefault()
        {
            Name = "OpenGL";

            IsBlend = true;
            IsClipDistance = false;
            IsCullFace = true;
            IsDepthClamp = false;
            IsDepthTest = true;
            IsDither = false;
            
            ClearColor = ColorRgb.Black;
            return this;
        }
    }
}
