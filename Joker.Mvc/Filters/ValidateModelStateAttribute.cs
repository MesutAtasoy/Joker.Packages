using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Joker.Response;

namespace Joker.Mvc.Filters;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();

        var response = new JokerBaseResponse()
        {
            StatusCode = (int) HttpStatusCode.BadRequest,
            Message = String.Join("," , errors)
        };

        context.Result = new BadRequestObjectResult(response)
        {
            StatusCode = (int) HttpStatusCode.BadRequest
        };
    }
}