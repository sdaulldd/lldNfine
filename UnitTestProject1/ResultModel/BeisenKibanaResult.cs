using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ResultModel
{


    public class BeisenKibanaTraceResult
    {
        public Respons[] responses { get; set; }
    }

    public class Respons
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public _Shards _shards { get; set; }
        public Hits hits { get; set; }
        public Aggregations aggregations { get; set; }
        public int status { get; set; }
    }

    public class _Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int failed { get; set; }
    }

    public class Hits
    {
        public int total { get; set; }
        public object max_score { get; set; }
        public Hit[] hits { get; set; }
    }

    public class Hit
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public object _score { get; set; }
        public _Source _source { get; set; }
        public Fields fields { get; set; }
        public Highlight highlight { get; set; }
        public long[] sort { get; set; }
    }

    public class _Source
    {
        public string TraceID { get; set; }
        public string BindingID { get; set; }
        public string ConversationID { get; set; }
        public int CurrentLevel { get; set; }
        public string Tag { get; set; }
        public string CustomeInfo { get; set; }
        public string ServiceName { get; set; }
        public int CostInMillisecond { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public int LogType { get; set; }
        public int ResultType { get; set; }
        public string MachineName { get; set; }
        public string VirtualPath { get; set; }
        public string Parameters { get; set; }
        public string ComponentName { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
        public int ISVID { get; set; }
        public long ID { get; set; }
    }

    public class Fields
    {
        public long[] CurrentDateTime { get; set; }
    }

    public class Highlight
    {
        public string[] TraceID { get; set; }
    }

    public class Aggregations
    {
        public _2 _2 { get; set; }
    }

    public class _2
    {
        public Bucket[] buckets { get; set; }
    }

    public class Bucket
    {
        public DateTime key_as_string { get; set; }
        public long key { get; set; }
        public int doc_count { get; set; }
    }


}
