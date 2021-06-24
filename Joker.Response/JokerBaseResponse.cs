using System.Collections.Generic;

namespace Joker.Response
{
    public class JokerBaseResponse : JokerBaseResponse<object>
    {
    }
    
    public class JokerBaseResponse<T>
    {
        protected JokerBaseResponse()
        {
            Messages = new List<string>();
        }

        protected JokerBaseResponse(T payload) 
            : this()
        {
            Payload = payload;
        }
        
        protected JokerBaseResponse(T payload, int statusCode)
            : this()
        {
            Payload = payload;
            StatusCode = statusCode;
        }
        
        
        protected JokerBaseResponse(T payload, int statusCode, List<string> messages)
            : this()
        {
            Payload = payload;
            StatusCode = statusCode;
            Messages = messages;
        }
        
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
        public T Payload { get; set; }

        public void AddMessage(string message)
        {
            Messages ??= new List<string>();
            Messages.Add(message);
        }
        
        public void AddMessages(List<string> messages)
        {
            Messages ??= new List<string>();
            Messages.AddRange(messages);
        }
    }
}