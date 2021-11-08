using HappyHttp.Enums;
using HappyHttp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HappyHttp;

public static class HttpExtensions
{
    /// <summary>
    /// Send a request to the resources in IHttpRequest object
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    public static async Task<HttpResponseMessage> SendRequest(this HttpClient httpClient, IHttpRequest httpRequest)
    {
        httpClient.DefaultRequestHeaders.Authorization =
            httpRequest.AuthorizationType != AuthorizationType.None && string.IsNullOrEmpty(httpRequest.Token) == false
            ? new AuthenticationHeaderValue(AuthorizationHandler.GetAuthorizationType(httpRequest.AuthorizationType), httpRequest.Token)
            : default;

        httpRequest.SetHttpRequestContent();

        if (httpRequest.HasHeaderValues)
        {
            foreach (var header in httpRequest.Headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

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

    /// <summary>
    /// Extension method for deserialize json to generic objects
    /// </summary>
    /// <typeparam name="T">Object to wich the string is being converted</typeparam>
    /// <param name="httpResponse"></param>
    /// <returns></returns>
    public static async ValueTask<T> DeserializeHttpResponse<T>(this HttpResponseMessage httpResponse)
    {
        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(jsonResponse);
    }

    /// <summary>
    /// Extension method for objects serialization
    /// </summary>
    /// <param name="obj">The generic object to serialize</param>
    /// <returns></returns>
    public static string SerializeObject(this object obj) =>
        JsonConvert.SerializeObject(obj);


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

