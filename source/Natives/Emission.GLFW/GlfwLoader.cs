using Emission.Natives.Win32;
using Emission.Core;
using Emission.Core.IO;
using Emission.Core.Memory;

namespace Emission.Natives.GLFW
{
    internal class GlfwLoader
    {
        public static unsafe IntPtr Initialize()
        {
            if((Application.Instance!.Context.DebugFlags & DebugFlags.ShowGlfwInfo) == DebugFlags.ShowGlfwInfo)
                Debug.Log("[Emission.GLFW] Starting loading Emission.GLFW Bindings.");

            // Load dll as a ptr
            IntPtr libPtr = IntPtr.Zero;
            if (!Library.LibraryFileExist(Library.GLFW))
                throw new DllNotFoundException(Library.GLFW);

            libPtr = Kernel32.LoadLibrary(Library.GetLibraryPath(Library.GLFW));
            if (libPtr == IntPtr.Zero)
                throw new FatalEmissionException(EmissionException.ERR_GLFW, "Cannot load libglfw.dll!");

            ulong linkableDelegates = 0;
            ulong linkedDelegates = 0;

            Library.LoadNativeLibrary(libPtr, typeof(Glfw), &linkableDelegates, &linkedDelegates);

            if ((Application.Instance!.Context.DebugFlags & DebugFlags.ShowGlfwInfo) == DebugFlags.ShowGlfwInfo)
            {
                Debug.Log("[Emission.GLFW] Linked " + linkedDelegates + " out of " + linkableDelegates + " delegates");
                Debug.Log($"[Emission.GLFW] Detected version '{Memory.PtrToStringUtf8(Glfw.glfwGetVersionString())}'");
            }
            
            return libPtr;
        }
    }
}
