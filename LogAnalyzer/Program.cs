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

        static async Task Main()
        {
            _builder.BuildConfig();
            Console.WriteLine("Hello Logalyzer!");

            var host = Host.CreateDefaultBuilder()
                     .ConfigureServices((context, services) =>
                     {
                         services.AddTransient<IDNSResolver, DNSResolver>();
                         services.AddTransient<IVisualizeReportService, VisualizeReportService>();
                         services.AddTransient<IFileProcessService, FileProcessService>();
                     }).Build();

            var logalyzer = ActivatorUtilities.CreateInstance<Logalyzer>(host.Services);
            await logalyzer.Run();
        }
    }
}
