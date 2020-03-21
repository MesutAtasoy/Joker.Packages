using System.Collections.Generic;

namespace Joker.Shared.Models.Base
{
    public class BaseJsonResult
    {
        public List<string> Messages { get; set; }

        public bool IsError { get; set; }

        public object Result { get; set; }

        public string RedirectUrl { get; set; }

        public BaseJsonResult()
        {
            this.Messages = new List<string>();
        }

        public BaseJsonResult(object result, List<string> messages, bool isError, string redirectUrl)
        {
            Result = result;
            Messages = messages;
            IsError = isError;
            RedirectUrl = redirectUrl;
        }
    }
}