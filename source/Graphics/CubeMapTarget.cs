using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public enum CubeMapTarget
    {
        TargetPositiveX = GL_TEXTURE_CUBE_MAP_POSITIVE_X, 
        TargetNegativeX = GL_TEXTURE_CUBE_MAP_NEGATIVE_X, 
        TargetPositiveY = GL_TEXTURE_CUBE_MAP_POSITIVE_Y, 
        TargetNegativeY = GL_TEXTURE_CUBE_MAP_NEGATIVE_Y, 
        TargetPositiveZ = GL_TEXTURE_CUBE_MAP_POSITIVE_Z, 
        TargetNegativeZ = GL_TEXTURE_CUBE_MAP_NEGATIVE_Z
    }
}