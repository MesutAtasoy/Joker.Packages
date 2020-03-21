using Joker.Shared.Expections;
using Joker.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Joker.Shared.Models.Base
{
    public partial class BaseResponseModel : BaseResponseModel<object>
    {
    }

    public partial class BaseResponseModel<T>
    {
        private bool _isError = false;

        public BaseResponseModel()
        {
            Payload = default(T);
            Messages = new HashSet<ResponseMessage>();
            Validations = new HashSet<ValidationModel>();
            StatusCode = (int) HttpStatusCode.OK;
        }

        public virtual T Payload { get; set; }
        public virtual EventBaseState EventBaseState { get; set; }
        public virtual int StatusCode { get; set; }
        public virtual bool IsError
        {
            get
            {
                if (Exception != null)
                    return true;

                if (Messages.Any(p => p.ResponseMessageType != ResponseMessageType.Success))
                    return true;

                if (Validations.Count > 0)
                    return true;

                return false;
            }
            set { _isError = value; }
        }
        public virtual HashSet<ResponseMessage> Messages { get; set; }
        public virtual Exception Exception { get; set; }
        public virtual HashSet<ValidationModel> Validations { get; set; }
        public virtual BaseResponseModel<T> AddMessage(ResponseMessage responseMessage)
        {
            Messages.Add(responseMessage);
            return this;
        }

        public virtual BaseResponseModel<T> AddMessage(string message,
            ResponseMessageType responseMessageType = ResponseMessageType.Success,
            AppException exception = null,
            object data = null)
        {
            var responseMessage = new ResponseMessage
            {
                ResponseMessageType = responseMessageType,
                Message = message,
                Exception = exception,
                Data = data
            };

            Messages.Add(responseMessage);
            return this;
        }

    }
}
