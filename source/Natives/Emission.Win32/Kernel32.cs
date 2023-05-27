using System.Runtime.InteropServices;

namespace Emission.Natives.Win32
{
    public class Kernel32
    {
        [DllImport("kernel32", EntryPoint = "LoadLibrary" )]
        public static extern IntPtr LoadLibrary([In] [MarshalAs( UnmanagedType.LPStr )] string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetProcAddress" )]
        public static extern IntPtr GetProcAddress(IntPtr hModule, [In] [MarshalAs( UnmanagedType.LPStr )] string lpProcName);
    }
}
