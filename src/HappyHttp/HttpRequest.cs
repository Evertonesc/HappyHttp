using HappyHttp.Enums;
using HappyHttp.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HappyHttp;

public class HttpRequest : IHttpRequest
{
    public string Url { get; set; }
    public HttpVerb HttpVerb { get; set; }
    public HttpMediaType MediaType { get; set; }
    public AuthorizationType AuthorizationType { get; set; }
    public string Token { get; set; }
    public string JsonBody { get; set; }
    public Dictionary<string, string> UrlEncodedParams { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public bool HasHeaderValues { get; set; }
    public HttpContent Content { get; private set; }

    public void SetHttpRequestContent()
    {
        if (HttpVerb == HttpVerb.Get)
            return;

        switch (MediaType)
        {
            case HttpMediaType.UrlEncoded:
                Content = new FormUrlEncodedContent(UrlEncodedParams);
                break;
            case HttpMediaType.Json:
                Content = new StringContent(JsonBody, Encoding.UTF8, "application/json");
                break;
            default:
                break;
        }
    }
}

