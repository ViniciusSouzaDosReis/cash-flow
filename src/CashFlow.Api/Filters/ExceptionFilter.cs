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
        // Podemox fazer tanto do jeito q esta comentado quanto o outro. A diferença é que no primeiro, caso context.Exception não consig ser do tipo inferido,
        // ele ira ser nulo, ja na segunda opção, ele vai estouorar um erro
        //  var cashFlowException = context.Exception as ErrorOnValidationException;
        var cashFlowException = (CashFlowException)context.Exception;
        var errorMessage = new ResponseErrorJson(cashFlowException.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorMessage);
    }
    
    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorMessage = new ResponseErrorJson("Unknown error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessage);
    }
}
