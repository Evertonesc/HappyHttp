using HappyHttp;
using HappyHttp.Enums;
using HappyHttpTests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HappyHttpTests.Unit
{
    public class RequestsTests
    {
        [Fact]
        public async Task ShouldReturn_An_Error()
        {
            //Arrange
            var config = ConfigurationFiles.ReadAppSettings();
            var charactersUrl = config["MarvelUrls:CharactersName"];
            var httpVerb = HttpVerb.Post;
            var mediaType = HttpMediaType.Json;
            var authType = AuthorizationType.Bearer;

            var httpRequest = new HttpRequestBuilder()
                .WithUrl(charactersUrl)
                .WithHttpVerb(httpVerb)
                .WithMediaType(mediaType)
                .WithAuthorization(authType, "token")
                .WithJsonRequestBody(string.Empty)
                .Build();

            //Act
            var httpclient = new HttpClient();
            var act = await httpclient.SendRequest(httpRequest);
        }
    }
}
