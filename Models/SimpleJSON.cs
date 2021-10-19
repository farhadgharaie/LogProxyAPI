using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Models
{
    public class SimpleJSON
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
    public class ExtendedSimpleJSON : SimpleJSON
    {
        public int id { get; set; }
        public DateTime receivedAt { get; set; }
    }
}
