using LogAnalyzer.Interfaces;
using LogAnalyzer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogAnalyzer
{
    public class Logalyzer
    {
        private readonly IDNSResolver _dnsResolver;
        private readonly IVisualizeReportService _visualizeReport;
        private readonly IFileProcessService _fileProcessService;

        public Logalyzer(IDNSResolver dnsResolver, IVisualizeReportService visualizeReportService, IFileProcessService fileProcessService)
        {
            _dnsResolver = dnsResolver;
            _visualizeReport = visualizeReportService;
            _fileProcessService = fileProcessService;
        }

        public async Task Run()
        {
            var path = Directory.GetCurrentDirectory() + "\\log2.txt";

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("*** Process File - Start***");

            var data = await _fileProcessService.Read(path);

            stopWatch.Stop();
            Console.WriteLine("*** Process File - End ***");
            Console.WriteLine("Time taken: " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            Console.WriteLine("*****");

            var reportList = await MapLogReport(data);

            _visualizeReport.Visualize(reportList);
        }

        private async Task<List<LogReportModel>> MapLogReport(ConcurrentDictionary<string, int> ipHitMap)
        {
            var hostNameTask = ipHitMap.Select(host => (host, _dnsResolver.GetHostNameAsync(host.Key))).ToList();
            await Task.WhenAll(hostNameTask.Select(res => res.Item2).ToArray()); 
            return hostNameTask.Select(res => new LogReportModel() { HitCount = res.host.Value, HostAddress = res.host.Key, HostName = res.Item2.Result }).ToList();
        }
    }
}
