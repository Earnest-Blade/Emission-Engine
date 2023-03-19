using System;

namespace Emission
{
    [Serializable]
    public partial class EmissionException : Exception
    {
        public EmissionException(uint type, Exception e) : this(type, e.Message) {}
        
        public EmissionException(uint type, string message) : this(type, message, false, false) {}
        
        public EmissionException(uint type, string message, bool showTrace, bool showMessage)
        {
            Debug.LogError($"[ERROR] {GetErrorType(type)} : {message}", showTrace);
            if(showMessage)
                Debug.FatalError($"[ERROR] {GetErrorType(type)} : {message}", false);
        }
    }
}
