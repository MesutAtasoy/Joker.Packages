using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using Joker.Response;

namespace Joker.Mvc.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;

            }

            var response = new JokerBaseResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToList();

            response.AddMessages(errors);

            context.Result = new BadRequestObjectResult(response)
            {
                StatusCode = 400
            };
        }
    }
}
