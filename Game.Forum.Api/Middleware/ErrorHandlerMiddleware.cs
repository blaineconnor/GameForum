﻿using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.RequestModels.User;
using System.Net;
using System.Text.Json;

namespace Game.Forum.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                response.ContentType = "application/json";
                HttpStatusCode httpStatusCode;
                switch (error)
                {
                    case ClientSideException e:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        httpStatusCode = HttpStatusCode.NotFound;
                        break;
                    default:
                        httpStatusCode = HttpStatusCode.InternalServerError;
                        break;
                }
                var result = UserResponseVM.Fail(error.Message, httpStatusCode);
                await response.WriteAsync(JsonSerializer.Serialize(result));
                _logger.LogError(error, $"Hatanın sebebi: {error.Message}");
            }
        }
    }
}
