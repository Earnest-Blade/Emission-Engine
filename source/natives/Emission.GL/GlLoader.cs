using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Emission.Natives.GLFW;
using static Emission.Natives.GL.Gl;

namespace Emission.Natives.GL
{
    internal unsafe class GlLoader
    {
        public static ushort Version { get; private set; }
        public static bool IsInitialize => Version != 0;

        public static void Initialize(Glfw.PFNGLFWGETPROCADDRESSPROC loader)
        {
            Debug.Log("[GL] Starting loading OpenGL Bindings.");

            if (loader == null)
                throw new EmissionException(EmissionException.ERR_OPEN_GL, "Cannot load OpenGl, Proc Address is null!", false, true);
            
            if (Glfw.glfwGetCurrentContext() == NULL)
                throw new NotSupportedException("Cannot load GLFW Context!");

            Type delegateType = typeof(MulticastDelegate);
            FieldInfo[] fields = typeof(Gl).GetFields(BindingFlags.Public | BindingFlags.Static);
            
            ulong linkableDelegates = 0;
            ulong linkedDelegates = 0;

            foreach (FieldInfo fi in fields)
            {
                if (fi.FieldType.BaseType != delegateType) continue;
                linkableDelegates++;

                byte[] fiName = Encoding.ASCII.GetBytes(fi.Name);

                fixed (byte* fiNamePtr = fiName)
                {
                    IntPtr ptr = loader.Invoke(fiNamePtr);

                    if (ptr != IntPtr.Zero)
                    {
                        typeof(Gl).GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                        linkedDelegates++;
                    }
                    else
                    {
                        Debug.LogWarning("[WARNING][GL] Could not link '" + fi.Name + "'");
                    }
                }
            } 
            
            FindVersion();

            if (!IsInitialize)
                throw new Exception("Error while loading OpenGl!");

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
                throw new EmissionException(EmissionException.ERR_OPEN_GL, "Could not bind OpenGL (Version is define as 0)");
        }
    }
}