using System;

namespace Emission.Window.GLFW
{
    /// <summary>
    /// Base exception class for GLFW related errors.
    /// </summary>
    public class Exception : System.Exception
    {
        #region Methods

        /// <summary>
        ///     Generic error messages if only an error code is supplied as an argument to the constructor.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <returns>Error message.</returns>
        public static string GetErrorMessage(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.NotInitialized:     return Strings.NotInitialized;
                case ErrorCode.NoCurrentContext:   return Strings.NoCurrentContext;
                case ErrorCode.InvalidEnum:        return Strings.InvalidEnum;
                case ErrorCode.InvalidValue:       return Strings.InvalidValue;
                case ErrorCode.OutOfMemory:        return Strings.OutOfMemory;
                case ErrorCode.ApiUnavailable:     return Strings.ApiUnavailable;
                case ErrorCode.VersionUnavailable: return Strings.VersionUnavailable;
                case ErrorCode.PlatformError:      return Strings.PlatformError;
                case ErrorCode.FormatUnavailable:  return Strings.FormatUnavailable;
                case ErrorCode.NoWindowContext:    return Strings.NoWindowContext;
                default:                           return Strings.UnknownError;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Exception" /> class.
        /// </summary>
        /// <param name="error">The error code to create a generic message from.</param>
        public Exception(ErrorCode error) : base(GetErrorMessage(error)) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Exception" /> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public Exception(string message) : base(message) { }

        #endregion
        
        
    }
    
    /// <summary>
    ///     Strongly-typed error codes for error handling.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        ///     An unknown or undefined error.
        /// </summary>
        [Obsolete("Use None")]
        Unknown = 0x00000000,

        /// <summary>
        ///     No error has occurred.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        ///     This occurs if a GLFW function was called that must not be called unless the library is initialized.
        /// </summary>
        NotInitialized = 0x00010001,

        /// <summary>
        ///     This occurs if a GLFW function was called that needs and operates on the current OpenGL or OpenGL ES context but no
        ///     context is current on the calling thread.
        /// </summary>
        NoCurrentContext = 0x00010002,

        /// <summary>
        ///     One of the arguments to the function was an invalid enum value.
        /// </summary>
        InvalidEnum = 0x00010003,

        /// <summary>
        ///     One of the arguments to the function was an invalid value, for example requesting a non-existent OpenGL or OpenGL
        ///     ES version like 2.7.
        /// </summary>
        InvalidValue = 0x00010004,

        /// <summary>
        ///     A memory allocation failed.
        /// </summary>
        OutOfMemory = 0x00010005,

        /// <summary>
        ///     GLFW could not find support for the requested API on the system.
        /// </summary>
        ApiUnavailable = 0x00010006,

        /// <summary>
        ///     The requested OpenGL or OpenGL ES version (including any requested context or framebuffer hints) is not available
        ///     on this machine.
        /// </summary>
        VersionUnavailable = 0x00010007,

        /// <summary>
        ///     A platform-specific error occurred that does not match any of the more specific categories.
        /// </summary>
        PlatformError = 0x00010008,

        /// <summary>
        ///     If emitted during window creation, the requested pixel format is not supported, else if emitted when querying the
        ///     clipboard, the contents of the clipboard could not be converted to the requested format.
        /// </summary>
        FormatUnavailable = 0x00010009,

        /// <summary>
        ///     A window that does not have an OpenGL or OpenGL ES context was passed to a function that requires it to have one.
        /// </summary>
        NoWindowContext = 0x0001000A
    }
}