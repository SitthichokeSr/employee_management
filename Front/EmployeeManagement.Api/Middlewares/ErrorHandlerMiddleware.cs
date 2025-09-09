using EmployeeManagement.Application.Exceptions;
using System.Diagnostics;
using System.Net;

namespace EmployeeManagement.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                var errors = new List<string>();
                response.ContentType = "application/json";

                switch (error)
                {
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        errors.Add(e.Message);
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await Results.Problem(
                    title: error.Message,
                    statusCode: response.StatusCode,
                    extensions: new Dictionary<string, object?>
                    {
                        { "traceId", Activity.Current?.Id },
                        { "errors", errors }
                    }
                ).ExecuteAsync(context);
            }
        }
    }
}
