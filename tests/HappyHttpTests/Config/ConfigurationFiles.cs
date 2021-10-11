using Microsoft.Extensions.Configuration;

namespace HappyHttpTests.Config
{
    public static class ConfigurationFiles
    {
        public static IConfiguration ReadAppSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }
    }
}
