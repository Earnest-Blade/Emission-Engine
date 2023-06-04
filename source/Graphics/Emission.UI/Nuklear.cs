using Emission.Core;
using Emission.Core.Memory;
using Emission.Natives.GLFW;
using nuklear;
using static nuklear.nuklear;

namespace Emission.Graphics.UI
{
    public class Nuklear
    {
        public static NkContext ActiveContext;
        
        private const uint DEMO_FLAGS = (uint)NkPanelFlags.NK_WINDOW_BORDER | (uint)NkPanelFlags.NK_WINDOW_MOVABLE | (uint)NkPanelFlags.NK_WINDOW_TITLE | (uint)NkPanelFlags.NK_WINDOW_NO_SCROLLBAR;

        public static void DrawDemo()
        {
            if (NkBegin(ActiveContext, "Demo Window", nk_rect(0, 0, 300, 150), DEMO_FLAGS))
            {
                NkLayoutRowStatic(ActiveContext, 10, 300, 1);
                NkLabel(ActiveContext, $"Fps: {Time.Fps}", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                NkLabel(ActiveContext, $"Delta Time: {Time.DeltaTime} ms", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
                //NkLabel(Context, $"Memory: {GC.GetTotalMemory(false)} bytes", (uint)NkTextAlign.NK_TEXT_ALIGN_LEFT);
            }
            
            NkEnd(ActiveContext);
        }
    }
}