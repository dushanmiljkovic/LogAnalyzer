using LogAnalyzer.Models; 
using System.Collections.Generic; 

namespace LogAnalyzer.Interfaces
{
    public interface IVisualizeReportService
    {
        void Visualize(IList<LogReportModel> data);
    }
}
