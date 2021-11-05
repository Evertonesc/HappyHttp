using FluentAssertions;
using HappyHttp;
using HappyHttp.Enums;
using HappyHttpTests.Config;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HappyHttpTests.Unit
{
    public class HttpRequestTest
    {
        private static readonly HttpClient _httpClient = new();


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
    }
}
