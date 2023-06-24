using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyManager.Api.Common.Http;

namespace SurveyManager.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if(errors.Count is 0)
            return Problem();
            
        if(errors.All(error => error.Type is ErrorType.Validation))
            return ValidationProblem(errors);


        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        var firstError = errors[0];
        return Problem(firstError);
    }

    private IActionResult Problem(Error error)
    {
        int statusCode =  error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };
        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        errors.ForEach(error =>
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        });
        return ValidationProblem(modelStateDictionary);
    }
}