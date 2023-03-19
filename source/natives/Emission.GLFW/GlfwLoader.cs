
using System.Reflection;
using System.Runtime.InteropServices;
using Emission.IO;

namespace Emission.Natives.GLFW
{
    internal class GlfwLoader
    {
        public static unsafe IntPtr Initialize()
        {
            Debug.Log("[GLFW] Starting loading GLFW Bindings.");

            string libPath = NativeLibraries.GetLibraryPath(NativeLibraries.GLFW);
            
            // Load dll as a ptr
            IntPtr libPtr = IntPtr.Zero;
            if (!GameFile.Exists(libPath))
                throw new DllNotFoundException(NativeLibraries.GLFW);

            libPtr = NativeLibraries.LoadLibrary(libPath);
            if (libPtr == IntPtr.Zero)
                throw new EmissionException(EmissionException.ERR_GLFW, "Cannot load glfw3.dll!", false, true);

            Type delegateType = typeof(MulticastDelegate);
            FieldInfo[] fields = typeof(Glfw).GetFields(BindingFlags.Public | BindingFlags.Static);
            
            ulong linkableDelegates = 0;
            ulong linkedDelegates = 0;

            foreach (var fi in fields)
            {
                if (fi.FieldType.BaseType != delegateType) continue;
                linkableDelegates++;

                IntPtr ptr = NativeLibraries.GetProcAddress(libPtr, fi.Name);
                if (ptr != IntPtr.Zero)
                {
                    typeof(Glfw).GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                    linkedDelegates++;
                }
                else
                {
                    Debug.LogWarning("[WARNING][GLFW] Could not link '" + fi.Name + "'");
                }
            }
            
            Debug.Log("[GLFW] Linked " + linkedDelegates + " out of " + linkableDelegates + " delegates");
            Debug.Log($"[GLFW] Detected version '{MemoryHelper.PtrToStringUtf8(Glfw.glfwGetVersionString())}'");
            return libPtr;
        }
    }
}
