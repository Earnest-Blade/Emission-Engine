using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Emission.IO;
using Emission.Natives.GL;

namespace Emission.Natives.GLFW
{
    internal class GlfwLoader
    {
        [DllImport(NativePaths.KERNEL32, EntryPoint = "LoadLibrary" )]
        private static extern IntPtr LoadLibrary( [In] [MarshalAs( UnmanagedType.LPStr )] string lpFileName );

        [DllImport(NativePaths.KERNEL32, EntryPoint = "GetProcAddress" )]
        private static extern IntPtr GetProcAddress( IntPtr hModule, [In] [MarshalAs( UnmanagedType.LPStr )] string lpProcName);

        public static unsafe IntPtr Initialize()
        {
            Debug.Log("[GLFW] Starting loading GLFW Bindings.");
            
            // Load dll as a ptr
            IntPtr libPtr = IntPtr.Zero;
            if (!GameFile.Exists(GameDirectory.GetCurrentDirectory() + NativePaths.GLFW))
                throw new DllNotFoundException(NativePaths.GLFW);

            libPtr = LoadLibrary(GameDirectory.GetCurrentDirectory() + NativePaths.GLFW);
            if (libPtr == IntPtr.Zero)
                throw new EmissionException(EmissionErrors.EmissionGlfwException, "Cannot load glfw3.dll!");

            Type delegateType = typeof(MulticastDelegate);
            FieldInfo[] fields = typeof(Glfw).GetFields(BindingFlags.Public | BindingFlags.Static);

            ulong linkableDelegates = 0;
            ulong linkedDelegates = 0;

            foreach (var fi in fields)
            {
                if (fi.FieldType.BaseType != delegateType) continue;
                linkableDelegates++;

                IntPtr ptr = GetProcAddress(libPtr, fi.Name);
                if (ptr != IntPtr.Zero)
                {
                    typeof(Glfw).GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                    linkedDelegates++;
                }
                else
                {
                    Debug.Warning("[WARNING][GLFW] Could not link '" + fi.Name + "'");
                }
            }
            
            Debug.Log("[GLFW] Linked " + linkedDelegates + " out of " + linkableDelegates + " delegates");
            Debug.Log($"[GLFW] Detected version '{GlUtils.PtrToStringUTF8(Glfw.glfwGetVersionString())}'");
            return libPtr;
        }
    }
}
