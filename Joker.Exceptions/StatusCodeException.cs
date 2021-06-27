using System;

namespace Joker.Exceptions
{
    public abstract class StatusCodeException : Exception
    {
        public StatusCodeException()
        {
            
        }

        public StatusCodeException(string message)
            : base(message)
        {
            
        }
        
        public int StatusCode { get; set; }
    }
}