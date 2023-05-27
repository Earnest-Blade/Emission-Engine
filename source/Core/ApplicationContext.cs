using System.Runtime.InteropServices;

namespace Emission.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ApplicationContext
    {
        public string Name;
        public byte VersionMajor;
        public byte VersionMinor;
        public byte Patch;
        
        public int Framerate;
        public bool IsDebug;
        public DebugFlags DebugFlags;
        
        public long* Window;
        
        public Debug? Debugger;

        public override string ToString()
        {
            return $"Emission Engine - {VersionMajor}.{VersionMinor}.{Patch}";
        }
    }
}
