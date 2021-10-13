using HappyHttp.Enums;
using System.Collections.Generic;
using System.Net.Http;

namespace HappyHttp.Interfaces
{
    public interface IHttpRequest
    {
        public string Url { get; set; }
        public HttpVerb HttpVerb { get; set; }
        public HttpMediaType MediaType { get; set; }
        public AuthorizationType AuthorizationType { get; set; }
        public string Token { get; set; }
        public string JsonBody { get; set; }
        public Dictionary<string, string> UrlEncodedParams { get; set; }
        public HttpContent Content { get; }

        void SetHttpRequestContent();
    }
}
