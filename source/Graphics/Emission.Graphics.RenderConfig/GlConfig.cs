using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Core.Memory;
using Emission.Natives.GL;
using Emission.Natives.GLFW;
using static Emission.Natives.GL.Gl;
using static Emission.Natives.GLFW.Glfw;

namespace Emission.Graphics.RenderConfig
{
    public unsafe class GlConfig : RenderConfig
    {
        private const string ERROR_TEMPLATE = "\n[ERROR][Emission.GL]\n====================\nObject ID: {0}\nSeverity:  {1}\nType:      {2}\nSource:    {3}\nMessage:   {4}\n";
        
        private const string INFO_TEMPLATE = "[Emission.GL] {0}";
        private const string WARNING_TEMPLATE = "[WARNING][Emission.GL] {0}";

        public GlConfig()
        {
            Name = "OpenGL";
        }

        public override void Initialize(Glfw.PFNGLFWGETPROCADDRESSPROC address)
        {
            InfoCallback("Starting loading OpenGL Bindings.");

            if (address == null)
                throw new FatalEmissionException(EmissionException.ERR_OPEN_GL, "Cannot load OpenGl, Proc Address is null!");
            
            if (glfwGetCurrentContext() == NULL)
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
                    IntPtr ptr = address.Invoke(fiNamePtr);

                    if (ptr != IntPtr.Zero)
                    {
                        typeof(Gl).GetField(fi.Name)!.SetValue(null, Marshal.GetDelegateForFunctionPointer(ptr, fi.FieldType));
                        linkedDelegates++;
                    }
                    else
                    {
                        WarningCallback("[WARNING][Emission.GL] Could not link '" + fi.Name + "'");
                    }
                }
            } 
            
            FindVersion();

            if (Version == 0)
                throw new Exception("Error while loading OpenGl!");

            // Enable OpenGL debug output
            if (Application.Instance!.IsDebug)
            {
                glEnable(GL_DEBUG_OUTPUT);
                glEnable(GL_DEBUG_OUTPUT_SYNCHRONOUS);
                glDebugMessageCallback(MessageCallback, (void*)0);
                
                InfoCallback("Enable OpenGL Debug Callback");
            }
            
            InfoCallback("Linked " + linkedDelegates + " out of " + linkableDelegates + " delegates");
            InfoCallback("Detected version '" + Version + "'");
        }

        public override void InitializeRenderer()
        {
            glEnable(GL_BLEND);
            glEnable(GL_DEPTH_TEST);

            glDepthFunc(GL_LESS);
            glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

            int width, height;
            glfwGetWindowSize(Application.Instance!.Context.Window, &width, &height);
            glViewport(0, 0, width, height);
            
            InfoCallback($"Using OpenGL {Memory.PtrToStringUtf8(glGetString(GL_VERSION))}");
            InfoCallback($"Using GLSL {Memory.PtrToStringUtf8(glGetString(GL_SHADING_LANGUAGE_VERSION))}");
            InfoCallback($"Running with OpenGL Vendor {Memory.PtrToStringUtf8(glGetString(GL_VENDOR))}");
            InfoCallback($"Running with OpenGL Renderer {Memory.PtrToStringUtf8(glGetString(GL_RENDERER))}");
        }

        public override RenderConfig GetDefault()
        {
            IsBlend = true;
            IsClipDistance = false;
            IsCullFace = true;
            IsDepthClamp = false;
            IsDepthTest = true;
            IsDither = false;
            
            ClearColor = ColorRgb.Black;
            
            return this;
        }
        
        private static void* MessageCallback(uint source, uint type, uint id, uint severity, int length, char* message, void* userParam)
        {
            string _source = source switch
            {
                GL_DEBUG_SOURCE_API => "API",
                GL_DEBUG_SOURCE_WINDOW_SYSTEM => "WINDOW SYSTEM",
                GL_DEBUG_SOURCE_SHADER_COMPILER => "SHADER COMPILER",
                GL_DEBUG_SOURCE_THIRD_PARTY => "THIRD PARTY",
                GL_DEBUG_SOURCE_APPLICATION => "APPLICATION",
                GL_DEBUG_SOURCE_OTHER => "UNKNOWN",
                _ => "UNKNOWN"
            };

            string _type = type switch
            {
                GL_DEBUG_TYPE_ERROR => "ERROR",
                GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR => "DEPRECATED BEHAVIOR",
                GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR => "UNDEFINED BEHAVIOR",
                GL_DEBUG_TYPE_PORTABILITY => "PORTABILITY",
                GL_DEBUG_TYPE_PERFORMANCE => "PERFORMANCE",
                GL_DEBUG_TYPE_OTHER => "OTHER",
                GL_DEBUG_TYPE_MARKER => "MARKER",
                _ => "UNKNOWN"
            };
            
            switch (severity)
            {
                case GL_DEBUG_SEVERITY_NOTIFICATION:
                    InfoCallback(Memory.PtrToStringUtf8(message));
                    break;
                
                case GL_DEBUG_SEVERITY_LOW:
                    WarningCallback(Memory.PtrToStringUtf8(message));
                    break;
                
                case GL_DEBUG_SEVERITY_MEDIUM:
                    ErrorCallback(_source, _type, id, "GL_DEBUG_SEVERITY_MEDIUM", message);
                    break;
                
                case GL_DEBUG_SEVERITY_HIGH: 
                    ErrorCallback(_source, _type, id, "GL_DEBUG_SEVERITY_HIGH", message);
                    break;
            }
            
            return NULL;
        }

        private static void* InfoCallback(string? message)
        {
            if((Application.Instance!.Context.DebugFlags & DebugFlags.ShowGlWarnings) == DebugFlags.ShowGlWarnings)
                Debug.Log(String.Format(INFO_TEMPLATE, message));

            return NULL;
        }
        
        private static void* WarningCallback(string? message)
        {
            if((Application.Instance!.Context.DebugFlags & DebugFlags.ShowGlWarnings) == DebugFlags.ShowGlWarnings)
                Debug.LogWarning(String.Format(WARNING_TEMPLATE, message));

            return NULL;
        }
        
        private static void* ErrorCallback(string source, string type, uint id, string severity, char* message)
        {
            if ((Application.Instance!.Context.DebugFlags & DebugFlags.HideGlErrors) != DebugFlags.HideGlErrors)
            {
                throw new FatalEmissionException(EmissionException.ERR_OPEN_GL, string.Format(ERROR_TEMPLATE, id, severity, type, source, Memory.PtrToStringUtf8(message)));
            }
            
            return NULL;
        }
        
        private void FindVersion()
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
