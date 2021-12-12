using LogAnalyzer.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace LogAnalyzer.Services
{
    public class DNSResolver : IDNSResolver
    {
        private const string notResolved = "Unknown Host";
        public async Task<string> GetHostNameAsync(string ipAddress)
        {
            try
            {
                return (await Dns.GetHostEntryAsync(ipAddress))?.HostName;
            }
            catch
            {
                // TODO : Make it a setting 
                return notResolved;
            }
        }
    }
}
