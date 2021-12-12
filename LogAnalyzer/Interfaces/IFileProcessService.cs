using System.Collections.Concurrent; 
using System.Threading.Tasks;


namespace LogAnalyzer.Interfaces
{
    public interface IFileProcessService
    {
        Task<ConcurrentDictionary<string, int>> Read(string path);
    }
}
