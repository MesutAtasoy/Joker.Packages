using Joker.Shared.Expections;
using Joker.Shared.Models.Enums;

namespace Joker.Shared.Models.Base
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {

        }

        public ResponseMessage(string message)
        {
            ResponseMessageType = ResponseMessageType.Success;
            Message = message;
        }

        public ResponseMessage(string message, object data) 
            : this(message)
        {
            ResponseMessageType = ResponseMessageType.Success;
            Data = data;
        }

        public ResponseMessage(string message, object data, ResponseMessageType responseMessageType) 
            : this(message, data)
        {
            ResponseMessageType = responseMessageType;
        }

        public ResponseMessage(string message, object data, ResponseMessageType responseMessageType, AppException exception) 
            : this(message, data, responseMessageType)
        {
            Exception = exception;
        }

        public virtual ResponseMessageType ResponseMessageType { get; set; }
        public virtual string Message { get; set; }
        public virtual AppException Exception { get; set; }
        public virtual object Data { get; set; }
    }

}
