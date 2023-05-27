using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Emission.Core.IO;
using Emission.Natives.Win32;

namespace Emission.Core
{
    public static class Library
    {
        public const string GLFW = "libglfw";
        public const string OPENAL = "liboal";
        public const string STB = "stb";

        private const string DllResourcePath = "bin/runtimes";
        
        public static string GetLibraryPath(string library)
        {
            if (string.IsNullOrEmpty(library))
                throw new ArgumentNullException(nameof(library));
            
            string cpu = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X86 => "x86",
                Architecture.X64 => "x64",
                Architecture.Arm => "ARM",
                _ => throw new PlatformNotSupportedException()
            };

            string extension = Platform.Type switch
            {
                PlatformType.Windows => ".dll",
                PlatformType.Linux => ".so",
                PlatformType.MacOS => ".dylib",
                _ => throw new PlatformNotSupportedException()
            };
            
            string platform = Platform.Type switch
            {
                PlatformType.Windows => "win",
                PlatformType.Linux => "linux",
                PlatformType.MacOS => "osx",
                _ => throw new PlatformNotSupportedException()
            };

            string path = Path.Combine(EDirectory.GetCurrentDirectory(), DllResourcePath, platform + '-' + cpu, library + extension);
            Debug.Log($"[INFO] Loading {library} from '{path}'");
            return path;
        }

        public static unsafe void LoadNativeLibrary(IntPtr libraryPointer, Type source, ulong* linkedDelegates, ulong* linkableDelegates)
        {
            Type delegateType = typeof(MulticastDelegate);
            FieldInfo[] fields = source.GetFields(BindingFlags.Public | BindingFlags.Static);
            
            foreach (FieldInfo fi in fields)
            {
                if (fi.FieldType.BaseType != delegateType) continue;
                (*linkableDelegates)++;

                IntPtr ptr = Kernel32.GetProcAddress(libraryPointer, fi.Name);
                if (ptr != IntPtr.Zero)
                {
                    source.GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                    (*linkedDelegates)++;
                }
                else
                {
                    Debug.LogWarning($"[WARNING][{source.Name.ToUpper()}] Could not link '" + fi.Name + "'");
                }
            }
        }

        public static bool LibraryFileExist(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            return EFile.Exists(GetLibraryPath(path));
        }
    }
}
