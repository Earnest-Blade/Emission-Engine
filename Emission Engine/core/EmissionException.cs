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


        public EmissionException(string errorType, string message)
        {
            Debug.Log($"[ERROR] Emission throw {errorType}!\n -> {message}", ConsoleColor.Red);
            Debug.Log(base.StackTrace);
        }
    }
}
