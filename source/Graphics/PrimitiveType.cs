using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public enum PrimitiveType : uint
    {
        Point = GL_POINT,
        
        Line = GL_LINE,
        LineAdjacency = GL_LINES_ADJACENCY,
        LineStrip = GL_LINE_STRIP,
        LineStripAdjacency = GL_LINE_STRIP_ADJACENCY,
        LineLoop = GL_LINE_LOOP,
        
        Triangle = GL_TRIANGLES,
        TriangleAdjacency = GL_TRIANGLES_ADJACENCY,
        TriangleStrip = GL_TRIANGLE_STRIP,
        TriangleStripAdjacency = GL_TRIANGLE_STRIP_ADJACENCY,
        TriangleFan = GL_TRIANGLE_FAN,
        
        Quads = GL_QUADS,
        
        Patches = GL_PATCHES
    }
}
