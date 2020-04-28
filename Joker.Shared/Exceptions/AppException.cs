using Joker.Shared.Models.Enums;
using System;

namespace Joker.Shared.Exceptions
{
    public class AppException : Exception
    {
        public string Code { get; }

        public AppException()
        {
        }

        public AppException(string code)
        {
            Code = code;
        }

        public AppException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public AppException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public AppException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public AppException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }

}
