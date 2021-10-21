using System;

namespace LogProxyAPI.Models
{
    public class Fields
    {
        public string id { get; set; }
        public string Message { get; set; }
        public string Summary { get; set; }
        public DateTime receivedAt { get; set; }
    }
}
