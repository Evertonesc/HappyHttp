using FluentAssertions;
using HappyHttp;
using HappyHttp.Enums;
using HappyHttpTests.Config;
using Xunit;

namespace HappyHttpTests.Unit
{
    public class HttpRequestBuilderTest
    {
        [Fact(DisplayName = "Should Return HttpRequest object")]
        [Trait("Unit", "HttpRequestBuilder")]
        public void ShouldReturn_HttpRequest_Object()
        {
            //Arrange
            var config = ConfigurationFiles.ReadAppSettings();
            var charactersUrl = config["MarvelUrls:CharactersName"];
            var httpVerb = HttpVerb.Post;
            var mediaType = HttpMediaType.Json;
            var authType = AuthorizationType.Bearer;

            //Act
            var httpRequest = new HttpRequestBuilder()
                .WithUrl(charactersUrl)
                .WithHttpVerb(httpVerb)
                .WithMediaType(mediaType)
                .WithAuthorization(authType, "token")
                .WithJsonRequestBody("jsonbody")
                .Build();

            //Assert
            httpRequest.Url.Should().NotBeNullOrEmpty();
            httpRequest.HttpVerb.Should().Be(HttpVerb.Post);
            httpRequest.MediaType.Should().Be(HttpMediaType.Json);
            httpRequest.AuthorizationType.Should().Be(AuthorizationType.Bearer);
        }
    }
}
