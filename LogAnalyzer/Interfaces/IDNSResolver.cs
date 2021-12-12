using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalyzer.Interfaces
{
    public interface IDNSResolver
    {
        /// <summary>
        ///  Returns Host Name, resolved by IP
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Host Name</returns>
        Task<string> GetHostNameAsync(string ipAddress);
    }
}
