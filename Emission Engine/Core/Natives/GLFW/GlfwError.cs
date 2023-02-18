using System;
using System.Runtime.InteropServices;

namespace Emission.Natives.GLFW
{
    internal static class GlfwError
    {
        public static void ErrorCallback(int code, IntPtr message)
        {
            Debug.Error("[ERROR][GLFW] raised : " + Marshal.PtrToStringUTF8(message));
        }
    }
}
