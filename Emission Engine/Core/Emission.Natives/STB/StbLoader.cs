using System;
using System.Reflection;
using System.Runtime.InteropServices;

using Emission.IO;

namespace Emission.Natives.STB
{
    public static class StbLoader
    {
        private static readonly Type[] StbTypes = { typeof(StbImage) };

        public static unsafe IntPtr Initialize()
        {
            Debug.Log("[STB] Starting loading STB Bindings.");
            
            // Load dll as a ptr
            IntPtr libPtr = IntPtr.Zero;
            if (!GameFile.Exists(GameDirectory.GetCurrentDirectory() + NativeLibraries.STB))
                throw new DllNotFoundException(NativeLibraries.STB);

            libPtr = NativeLibraries.LoadLibrary(GameDirectory.GetCurrentDirectory() + NativeLibraries.STB);
            if (libPtr == IntPtr.Zero)
                throw new EmissionException(EmissionErrors.EmissionGlfwException, "Cannot load stb.dll!");

            Type delegateType = typeof(MulticastDelegate);
            
            ulong linkableDelegates = 0;
            ulong linkedDelegates = 0;

            foreach (Type type in StbTypes)
            {
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                
                foreach (var fi in fields)
                {
                    if (fi.FieldType.BaseType != delegateType) continue;
                    linkableDelegates++;

                    IntPtr ptr = NativeLibraries.GetProcAddress(libPtr, fi.Name);
                    if (ptr != IntPtr.Zero)
                    {
                        type.GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                        linkedDelegates++;
                    }
                    else
                    {
                        Debug.Warning("[WARNING][STB] Could not link '" + fi.Name + "'");
                    }
                }
            }

            Debug.Log("[STB] Linked " + linkedDelegates + " out of " + linkableDelegates + " delegates");
            return libPtr;
        }
    }
}
