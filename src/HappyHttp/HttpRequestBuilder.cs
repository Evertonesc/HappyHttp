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
        public Dictionary<string, string> Headers { get; set; }
        public bool HasHeaderValues { get; set; }

        /// <summary>
        /// Set the Url request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public HttpRequestBuilder WithUrl(string url)
        {
            Url = url;
            return this;
        }

        /// <summary>
        /// Set the verb request
        /// </summary>
        /// <param name="httpVerb"></param>
        /// <returns></returns>
        public HttpRequestBuilder WithHttpVerb(HttpVerb httpVerb)
        {
            HttpVerb = httpVerb;
            return this;
        }

        /// <summary>
        /// Set the request media type 
        /// </summary>
        /// <param name="mediaType">The HttpMediaType Enum param</param>
        /// <param name="urlEncodedParams">Dictionary with encoded params</param>
        /// <returns></returns>
        public HttpRequestBuilder WithMediaType(HttpMediaType mediaType, Dictionary<string, string> urlEncodedParams = null)
        {
            MediaType = mediaType;
            UrlEncodedParams = urlEncodedParams;
            return this;
        }

        /// <summary>
        /// Add headers to Http request
        /// </summary>
        /// <param name="headers">Dictionary with key value headers</param>
        /// <returns></returns>
        public HttpRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            Headers = headers;
            HasHeaderValues = true;
            return this;
        }

        /// <summary>
        /// Set the authorization header
        /// </summary>
        /// <param name="authorizationType">The AuthorizationType Enum param</param>
        /// <param name="token">Empty by default</param>
        /// <returns></returns>
        public HttpRequestBuilder WithAuthorization(AuthorizationType authorizationType, string token = "")
        {
            AuthorizationType = authorizationType;
            Token = token;
            return this;
        }

        /// <summary>
        /// Set the request json body
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public HttpRequestBuilder WithJsonRequestBody(string jsonBody)
        {
            JsonBody = jsonBody;
            return this;
        }

        /// <summary>
        /// Builds an IHttpRequest object with keys and values
        /// </summary>
        /// <returns></returns>
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
                UrlEncodedParams = UrlEncodedParams,
                Headers = Headers,
                HasHeaderValues = HasHeaderValues
            };
        }
    }

    interface IBuilder<T>
    {
        T Build();
    }
}
