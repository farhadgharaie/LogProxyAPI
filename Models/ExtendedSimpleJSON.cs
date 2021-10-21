using System;

namespace LogProxyAPI.Models
{
    public class ExtendedSimpleJSON : SimpleJSON
    {
        public string id { get; set; }
        public DateTime receivedAt { get; set; }
    }
}
