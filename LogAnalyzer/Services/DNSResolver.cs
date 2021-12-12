using LogAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzer.Services
{
    public class DNSResolver : IDNSResolver
    {
        public async Task<string> GetHostNameAsync(string ipAddress)
        {
            try
            {
                return (await Dns.GetHostEntryAsync(ipAddress))?.HostName;
            }
            catch
            {
                // TODO : Make it a setting 
                return "-";
            }
        }
    }
}
