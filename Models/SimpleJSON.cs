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
        public string id { get; set; }
        public DateTime receivedAt { get; set; }
    }
    public class AirTableRequesModel
    {
        public IEnumerable<RecordRequetModel> records { get; set; }
    }
    public class RecordRequetModel
    {
        public Fields fields { get; set; }
    }
    public class AirTableDto
    {
        public IEnumerable<RecordDto> records { get; set; }
        public string offset { get; set; }
    }
    public class RecordDto
    {
        public string id { get; set; }
        public Fields fields { get; set; }
        public DateTime createdTime { get; set; }
    }
    public class Fields
    {
        public string id { get; set; }
        public string Message { get; set; }
        public string Summary { get; set; }
        public DateTime receivedAt { get; set; }
    }
}
