using System.Net;

namespace Game.Forum.Application.Models.DTOs.Response
{
    public class CustomResponseDto
    {
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }

        public static CustomResponseDto Success(object result, HttpStatusCode statusCode)
        {
            return new CustomResponseDto { Result = result, StatusCode = statusCode, Message = "İşlem başarılı", Error = false };
        }

        public static CustomResponseDto Fail(object result, HttpStatusCode statusCode)
        {
            return new CustomResponseDto { Result = result, StatusCode = statusCode, Message = "İşlem başarısız", Error = true };
        }
    }
}
