using System;
using static Emission.Natives.GL.Gl;

namespace Emission.Natives.GL
{
    internal static unsafe class GlMessage
    {
        public static void* MessageCallback(uint source, uint type, uint id, uint severity, int length, char* message, void* userParam)
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
                GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR => "UDEFINED BEHAVIOR",
                GL_DEBUG_TYPE_PORTABILITY => "PORTABILITY",
                GL_DEBUG_TYPE_PERFORMANCE => "PERFORMANCE",
                GL_DEBUG_TYPE_OTHER => "OTHER",
                GL_DEBUG_TYPE_MARKER => "MARKER",
                _ => "UNKNOWN"
            };

            switch (severity)
            {
                case GL_DEBUG_SEVERITY_NOTIFICATION:
                    Debug.Log($"[GL][{id}] {_type} raised from {_source} \n[GL] GL MSG CALLBACK : {MemoryHelper.PtrToStringUtf8(message)}");
                    break;
                
                case GL_DEBUG_SEVERITY_MEDIUM: case GL_DEBUG_SEVERITY_LOW:
                    Debug.LogWarning($"[WARNING][GL][{id}] {_type} raised from {_source} \n[GL] GL WARN CALLBACK : {MemoryHelper.PtrToStringUtf8(message)}");
                    break;
                
                case GL_DEBUG_SEVERITY_HIGH:
                    throw new EmissionException(EmissionException.ERR_OPEN_GL, $"[ERROR][GL][{id}] {_type} raised from {_source} \n[GL] GL ERROR CALLBACK : {MemoryHelper.PtrToStringUtf8(message)}", false, true);
                    break;
            }
            
            return NULL;
        }
    }
}
