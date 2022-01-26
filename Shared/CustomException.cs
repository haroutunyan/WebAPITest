using System.Net;

namespace Shared
{
    public class CustomException : Exception
    {
        public override string Message { get; }
        public HttpStatusCode Code { get; }
        public CustomException(string msg = Constants.UserNotFound, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            Message = msg;
            Code = code;
        }
    }
}