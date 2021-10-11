using HappyHttp.Enums;
using HappyHttp.Interfaces;

namespace HappyHttp
{
    public class HttpRequestBuilder : IBuilder<IHttpRequest>
    {
        private string Url { get; set; }
        private HttpVerb HttpVerb { get; set; }
        private HttpMediaType MediaType { get; set; }
        private AuthorizationType AuthorizationType { get; set; }
        private string Token { get; set; }
        private string JsonPayload { get; set; }

        public HttpRequestBuilder WithUrl(string url)
        {
            Url = url;
            return this;
        }

        public HttpRequestBuilder WithHttpVerb(HttpVerb httpVerb)
        {
            HttpVerb = httpVerb;
            return this;
        }

        public HttpRequestBuilder WithMediaType(HttpMediaType mediaType)
        {
            MediaType = mediaType;
            return this;
        }

        public HttpRequestBuilder WithAuthorization(AuthorizationType authorizationType, string token = "")
        {
            AuthorizationType = authorizationType;
            Token = token;
            return this;
        }

        public HttpRequestBuilder WithJsonRequestBody(string jsonPayload)
        {
            JsonPayload = jsonPayload;
            return this;
        }

        public IHttpRequest Build()
        {
            return new HttpRequest
            {
                Url = Url,
                HttpVerb = HttpVerb,
                MediaType = MediaType,
                AuthorizationType = AuthorizationType,
                Token = Token,
                JsonPayload = JsonPayload
            };
        }
    }

    interface IBuilder<T>
    {
        T Build();
    }
}
