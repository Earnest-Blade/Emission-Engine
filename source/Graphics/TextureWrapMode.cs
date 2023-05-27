using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public enum TextureWrapMode
    {
        Repeat = GL_REPEAT,
        ClampToEdge = GL_CLAMP_TO_EDGE,
        ClampToBorder = GL_CLAMP_TO_BORDER,
        MirrorRepeat = GL_MIRRORED_REPEAT,
        MirrorClamp = GL_MIRROR_CLAMP_TO_EDGE
    }
}
