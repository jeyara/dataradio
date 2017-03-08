
using System.Collections.Generic;

namespace dataradio.core
{
    public interface ITransReceiver
    {
        string ReceiverId { get; set; }
        void ReceiveData(Packet data, IMedium medium);
        void Broadcast(IMedium medium, Packet packet);
        int GetDelay();
        List<ITransReceiver> TransReceiversInRange { get; set; }
        Packet GetLastPacket();
    }
}
