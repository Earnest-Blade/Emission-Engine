using System;

namespace Emission
{
    public class EmissionException : Exception
    {
        public const string EmissionEventException = "EmissionEventException";
        public const string EmissionAssemblyException = "EmissionAssemblyException";
        public const string EmissionGlfwException = "EmissionGlfwException";
        public const string EmissionOpenGlException = "EmissionOpenGlException";

        public const string EmissionIoException = "EmissionIOException";
        public const string EmissionTextureException = "EmissionTextureException";
        public const string EmissionBundleException = "EmissionBundleException";
        public const string EmissionCompressionException = "EmissionCompressionException";
        
        public EmissionException(string type, string message) : base(message)
        {
            Print(type, message, base.StackTrace);
        }

        public EmissionException(string type, Exception exception) : base(exception.Message)
        {
            Print($"{type}, {exception.GetType()}", exception.ToString(), exception.StackTrace);
        }

        protected void Print(string type, string message, string stack)
        {
            Debug.LogColor($"[ERROR] Emission throw {type}!\n -> {message}", ConsoleColor.Red);
            Debug.LogColor(stack + '\n', ConsoleColor.Red);
        }
    }
}
