using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.core
{
    public class Packet
    {
        public string SourceId { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"From {SourceId} | Message {Message}";
        }
    }
}
