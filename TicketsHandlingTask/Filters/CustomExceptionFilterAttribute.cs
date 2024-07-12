using TicketsHandling.Application.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;
 
using Microsoft.AspNetCore.Mvc;
using TicketsHandling.Application.Common.SharedModels;
using System.Text.Json;

namespace TicketsHandling.API.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An exception occurred.");
            await HandleException(context.HttpContext, context.Exception);
            context.ExceptionHandled = true; // Ensure the exception is marked as handled
        }

        private async Task HandleException(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An error occurred while processing your request."
            };

            switch (error)
            {
                case UnauthorizedAccessException e:
                    responseModel.Message = e.Message;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case FluentValidation.ValidationException e:
                    responseModel.Message = e.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors.Select(x => x.ErrorMessage).ToList();
                    break;

                case KeyNotFoundException e:
                    responseModel.Message = e.Message;
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case DbUpdateException e:
                    responseModel.Message = e.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel);
            _logger.LogInformation("Writing response: {Result}", result); // Log the response for debugging

            await response.WriteAsync(result);
            await response.Body.FlushAsync(); // Ensure the response is flushed
        }
    }

}
