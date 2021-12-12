using Microsoft.Extensions.Configuration; 
using System.IO;

namespace LogAnalyzer
{
    public static class Configuration
    {
        public static void BuildConfig(this IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
