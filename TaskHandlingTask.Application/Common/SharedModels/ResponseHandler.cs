using System.Net;

namespace TicketsHandling.Application.Common.SharedModels
{
    public class ResponseHandler
    {
        public ResponseHandler() { }

        public Response<T> Deleted<T>()
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully",
            };
        }

        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Success",
                Meta = Meta
            };
        }

        public Response<T> EditSuccess<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Updated Successfully",
                Meta = Meta
            };
        }

        public Response<T> Success<T>(T entity, string message, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = message,
                Meta = Meta
            };
        }

        public Response<T> Unauthorized<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = string.IsNullOrEmpty(message) ? "Unauthorized" : message
            };
        }

        public Response<T> BadRequest<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = string.IsNullOrEmpty(message) ? "Bad Request" : message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = string.IsNullOrEmpty(message) ? "Not Found" : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created Successfully",
                Meta = Meta
            };
        }

        public Response<T> UnprocessableEntity<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = string.IsNullOrEmpty(message) ? "UnprocessableEntity" : message
            };
        }
    }
}
