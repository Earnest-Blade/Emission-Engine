using System;
using Emission.Core;

namespace Emission
{
    [Serializable]
    public partial class EmissionException : Exception
    {
        public string? Title;
        public uint Id;
        public Type? ErrorType;
        
        private EmissionException() {}
        
        internal EmissionException(string message) : base(message) {}

        public EmissionException(Exception e) : base(e.Message) {}
        public EmissionException(uint type) : this(type, String.Empty) {}
        public EmissionException(uint type, Exception e) : this(type, e.Message) {}
        public EmissionException(uint type, string message) : base(message)
        {
            (string title, uint id, Type errType) = GetErrorType(type);
            Title = title;
            Id = id;
            ErrorType = errType;
            
            Debug.LogError(this);
        }
    }
    
    public class FatalEmissionException : EmissionException
    {
        public FatalEmissionException(uint type) : this(type, String.Empty) {}

        public FatalEmissionException(uint type, string message) : base(message)
        {
            (string title, uint id, Type errType) = GetErrorType(type);
            Title = title;
            Id = id;
            ErrorType = errType;
            
            Debug.FatalError(this);
        }
    }
}
