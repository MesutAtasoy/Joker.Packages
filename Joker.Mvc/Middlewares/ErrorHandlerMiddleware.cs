using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Joker.Shared.Exceptions;
using Joker.Shared.Models.Base;

namespace Joker.Mvc.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var responseMessages = new List<ResponseMessage>();
            var validationModels = new List<ValidationModel>();
            switch (exception)
            {
                case ApiException e:
                    statusCode = e.StatusCode;
                    responseMessages = e.Messages;
                    validationModels = e.Validations;
                    break;
            }
            var response = new BaseResponseModel()
            {
                StatusCode = (int)statusCode,
                Exception = exception
            };

            if (responseMessages != null && responseMessages.Any())
                response.Messages = new HashSet<ResponseMessage>(responseMessages);

            if (validationModels != null && validationModels.Any())
                response.Validations = new HashSet<ValidationModel>(validationModels);

            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }

}
