using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace Emission.Natives.STB
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public unsafe class StbImage
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__context
        {
            public Int32 img_x, img_y;
            public int img_n, img_out_n;

            public IntPtr io; // stci_io_callbacks
            public void* io_user_data;

            public int read_from_callbacks;
            public int buflen;
            public IntPtr buffer_start; // stbi_uc[128]
            public int callback_already_read;

            public IntPtr img_buffer, img_buffer_end;
            public IntPtr img_buffer_original, img_buffer_original_end;
        }

        public delegate void PFNSTBIGET8(stbi__context s);
        public delegate void PFNSTBISKIP(stbi__context s, int skip);
        public delegate void PFNSTBIREWIND(stbi__context s);
        public delegate int PFNSTBIATEOF(stbi__context s);
        public delegate int PFNSTBIGENN(stbi__context s, byte* buf, int size);

        public static PFNSTBIGET8 stbi__get8;
        public static PFNSTBISKIP stbi__skip;
        public static PFNSTBIREWIND stbi__rewind;
        public static PFNSTBIATEOF stbi__at_eof;
        public static PFNSTBIGENN stbi__getn;
    }
}
