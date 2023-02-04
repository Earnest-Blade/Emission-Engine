using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Emission.Natives.Win32
{
    public static class Win32
    {
        private const string User32 = "User32.dll";
        private const string Kernel32 = "Kernel32.dll";
        
        public const int WS_BORDER = 0x00800000;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_CHILD = 0x40000000;
        public const int WS_CHILDWINDOW = 0x40000000;
        public const int WS_CLIPCHILDREN = 0x02000000;
        public const int WS_CLIPSIBLINGS = 0x04000000;
        public const int WS_DISABLED = 0x08000000;
        public const int WS_DLGFRAME = 0x00400000;
        public const int WS_GROUP = 0x00020000;
        public const int WS_HSCROLL = 0x00100000;
        public const int WS_ICONIC = 0x20000000;
        public const int WS_MAXIMIZE = 0x01000000;
        public const int WS_MAXIMIZEBOX = 0x00010000;
        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_OVERLAPPED = 0x00000000;
        public const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        public const int WS_POPUP = unchecked((int)0x80000000);
        public const int WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU);
        public const int WS_SIZEBOX = 0x00040000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_TABSTOP = 0x00010000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int WS_TILED = 0x00000000;
        public const int WS_TILEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_VSCROLL = 0x00200000;
        
        public const int WS_EX_ACCEPTFILES = 0x00000010;
        public const int WS_EX_APPWINDOW = 0x00040000;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_COMPOSITED = 0x02000000;
        public const int WS_EX_CONTEXTHELP = 0x00000400;
        public const int WS_EX_CONTROLPARENT = 0x00010000;
        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_LAYERED = 0x00080000;
        public const int WS_EX_LAYOUTRTL = 0x00400000;
        public const int WS_EX_LEFT = 0x00000000;
        public const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const int WS_EX_LTRREADING = 0x00000000;
        public const int WS_EX_MDICHILD = 0x00000040;
        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int WS_EX_NOINHERITLAYOUT = 0x00100000;
        public const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const int WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
        public const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        public const int WS_EX_RIGHT = 0x00001000;
        public const int WS_EX_RIGHTSCROLLBAR = 0x00000000;
        public const int WS_EX_RTLREADING = 0x00002000;
        public const int WS_EX_STATICEDGE = 0x00020000;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_WINDOWEDGE = 0x00000100;

        public const int SW_HIDE = 0;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;

        public const int CS_BYTEALIGNCLIENT = 0x1000;
        public const int CS_BYTEALIGNWINDOW = 0x2000;
        public const int CS_CLASSDC = 0x0040;
        public const int CS_DBLCLKS = 0x0008;
        public const int CS_DROPSHADOW = 0x00020000;
        public const int CS_GLOBALCLASS = 0x4000;
        public const int CS_HREDRAW = 0x0002;
        public const int CS_NOCLOSE = 0x0200;
        public const int CS_OWNDC = 0x0020;
        public const int CS_PARENTDC = 0x0080;
        public const int CS_SAVEBITS = 0x0800;
        public const int CS_VREDRAW = 0x0001;
            
        [DllImport(User32, EntryPoint = "FindWindow")] 
        public static extern Int32 FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Creates an overlapped, pop-up, or child window. It specifies the window class, window title, window style,
        /// and (optionally) the initial position and size of the window. The function also specifies the window's parent or owner, if any, and the window's menu.
        /// </summary>
        [DllImport(User32, EntryPoint = "CreateWindowEx")]
        public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, Int64 dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hIntance, IntPtr lpParam);

        [DllImport(User32, EntryPoint = "CreateWindowW")]
        public static extern IntPtr CreateWindow(string lpClassName, string lpWindowName, Int64 dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hIntance, IntPtr lpParam);

        [DllImport(User32, EntryPoint = "RegisterClass")]
        public static extern ushort RegisterClass(ref WNDCLASS lpWndClass);

        [DllImport(User32, EntryPoint = "RegisterClassEx")]
        public static extern ushort RegisterClassEx(ref WNDCLASSEX lpWndclass);
        
        [DllImport(User32, EntryPoint = "UnregisterClass")]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);
        
        [DllImport(User32, EntryPoint = "UnregisterClassEx")]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterClassEx(string lpClassName, IntPtr hInstance);

        [DllImport(User32, EntryPoint = "ShowWindow")]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(Kernel32, EntryPoint = "GetModuleHandle", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPTStr)] string lpModuleName);
    }
}
