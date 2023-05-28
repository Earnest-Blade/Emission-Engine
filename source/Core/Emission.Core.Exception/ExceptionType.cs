namespace Emission
{
    public partial class EmissionException
    {
        public const UInt16 ERR_UNDEFINED = 0x000;
        public const UInt16 ERR_OPEN_GL = 0x001;
        public const UInt16 ERR_GLFW = 0x002;
        public const UInt16 ERR_ASSIMP = 0x004;
        public const UInt16 ERR_IO = 0x008;
        public const UInt16 ERR_IMAGE = 0x010;
        public const UInt16 ERR_EVENT = 0x020;
        public const UInt16 ERR_SHADER = 0x040;
        public const UInt16 ERR_TEXTURE = 0x080;
        public const UInt16 ERR_GL_BUFFER = 0x100;
        public const UInt16 ERR_PAGE = 0x200;

        private static bool IsValidType(uint type) => type >= ERR_OPEN_GL && type <= ERR_PAGE;

        protected static (string title, uint id, Type type) GetErrorType(uint type)
        {
            if (!IsValidType(type))
                type = ERR_UNDEFINED;
            
            return type switch
            {
                ERR_UNDEFINED => ("Error Undefined", ERR_UNDEFINED, typeof(Exception)),
                ERR_OPEN_GL => ("Error while using OpenGl", ERR_OPEN_GL, typeof(SystemException)),
                ERR_GLFW => ("Error while using GLFW", ERR_GLFW, typeof(SystemException)),
                ERR_ASSIMP => ("Error while using Assimp", ERR_ASSIMP, typeof(SystemException)),
                ERR_IO => ("Error while trying to read or write", ERR_IO, typeof(IOException)),
                ERR_IMAGE => ("Error while trying to read or write an image", ERR_IMAGE, typeof(IOException)),
                ERR_EVENT => ("Error while calling Event", ERR_EVENT, typeof(SystemException)),
                ERR_SHADER => ("Error while using Shader", ERR_SHADER, typeof(SystemException)),
                ERR_TEXTURE => ("Error while using Texture", ERR_TEXTURE, typeof(SystemException)),
                ERR_GL_BUFFER => ("Error while using OpenGl's Buffers", ERR_GL_BUFFER, typeof(SystemException)),
                ERR_PAGE => ("Error while using Page System", ERR_PAGE, typeof(SystemException)),
                _ => ("Error Undefined", ERR_UNDEFINED, typeof(Exception))
            };
        }
    }
}
