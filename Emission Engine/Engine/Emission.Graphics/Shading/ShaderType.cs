using System;

namespace Emission.Graphics.Shading
{
    [Flags]
    public enum ShaderType
    {
        None = 0,
        Vertex = 0x1,
        Fragment = 0x2,
        Define = 0x3
    }
}
