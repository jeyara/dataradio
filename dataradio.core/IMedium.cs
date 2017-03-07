using System.Collections.Generic;

namespace dataradio.core
{
    public interface IMedium
    {
        void Broadcast(Packet packet, List<ITransReceiver> to);
    }
}
