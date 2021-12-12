using LogAnalyzer.Interfaces;
using LogAnalyzer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LogAnalyzer.Services
{
    public class FileProcessService : IFileProcessService
    {
        private const string fileIdProperty = "c-ip";
        private const char headerConst = '#';
        private const int skipHeader = 1;

        public async Task<ConcurrentDictionary<string, int>> Read(string path)
        {
            var propLineIndex = -1;

            var ipHitMap = new ConcurrentDictionary<string, int>();

            await foreach (var line in ReadLog(path))
            {
                var lineType = GetlineType(line);

                if (lineType is LineType.Header)
                {
                    propLineIndex = GetPropertyLocation(line);
                }
                else if (lineType is LineType.Body && propLineIndex >= 0)
                {
                    var prop = GetProperty(line, propLineIndex);
                    ipHitMap.AddOrUpdate(prop, 1, (_, oldValue) => oldValue + 1);
                }
            }
            return ipHitMap;
        }


        private static async IAsyncEnumerable<string> ReadLog(string path)
        {
            using var reader = File.OpenText(path);
            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }

        private static LineType GetlineType(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return LineType.NA;
            }

            return line[0] == headerConst ? LineType.Header : LineType.Body;
        }

        private static int GetPropertyLocation(string line)
        {
            try
            {
                var brLine = line.Split().Skip(skipHeader).ToList();
                return brLine.IndexOf(fileIdProperty);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        private static string GetProperty(string line, int propertyIndex)
        {
            try
            {
                var lineSplit = line.Split().ToList();

                // TODO : index out of range 
                // does exists ? if exits ++ else add
                return lineSplit[propertyIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

    }
}
