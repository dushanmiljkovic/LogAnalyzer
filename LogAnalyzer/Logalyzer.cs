using LogAnalyzer.Interfaces;
using LogAnalyzer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; 
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
            var path = Directory.GetCurrentDirectory() + "\\log.txt";
             
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("*** Process File - Start***");

            var data = await _fileProcessService.Read(path);

            stopWatch.Stop();
            Console.WriteLine("*** Process File - End ***");
            Console.WriteLine("Time taken: " + stopWatch.Elapsed.ToString(@"m\:ss\.fff"));
            Console.WriteLine("*****");

            var reportList = await DictionaryToList(data);

            _visualizeReport.Visualize(reportList);
        }

        private async Task<List<LogReportModel>> DictionaryToList(ConcurrentDictionary<string, int> hitMap)
        {
            // This should be in Parallel due to speed
            var mappedList = new List<LogReportModel>();
            foreach (var (ipAddress, count) in hitMap)
            {
                var hostname = await _dnsResolver.GetHostNameAsync(ipAddress);
                var testPrintModel = new LogReportModel() { HitCount = count, HostName = hostname, HostAddress = ipAddress };
                mappedList.Add(testPrintModel);
            }
            return mappedList;
        }
    }
}
