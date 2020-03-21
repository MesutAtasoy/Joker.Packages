using System.Collections.Generic;
using Joker.Shared.Models.Base;
using Joker.Shared.Models.Enums;

namespace Joker.Shared.Expections
{
    public class ApiException : System.Exception
    {
        public int StatusCode { get; set; }
        public List<ValidationModel> Validations { get; set; }
        public List<ResponseMessage> Messages { get; set; }

        public object Result { get; set; }

        public ApiException(string message,
            ResponseMessageType messageType = ResponseMessageType.BadRequest,
            object data = null,
            int statusCode = 400) :
            base(message)
        {
            this.StatusCode = statusCode;
            this.Messages = new List<ResponseMessage>
            {
                new ResponseMessage
                {
                    Message = message,
                    ResponseMessageType = messageType,
                    Data = data
                }
            };
        }
        
        
        public ApiException(ResponseMessage error, 
            int statusCode = 400)
        {
            this.Messages = new List<ResponseMessage>
            {
                error
            };
            this.StatusCode = statusCode;
        }
        
        public ApiException(ValidationModel validationModel, 
            int statusCode = 400)
        {
            this.Validations = new List<ValidationModel>
            {
                validationModel
            };
            this.StatusCode = statusCode;
        }

        public ApiException(List<ResponseMessage> errors, 
            int statusCode = 400)
        {
            this.Messages = errors;
            this.StatusCode = statusCode;
        }
        
        public ApiException(List<ValidationModel> validations, 
            int statusCode = 400)
        {
            this.Validations = validations;
            this.StatusCode = statusCode;
        }
        
        public ApiException(List<ResponseMessage> errors,
            List<ValidationModel> validations,
            int statusCode = 400)
        {
            this.Messages = errors;
            this.Validations = validations;
            this.StatusCode = statusCode;
        }

        public ApiException(System.Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}