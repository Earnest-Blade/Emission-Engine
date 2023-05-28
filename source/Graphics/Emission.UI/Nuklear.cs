using Emission.Core;
using Emission.Core.Memory;
using Emission.Natives.GLFW;
using nuklear;

namespace Emission.Graphics.Emission.UI
{
    public unsafe class Nuklear
    {
        public static NkContext Context => _nuklearContext;
        
        private static NkContext _nuklearContext;
        private const uint DEMO_FLAGS = (uint)NkPanelFlags.NK_WINDOW_BORDER | (uint)NkPanelFlags.NK_WINDOW_MOVABLE | (uint)NkPanelFlags.NK_WINDOW_CLOSABLE;

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
            if (nuklear.nuklear.NkBegin(_nuklearContext, "Window", nuklear.nuklear.nk_rect(0, 0, 250, 150), DEMO_FLAGS))
            {
                nuklear.nuklear.NkLayoutRowStatic(Context, 10, 250, 1);
                nuklear.nuklear.NkLabel(Context, $"Fps: {Time.Fps}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                nuklear.nuklear.NkLabel(Context, $"Delta Time: {Time.DeltaTime}ms", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                nuklear.nuklear.NkLabel(Context, $"Memory: {GC.GetTotalAllocatedBytes()} bytes", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
            }
            
            nuklear.nuklear.NkEnd(_nuklearContext);
        }
    }
}