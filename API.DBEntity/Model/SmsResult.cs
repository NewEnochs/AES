using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace API.DBEntity.Model
{
    [XmlType("result")]
    public class SmsResult 
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public string extras { get; set; }
        public long timestamp { get; set; }
    }
}
