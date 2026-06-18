using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ums.Exceptions;

namespace Ums.Filters
{
    public class CustomException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException notFoundEx)
            {
                // Return 404 with message
                context.Result = new NotFoundObjectResult(new
                {
                    Message = notFoundEx.Message,
                    StatusCode = 404
                });
                context.ExceptionHandled = true;
            }
            else
            {
                // Return 500 for other exceptions
                context.Result = new ObjectResult(new
                {
                    Message = "An unexpected error occurred.",
                    Details = context.Exception.Message,
                    StatusCode = 500
                })
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
