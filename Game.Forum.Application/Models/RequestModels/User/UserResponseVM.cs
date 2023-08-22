using System.Net;

namespace Game.Forum.Application.Models.RequestModels.User
{
    public class UserResponseVM
    {
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }

        public static UserResponseVM Success(object result, HttpStatusCode statusCode)
        {
            return new UserResponseVM { Result = result, StatusCode = statusCode, Message = "İşlem başarılı", Error = false };
        }

        public static UserResponseVM Fail(object result, HttpStatusCode statusCode)
        {
            return new UserResponseVM { Result = result, StatusCode = statusCode, Message = "İşlem başarısız", Error = true };
        }
    }
}
