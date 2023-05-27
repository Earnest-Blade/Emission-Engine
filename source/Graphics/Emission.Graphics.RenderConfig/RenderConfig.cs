using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Natives.GLFW;

namespace Emission.Graphics.RenderConfig
{
    public abstract class RenderConfig
    {
        public string Name { get; protected set; }
        public ushort Version { get; protected set; }

        public bool IsBlend { get; protected set; }
        public bool IsClipDistance { get; protected set; }
        public bool IsCullFace { get; protected set; }
        public bool IsDepthClamp { get; protected set; }
        public bool IsDepthTest { get; protected set; }
        public bool IsDither { get; protected set; }

        public ColorRgb ClearColor { get; protected set; }
        
        public RenderConfig() { }

        public abstract void Initialize(Glfw.PFNGLFWGETPROCADDRESSPROC address);
        public abstract void InitializeRenderer();
        public abstract RenderConfig GetDefault();
    }
}
