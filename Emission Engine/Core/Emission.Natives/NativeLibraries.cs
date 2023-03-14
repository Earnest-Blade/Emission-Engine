using System;
using System.Runtime.InteropServices;

namespace Emission.Natives
{
    internal static class NativeLibraries
    {
        public const string GLFW = "/Bin/glfw3.dll";
        public const string OPENAL = "/Bin/oal.dll";
        public const string STB = "/Bin/stb.dll";
        
        public const string KERNEL32 = "kernel32";
        public const string USER32 = "user32";
        
        [DllImport(KERNEL32, EntryPoint = "LoadLibrary" )]
        public static extern IntPtr LoadLibrary( [In] [MarshalAs( UnmanagedType.LPStr )] string lpFileName );

        [DllImport(KERNEL32, EntryPoint = "GetProcAddress" )]
        public static extern IntPtr GetProcAddress( IntPtr hModule, [In] [MarshalAs( UnmanagedType.LPStr )] string lpProcName);
    }
}
