using HappyHttp.Enums;
using HappyHttp.Interfaces;
using System.Collections.Generic;

namespace HappyHttp
{
    public class HttpRequestBuilder : IBuilder<IHttpRequest>
    {
        private string Url { get; set; }
        private HttpVerb HttpVerb { get; set; }
        private HttpMediaType MediaType { get; set; }
        private AuthorizationType AuthorizationType { get; set; }
        private string Token { get; set; }
        private string JsonBody { get; set; }
        private Dictionary<string, string> UrlEncodedParams { get; set; }

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

        public HttpRequestBuilder WithMediaType(HttpMediaType mediaType, Dictionary<string, string> urlEncodedParams = null)
        {
            MediaType = mediaType;
            UrlEncodedParams = urlEncodedParams;
            return this;
        }

        public HttpRequestBuilder WithAuthorization(AuthorizationType authorizationType, string token = "")
        {
            AuthorizationType = authorizationType;
            Token = token;
            return this;
        }

        public HttpRequestBuilder WithJsonRequestBody(string jsonBody)
        {
            JsonBody = jsonBody;
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
                JsonBody = JsonBody,
                UrlEncodedParams = UrlEncodedParams
            };
        }
    }

    interface IBuilder<T>
    {
        T Build();
    }
}
