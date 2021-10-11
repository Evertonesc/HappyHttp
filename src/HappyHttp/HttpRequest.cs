using HappyHttp.Enums;
using HappyHttp.Interfaces;

namespace HappyHttp
{
    public class HttpRequest : IHttpRequest
    {
        public string Url { get; set; }
        public HttpVerb HttpVerb { get; set; }
        public HttpMediaType MediaType { get; set; }
        public AuthorizationType AuthorizationType { get; set; }
        public string Token { get; set; }
        public string JsonPayload { get; set; }
    }
}
