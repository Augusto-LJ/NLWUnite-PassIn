﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using System.Net;

namespace PassIn.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = context.Exception is PassInException;

            if (result)
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
            if (context.Exception is NotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else if (context.Exception is ErrorOnValidationException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else if (context.Exception is ConflictException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Result = new ConflictObjectResult(context.Exception.Message);
            }
        }
        private void ThrowUnknownError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson("Unknown error"));
        }
    }

    
}
