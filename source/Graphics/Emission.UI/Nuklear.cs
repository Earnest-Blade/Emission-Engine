using Emission.Core;
using nuklear;

namespace Emission.Graphics.Emission.UI
{
    public unsafe class Nuklear
    {
        public static NkContext Context => _nuklearContext;
        
        private static NkContext _nuklearContext;

        public static void Initialize(long* handle, int maxVertexBuffer, int maxElementBuffer)
        {
            _nuklearContext = nuklear.nuklear.NkCreateGlfwContext(handle, maxVertexBuffer, maxElementBuffer);
            
            nuklear.nuklear.NkBeginFontAtlas();
            
            // TODO: Load custom font
            
            nuklear.nuklear.NkEndFontAtlas();
        }
        
        public static void PrepareRendering()
        {
            nuklear.nuklear.NkNewFrame();
        }

        public static void Render()
        {
            nuklear.nuklear.NkRender(false);
        }

        public static void Destroy()
        {
            nuklear.nuklear.NkShutdown();
        }

        public static void DrawDemo()
        {
            if (nuklear.nuklear.NkBegin(_nuklearContext, "Window", nuklear.nuklear.nk_rect(0, 0, 500, 500), 0))
            {
                
            }
            
            nuklear.nuklear.NkEnd(_nuklearContext);
        }
    }
}