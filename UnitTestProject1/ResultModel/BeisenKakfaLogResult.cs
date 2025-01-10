using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ResultModel
{
    internal class BeisenKakfaLogResult
    {
        public ResponsV2[] responses { get; set; }
    }

    public class ResponsV2
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public _ShardsV2 _shards { get; set; }
        public HitsV2 hits { get; set; }
        public Aggregations aggregations { get; set; }
        public int status { get; set; }
    }

    public class _ShardsV2
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int failed { get; set; }
    }

    public class HitsV2
    {
        public int total { get; set; }
        public object max_score { get; set; }
        public HitV2[] hits { get; set; }
    }

    public class HitV2
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public object _score { get; set; }
        public string _routing { get; set; }
        public _SourceV2 _source { get; set; }
        public Fields fields { get; set; }
        public Highlight highlight { get; set; }
        public long[] sort { get; set; }
    }

    public class _SourceV2
    {
        public string ApplicationName { get; set; }
        public DateTime LogDate { get; set; }
        public string LogGuid { get; set; }
        public string MachineName { get; set; }
        public string Message { get; set; }
        public string OSVersion { get; set; }
        public int Port { get; set; }
        public string Tag { get; set; }
        public int TenantID { get; set; }
        public long TimeStamp { get; set; }
        public long CostInMillisecond { get; set; }
        
        public int UserID { get; set; }
        public string ClrVersion { get; set; }
        public string EagleEyeTraceID { get; set; }
        public string EagleEyeBindingID { get; set; }
        public DateTime CreateDate { get; set; }
        public int ThreadID { get; set; }
        public int LogLevel { get; set; }
        public int LogLevelTag { get; set; }
        public string BrowserName { get; set; }
        public string BrowserType { get; set; }
        public string BrowserVersion { get; set; }
        public string PostParams { get; set; }
        public string QueryParams { get; set; }
        public string UrlAbsolutePath { get; set; }
        public string UserAgent { get; set; }
        public string UrlReferrer { get; set; }
        public string VirtualPath { get; set; }
        
    }

    public class FieldsV2
    {
        public long[] CreateDate { get; set; }
        public long[] LogDate { get; set; }
    }
     
}
