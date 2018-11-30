using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsAPI.Entities
{
    public class FileDTO
    {
        public String UniqueName { get; set; }
        public String ActualFileName { get; set; }
        public String ContentType { get; set; }
        public long ConentLengthInBytes { get; set; }
        public String Extension { get; set; }
    }
}
