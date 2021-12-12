namespace LogAnalyzer.Models
{
    public class LogReportModel
    {
        public string HostName { get; set; }
        public string HostAddress { get; set; }
        public int HitCount { get; set; }

        public override string ToString()
        {
            return $"{HostName} ({HostAddress}) - {HitCount}";
        }
    }
}
