using System.Collections.Generic;

namespace LogProxyAPI.Models
{
    public class AirTableDto
    {
        public IEnumerable<RecordDto> records { get; set; }
        public string offset { get; set; }
    }
}
