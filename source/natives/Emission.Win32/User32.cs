using System.Runtime.InteropServices;

namespace Emission.Natives.Win32
{
    public static class User32
    {
        /// <summary>
        /// Displays a modal dialog box that contains a system icon, a set of buttons, and a brief application-specific message, such as status or error information.
        /// The message box returns an integer value that indicates which button the user clicked.
        /// </summary>
        /// <param name="h">A handle to the owner window of the message box to be created. If this parameter is NULL, the message box has no owner window.</param>
        /// <param name="message">The message to be displayed. If the string consists of more than one line, you can separate the lines using a carriage return and/or linefeed character between each line.</param>
        /// <param name="title">The dialog box title. If this parameter is NULL, the default title is Error.</param>
        /// <param name="type">Content and behavior of the dialog box.</param>
        /// <returns></returns>
        [DllImport(NativeLibraries.USER32, CharSet = CharSet.Unicode, EntryPoint = "MessageBox")]
        public static extern int ShowMessageBox(IntPtr h, string message, string title, uint type);

        public const uint MB_ABORTRETRYIGNORE = 0x00000002;
        public const uint MB_CANCELTRYCONTINUE = 0x00000006;
        public const uint MB_HELP = 0x00004000;
        public const uint MB_OK = 0x00000000;
        public const uint MB_OKCANCEL = 0x00000001;
        public const uint MB_RETRYCANCEL = 0x00000005;
        public const uint MB_YESNO = 0x00000004;
        public const uint MB_YESNOCANCEL = 0x00000003;
        
        public const uint MB_ICONEXCLAMATION = 0x00000030;
        public const uint MB_ICONWARNING = 0x00000030;
        public const uint MB_ICONINFORMATION = 0x00000040;
        public const uint MB_ICONASTERISK = 0x00000040;
        public const uint MB_ICONQUESTION = 0x00000020;
        public const uint MB_ICONSTOP = 0x00000010;
        public const uint MB_ICONERROR = 0x00000010;
        public const uint MB_ICONHAND = 0x00000010;
        
        public const uint MB_DEFBUTTON1 = 0x00000000;
        public const uint MB_DEFBUTTON2 = 0x00000100;
        public const uint MB_DEFBUTTON3 = 0x00000200;
        public const uint MB_DEFBUTTON4 = 0x00000300;
        
        public const uint MB_APPLMODAL = 0x00000000;
        public const uint MB_SYSTEMMODAL = 0x00001000;
        public const uint MB_TASKMODAL = 0x00002000;
        
        public const uint MB_DEFAULT_DESKTOP_ONLY = 0x00020000;
        public const uint MB_RIGHT = 0x00080000;
        public const uint MB_RTLREADING = 0x00100000;
        public const uint MB_SETFOREGROUND = 0x00010000;
        public const uint MB_TOPMOST = 0x00040000;
        public const uint MB_SERVICE_NOTIFICATION = 0x00200000;
    }
}
