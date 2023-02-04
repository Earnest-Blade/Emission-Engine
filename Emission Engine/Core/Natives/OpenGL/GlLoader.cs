using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Emission.Natives.GL;

using Emission.Window.GLFW;
using static Emission.Natives.GL.Gl;

namespace Emission.Natives.GL
{
    internal class GlLoader
    {
        public static ushort Version { get; private set; }
        public static bool IsInitialize => Version != 0;

        public static void Initialize(GetProcAddressHandler loader)
        {
            Debug.Log("[GL] Starting loading OpenGL Bindings.");
            
            if (Glfw.CurrentContext == IntPtr.Zero)
                throw new EmissionException(EmissionErrors.EmissionOpenGlException, "No valid OpenGL context has been set");

            Type delegateType = typeof(MulticastDelegate);
            FieldInfo[] fields = typeof(Gl).GetFields(BindingFlags.Public | BindingFlags.Static);
            
            ulong linkableDelegates = 0;
            ulong linkedDelegates = 0;

            foreach (FieldInfo fi in fields)
            {
                if (fi.FieldType.BaseType != delegateType) continue;
                linkableDelegates++;

                IntPtr ptr = loader.Invoke(fi.Name);
                if (ptr != IntPtr.Zero)
                {
                    typeof(Gl).GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                    linkedDelegates++;
                }
                else
                {
                    Debug.Warning("[WARNING][GL] Could not link '" + fi.Name + "'");
                }
            } 
            
            FindVersion();

            Debug.Log("[GL] Linked " + linkedDelegates + " out of " + linkableDelegates + " delegates");
            Debug.Log("[GL] Detected version '" + Version + "'");
        }

        private static void FindVersion()
        {
            Version = 0;

            if (glCullFace != null)
                Version = 100;

            if (glDrawArrays != null)
                Version = 110;

            if (glDrawRangeElements != null)
                Version = 120;

            if (glActiveTexture != null)
                Version = 130;

            if (glBlendFuncSeparate != null)
                Version = 140;

            if (glGenQueries != null)
                Version = 150;

            if (glBlendEquationSeparate != null)
                Version = 200;

            if (glUniformMatrix2x3fv != null)
                Version = 210;

            if (glColorMaski != null)
                Version = 300;

            if (glDrawArraysInstanced != null)
                Version = 310;

            if (glDrawElementsBaseVertex != null)
                Version = 320;

            if (glBindFragDataLocationIndexed != null)
                Version = 330;

            if (glMinSampleShading != null)
                Version = 400;

            if (glReleaseShaderCompiler != null)
                Version = 410;

            if (glDrawArraysInstancedBaseInstance != null)
                Version = 420;

            if (glClearBufferData != null)
                Version = 430;

            if (glBufferStorage != null)
                Version = 440;

            if (glClipControl != null)
                Version = 450;

            if (glSpecializeShader != null)
                Version = 460;

            if (Version == 0)
                throw new EmissionException(EmissionErrors.EmissionGlfwException, "Could not bind OpenGL");
        }
    }
}

namespace Emission.Natives.GL
{
    public delegate IntPtr GetProcAddressHandler(string funcName);
}
