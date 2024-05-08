using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidationException)
        {
            // Podemox fazer tanto do jeito q esta comentado quanto o outro. A diferença é que no primeiro, caso context.Exception não consig ser do tipo inferido,
            // ele ira ser nulo, ja na segunda opção, ele vai estouorar um erro
            //  var exception = context.Exception as ErrorOnValidationException;
            var exception = (ErrorOnValidationException)context.Exception;

            var errorMessage = new ResponseErrorJson(exception.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorMessage);
        }
        else if (context.Exception is NotFoundException notFoundException)
        {
            var errorMessage = new ResponseErrorJson(notFoundException.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new NotFoundObjectResult(errorMessage);
        }
        else
        {
            var errorMessage = new ResponseErrorJson(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorMessage);
        }
    }
    
    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorMessage = new ResponseErrorJson("Unknown error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessage);
    }
}
