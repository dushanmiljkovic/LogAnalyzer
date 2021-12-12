using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace LogAnalyzer.Interfaces
{
    public interface IFileProcessService
    {
        Task<ConcurrentDictionary<string, int>> Do(string path);
    }
}
