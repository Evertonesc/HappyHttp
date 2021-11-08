using FluentAssertions;
using HappyHttp;
using HappyHttp.Enums;
using HappyHttpTests.Config;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HappyHttpTests.Unit;

public class HttpRequestTest
{
    private static readonly HttpClient _httpClient = new();
    private readonly IConfiguration _config;

    public HttpRequestTest()
    {
        _config = ConfigurationFiles.ReadAppSettings();
    }

    [Fact(DisplayName = "Should Return Books object")]
    [Trait("Unit", "HttpRequestBuilder")]
    public async Task ShouldReturn_BooksObject()
    {
        //Arrange
        var config = ConfigurationFiles.ReadAppSettings();
        var charactersUrl = config["GotApi:Houses"];
        var httpVerb = HttpVerb.Get;

        //Act
        var httpRequest = new HttpRequestBuilder()
            .WithUrl(charactersUrl)
            .WithHttpVerb(httpVerb)
            .Build();

        var httpResponse = await _httpClient.SendRequest(httpRequest);
        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

        //Assert
        jsonResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task HttpPostRequestWithHeaders_ShouldReturn_Status200()
    {
        //Arrange
        var smsUrl = _config["CovidStatistics:Url"];
        var httpVerb = HttpVerb.Get;

        var headers = new Dictionary<string, string>
            {
                { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
                { "x-rapidapi-key", "566bce9ffemsh831e1cbade3c7cap11f533jsn067b4d19c55e" }
            };

        var requestObject = new
        {
            country = "Brazil",
        };

        var jsonBody = requestObject.SerializeObject();

        var httpRequest = new HttpRequestBuilder()
            .WithUrl(smsUrl)
            .WithHttpVerb(httpVerb)
            .WithHeaders(headers)
            .WithJsonRequestBody(jsonBody)
            .Build();

        //Act
        var httpResponse = await _httpClient.SendRequest(httpRequest);
        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

        //Assert
        Assert.True(httpResponse.IsSuccessStatusCode);
        jsonResponse.Should().NotBeNullOrEmpty();
    }
}

