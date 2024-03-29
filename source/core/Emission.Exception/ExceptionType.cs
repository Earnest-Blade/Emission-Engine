﻿namespace Emission
{
    public partial class EmissionException
    {
        public const uint ERR_UNDEFINED = 0x00000000;
        public const uint ERR_OPEN_GL = 0x00000001;
        public const uint ERR_GLFW = 0x00000002;
        public const uint ERR_ASSIMP = 0x00000003;
        public const uint ERR_IO = 0x00000004;
        public const uint ERR_IMAGE = 0x00000005;
        public const uint ERR_EVENT = 0x00000006;
        public const uint ERR_SHADER = 0x00000007;
        public const uint ERR_TEXTURE = 0x00000008;
        public const uint ERR_GL_BUFFER = 0x00000009;
        public const uint ERR_PAGE = 0x0000000A;

        private static bool IsValidType(uint type) => type >= ERR_OPEN_GL && type <= ERR_PAGE;

        private static string GetErrorType(uint type)
        {
            if (!IsValidType(type))
                type = ERR_UNDEFINED;
            
            return type switch
            {
                ERR_UNDEFINED => "Error Undefined",
                ERR_OPEN_GL => "Error while using OpenGl",
                ERR_GLFW => "Error while using GLFW",
                ERR_ASSIMP => "Error while using Assimp",
                ERR_IO => "Error while trying to read or write",
                ERR_IMAGE => "Error while trying to read or write an image",
                ERR_EVENT => "Error while calling Event",
                ERR_SHADER => "Error while using Shader",
                ERR_TEXTURE => "Error while using Texture",
                ERR_GL_BUFFER => "Error while using OpenGl's Buffers",
                ERR_PAGE => "Error while using Page System",
                _ => "Error Undefined"
            };
        }
    }
}
