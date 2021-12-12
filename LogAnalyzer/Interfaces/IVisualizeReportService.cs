using LogAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace LogAnalyzer.Interfaces
{
    public interface IVisualizeReportService
    {
        void Visualize(IList<LogReportModel> data);
    }
}
