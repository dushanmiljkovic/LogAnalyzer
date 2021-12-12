using LogAnalyzer.Interfaces;
using LogAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogAnalyzer.Services
{
    public class VisualizeReportService : IVisualizeReportService
    {
        public void Visualize(IList<LogReportModel> data)
        {
            foreach (var report in data.OrderByDescending(x => x.HitCount))
            {
                Console.WriteLine(report.ToString());
            }
        }
    }
}
