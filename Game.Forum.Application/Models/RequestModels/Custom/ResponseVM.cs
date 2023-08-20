using System.Net;

namespace Game.Forum.Application.Models.RequestModels.Custom
{
    public class ResponseVM
    {
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }

        public static ResponseVM Success(object result, HttpStatusCode statusCode)
        {
            return new ResponseVM { Result = result, StatusCode = statusCode, Message = "İşlem başarılı", Error = false };
        }

        public static ResponseVM Fail(object result, HttpStatusCode statusCode)
        {
            return new ResponseVM { Result = result, StatusCode = statusCode, Message = "İşlem başarısız", Error = true };
        }
    }
}
