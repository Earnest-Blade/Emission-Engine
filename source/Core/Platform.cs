using System.Runtime.InteropServices;

namespace Emission.Core
{
    public static class Platform
    {
        public static PlatformType Type
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return PlatformType.Windows;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return PlatformType.Linux;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return PlatformType.MacOS;
            
                return PlatformType.Unknown;
            }
        }
    }
    
    public enum PlatformType
    {
        Windows, Linux, MacOS, Unknown
    }
}
