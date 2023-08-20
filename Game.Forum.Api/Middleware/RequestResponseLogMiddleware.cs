using Game.Forum.Application.Exceptions;
using Microsoft.IO;

namespace Game.Forum.Api.Middleware
{
    public class RequestResponseLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLogMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLogMiddleware(RequestDelegate requestDelegate, ILogger<RequestResponseLogMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (var requestBodyStream = _recyclableMemoryStreamManager.GetStream())
            {
                using (var responseBodyStream = _recyclableMemoryStreamManager.GetStream())
                {
                    Stream originalRequestBody = httpContext.Request.Body;
                    httpContext.Request.EnableBuffering();
                    Stream originalResponseBody = httpContext.Response.Body;

                    try
                    {
                        await LogRequest(httpContext, requestBodyStream);
                        await LogResponse(httpContext, responseBodyStream, originalResponseBody);
                    }
                    catch (Exception ex)
                    {
                        throw new ClientSideException(ex.Message);
                    }
                    finally
                    {
                        httpContext.Request.Body = originalRequestBody;
                        httpContext.Response.Body = originalResponseBody;
                    }
                }
            }
        }
        private async Task LogResponse(HttpContext httpContext, Stream responseBodyStream, Stream originalResponseBody)
        {
            httpContext.Response.Body = responseBodyStream;

            await _next(httpContext);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            string responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            _logger.LogInformation($"Response Body is: {responseBody}");

            responseBodyStream.Seek(0, SeekOrigin.Begin);

            await responseBodyStream.CopyToAsync(originalResponseBody);
        }
        private async Task LogRequest(HttpContext httpContext, Stream requestBodyStream)
        {
            await httpContext.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Request Body is: {requestBodyText}");
            httpContext.Request.Body = requestBodyStream;
        }
    }
}
