using HappyHttp.Enums;
using HappyHttp.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HappyHttp
{
    public static class HttpExtensions
    {
        public static async Task<HttpResponseMessage> SendRequest(this HttpClient httpClient, IHttpRequest httpRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                httpRequest.AuthorizationType != AuthorizationType.None && string.IsNullOrEmpty(httpRequest.Token) == false
                ? new AuthenticationHeaderValue(AuthorizationHandler.GetAuthorizationType(httpRequest.AuthorizationType), httpRequest.Token)
                : default;

            httpRequest.SetHttpRequestContent();

            var response = httpRequest.HttpVerb switch
            {
                HttpVerb.Get => await httpClient.GetAsync(httpRequest.Url),
                HttpVerb.Post => await httpClient.PostAsync(httpRequest.Url, httpRequest.Content),
                HttpVerb.Put => await httpClient.PutAsync(httpRequest.Url, httpRequest.Content),
                HttpVerb.Patch => await httpClient.PatchAsync(httpRequest.Url, httpRequest.Content),
                HttpVerb.Delete => await httpClient.DeleteAsync(httpRequest.Url),
                _ => throw new ArgumentException(message: "Invalid http verb")
            };

            return response;
        }

        protected static class AuthorizationHandler
        {
            public static string GetAuthorizationType(AuthorizationType authorizationType) =>
                authorizationType switch
                {
                    AuthorizationType.None => string.Empty,
                    AuthorizationType.Bearer => "Bearer",
                    _ => throw new ArgumentException(message: "Authorization type not found")
                };
        }
    }
}
