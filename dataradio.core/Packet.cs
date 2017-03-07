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
