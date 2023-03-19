using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Emission.IO;

namespace Emission.Natives
{
    internal static class NativeLibraries
    {
        public const string GLFW = "glfw3.dll";
        public const string OPENAL = "oal.dll";
        public const string STB = "stb.dll";
        
        public const string KERNEL32 = "kernel32";
        public const string USER32 = "user32";
        
        [DllImport(KERNEL32, EntryPoint = "LoadLibrary" )]
        public static extern IntPtr LoadLibrary([In] [MarshalAs( UnmanagedType.LPStr )] string lpFileName);

        [DllImport(KERNEL32, EntryPoint = "GetProcAddress" )]
        public static extern IntPtr GetProcAddress(IntPtr hModule, [In] [MarshalAs( UnmanagedType.LPStr )] string lpProcName);

        public static string GetLibraryPath(string library)
        {
            return GameDirectory.GetDirectoryFromFilePath(typeof(NativeLibraries).Assembly.Location) + library;
        }
    }
}
