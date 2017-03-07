using System.Collections.Generic;

namespace dataradio.core
{
    public class Packet
    {
        public Packet()
        {
            AckIds = new List<string>();
        }

        public string SourceId { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"From {SourceId} | Message {Message}";
        }

        public List<string> AckIds { get; set; }
    }
}
