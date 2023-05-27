namespace Emission.Core
{
    [Flags]
    public enum DebugFlags : uint
    {
        ShowEngineInfo = 0x000,
        ShowEngineWarnings = 0x001,
        ShowGlInfo = 0x002,
        ShowGlWarnings = 0x004,
        ShowGlfwInfo = 0x008,
        ShowGlfwWarnings = 0x010,
        
        HideEngineErrors = 0x20,
        HideGlErrors = 0x40,
        HideGlfwErrors = 0x80,
        
        ShowAll = (ShowEngineInfo | ShowEngineWarnings | ShowGlInfo | ShowGlWarnings | ShowGlfwInfo | ShowGlfwWarnings),
        ShowEngineAll = (ShowEngineInfo | ShowEngineWarnings),
        ShowGlAll = (ShowGlInfo | ShowGlWarnings),
        ShowGlfwAll = (ShowGlfwInfo | ShowGlfwWarnings),
        
        ShowNothing = (HideEngineErrors | HideGlErrors | HideGlfwErrors)
    }
}