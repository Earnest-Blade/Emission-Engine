using System;

namespace Emission
{
    public class EmissionException : Exception
    {
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
            Debug.Error($"[ERROR] Emission throw {type}!\n -> {message}");
            Debug.Error(stack + '\n');
        }
    }
}
