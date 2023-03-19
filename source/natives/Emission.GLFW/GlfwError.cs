using System;
using System.Runtime.InteropServices;

namespace Emission.Natives.GLFW
{
    internal static class GlfwError
    {
        public static void ErrorCallback(int code, IntPtr message)
        {
            throw new EmissionException(EmissionException.ERR_GLFW, "[ERROR][GLFW] raised : " + Marshal.PtrToStringUTF8(message), false, true);
        }
    }
}
