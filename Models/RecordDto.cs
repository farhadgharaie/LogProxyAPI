using System;

namespace LogProxyAPI.Models
{
    public class RecordDto
    {
        public string id { get; set; }
        public Fields fields { get; set; }
        public DateTime createdTime { get; set; }
    }
}
