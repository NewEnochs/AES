using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Model
{
    public class AppSettingsModel
    {
        public List<string> Addresses { get; set; } = new();
        public List<string> WebAddress { get; set; } = new();
        public int DefaultIndex { get; set; }
        public string DefaultConnection { get; set; } = string.Empty;
    }
}
