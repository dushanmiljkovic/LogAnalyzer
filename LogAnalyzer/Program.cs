using System;
using System.Threading.Tasks;
using LogAnalyzer.Interfaces;
using LogAnalyzer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
 
namespace LogAnalyzer
{
    static class Program
    {
        private readonly static ConfigurationBuilder _builder = new ConfigurationBuilder();

        static void Main(string[] args)
        {
            _builder.BuildConfig();
            Console.WriteLine("Hello Logalyzer!");
            MainAsync3().GetAwaiter().GetResult();
        }

        private static async Task MainAsync3()
        {
            var host = Host.CreateDefaultBuilder()
                     .ConfigureServices((context, services) =>
                     {
                         services.AddTransient<IDNSResolver, DNSResolver>();
                         services.AddTransient<IVisualizeReportService, VisualizeReportService>();
                         services.AddTransient<IFileProcessService, FileProcessService>();
                     }) 
                     .Build();

            var flow = ActivatorUtilities.CreateInstance<Logalyzer>(host.Services);
            await flow.Run();
        }
    }
}
