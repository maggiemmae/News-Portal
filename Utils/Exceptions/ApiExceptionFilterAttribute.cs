using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Utils.Exceptions
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            this.exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.InnerException == null ?
                    context.Exception.GetType() :
                    context.Exception.InnerException.GetType();

            if (this.exceptionHandlers.ContainsKey(type))
            {
                this.exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing request"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException ??
                            context.Exception.InnerException as NotFoundException;

            context.Result = new NotFoundObjectResult(new ProblemDetails() { Detail = exception.Message });

            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException ??
                            context.Exception.InnerException as NotFoundException;

            context.Result = new UnauthorizedObjectResult(new ProblemDetails() { Detail = exception.Message });

            context.ExceptionHandled = true;
        }
    }
}
