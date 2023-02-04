using System;
using static Emission.Natives.GL.Gl;

namespace Emission.Natives.GL
{
    public unsafe class GlError
    {
        public static void* ErrorCallback(uint source, uint type, uint id, uint severity, int length, char* message, void* userParam)
        {
            string _source, _type;

            switch (source) {
                case GL_DEBUG_SOURCE_API:
                    _source = "API";
                    break;

                case GL_DEBUG_SOURCE_WINDOW_SYSTEM:
                    _source = "WINDOW SYSTEM";
                    break;

                case GL_DEBUG_SOURCE_SHADER_COMPILER:
                    _source = "SHADER COMPILER";
                    break;

                case GL_DEBUG_SOURCE_THIRD_PARTY:
                    _source = "THIRD PARTY";
                    break;

                case GL_DEBUG_SOURCE_APPLICATION:
                    _source = "APPLICATION";
                    break;

                case GL_DEBUG_SOURCE_OTHER:
                    _source = "UNKNOWN";
                    break;

                default:
                    _source = "UNKNOWN";
                    break;
            }

            switch (type) {
                case GL_DEBUG_TYPE_ERROR:
                    _type = "ERROR";
                    break;

                case GL_DEBUG_TYPE_DEPRECATED_BEHAVIOR:
                    _type = "DEPRECATED BEHAVIOR";
                    break;

                case GL_DEBUG_TYPE_UNDEFINED_BEHAVIOR:
                    _type = "UDEFINED BEHAVIOR";
                    break;

                case GL_DEBUG_TYPE_PORTABILITY:
                    _type = "PORTABILITY";
                    break;

                case GL_DEBUG_TYPE_PERFORMANCE:
                    _type = "PERFORMANCE";
                    break;

                case GL_DEBUG_TYPE_OTHER:
                    _type = "OTHER";
                    break;

                case GL_DEBUG_TYPE_MARKER:
                    _type = "MARKER";
                    break;

                default:
                    _type = "UNKNOWN";
                    break;
            }

            switch (severity)
            {
                case GL_DEBUG_SEVERITY_NOTIFICATION:
                    Debug.Log($"[GL][{id}] {_type} raised from {_source} \nGL MSG CALLBACK : {GlUtils.PtrToStringUTF8(message)}");
                    break;
                
                case GL_DEBUG_SEVERITY_MEDIUM: case GL_DEBUG_SEVERITY_LOW:
                    Debug.Warning($"[WARNING][GL][{id}] {_type} raised from {_source} \nGL WARN CALLBACK : {GlUtils.PtrToStringUTF8(message)}");
                    break;
                
                case GL_DEBUG_SEVERITY_HIGH:
                    Debug.Error($"[ERROR][GL][{id}] {_type} raised from {_source} \nGL ERROR CALLBACK : {GlUtils.PtrToStringUTF8(message)}");
                    break;
            }
            
            return NULL;
        }
    }
}
