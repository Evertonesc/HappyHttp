![.NET](https://github.com/Evertonesc/HappyHttp/workflows/.NET/badge.svg?branch=main)
[![NuGet](http://img.shields.io/nuget/v/happyhttp)](https://www.nuget.org/packages/happyhttp/)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/Evertonesc/HappyHttp/blob/main/LICENSE
)

# HappyHttp
A simple package that helps you to get more productive when working with HTTP Requests in C#

## Instalation

Use the nuget package manager on Visual Studio or via cli:

```powershell
Install-Package happyhttp
```
```bash
dotnet add package happyhttp 
```

## How to use

After installing the package, instantiate the `HttpRequestBuilder` object and pass the properties like url, HttpVerb, token, and Json body to it. 

Then use the HttpClient extension method `SendRequest` and pass the HttpRequestBuilder object as a parameter. The SendRequest method adds the token to the HttpClient request header before making the call to the URL.
 

``` csharp

var config = ConfigurationFiles.ReadAppSettings();
var charactersUrl = config["GotApi:Houses"];
var httpVerb = HttpVerb.Get;

var httpRequest = new HttpRequestBuilder()
            .WithUrl(charactersUrl)
            .WithHttpVerb(httpVerb)
            .WithAuthorization(token)
            .WithJsonRequestBody(jsonBody)
            .Build();

var httpResponse = await _httpClient.SendRequest(httpRequest);
```

If you need to add custom headers to the request, HappyHttp has got you covered. You just need to create a dictionary with the values you want and pass it to the `WithHeaders` method.

``` csharp

var smsUrl = _config["CovidStatistics:Url"];
var httpVerb = HttpVerb.Get;

var headers = new Dictionary<string, string>
{
    { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
    { "x-rapidapi-key", "yourKey"}
};

var httpRequest = new HttpRequestBuilder()
            .WithUrl(smsUrl)
            .WithHttpVerb(httpVerb)
            .WithHeaders(headers)
            .WithJsonRequestBody(jsonBody)
            .Build();

```

Enjoy!