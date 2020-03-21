using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Joker.Shared.Models.Base;
using System.Linq;
using System.Net;

namespace Joker.Mvc.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var response = new BaseResponseModel
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToList();

            response.Validations.Add(new ValidationModel
            {
                Messages = errors.ToArray()
            });

            context.Result = new BadRequestObjectResult(response)
            {
                StatusCode = 400
            };
        }
    }
}
