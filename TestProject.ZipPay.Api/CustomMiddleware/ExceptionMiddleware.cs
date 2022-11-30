using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using TestProject.ZipPay.Common;

namespace TestProject.ZipPay.Api.CustomMiddleware
{
    /// <summary>
    /// Global exception middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (DbUpdateException ex)
            {
                await HandleException(httpContext, ex);

            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        /// <summary>
        /// Handle EntityFrameworkCore type exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task HandleException(HttpContext context, DbUpdateException ex)
        {
            logger.LogError(ex, ex.Message);
            return WriteErrorResponse(context, (int)HttpStatusCode.BadRequest, Constant.DuplicateEmailAccErrorMsg);
        }

        /// <summary>
        /// Handle system exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task HandleException(HttpContext context, Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return WriteErrorResponse(context, (int)HttpStatusCode.InternalServerError, Constant.InternalServerErrMsg);
        }

        /// <summary>
        /// Write error message and status code
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private static Task WriteErrorResponse(HttpContext context, int statusCode, string errorMsg)
        {
            var errorMessageObject =
                new { Message = errorMsg };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = Constant.ApplicationJsonContentType;
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
