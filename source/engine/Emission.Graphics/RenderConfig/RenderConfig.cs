using Emission.Mathematics;

namespace Emission.Graphics.RenderConfig
{
    public abstract class RenderConfig
    {
        public string Name { get; set; }
        public ushort Version { get; set; }

        public bool IsBlend { get; set; }
        public bool IsClipDistance { get; set; }
        public bool IsCullFace { get; set; }
        public bool IsDepthClamp { get; set; }
        public bool IsDepthTest { get; set; }
        public bool IsDither { get; set; }

        public ColorRgb ClearColor { get; set; }

        public RenderConfig() { }

        public abstract RenderConfig GetDefault();
    }
}
