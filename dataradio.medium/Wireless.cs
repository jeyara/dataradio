using dataradio.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dataradio.medium
{
    public class Wireless : IEnvironment, IMedium
    {
        private Ilogger _log;
        public Wireless(Ilogger log)
        {
            _log = log;
        }
        public void Broadcast(Packet packet, List<ITransReceiver> to)
        {
            if (packet == null) return;

            _log.Log("Medium Info", $"Broadcast from  {packet.SourceId}. In Range {string.Join(", ", to.Select(t=>t.ReceiverId).ToArray())}", LogLevel.Medium);

            to.ForEach(t => {
                var delay = t.GetDelay();
                _log.Log("Medium Info", $"Delay in transmission to {t.ReceiverId} ==> {delay}ms", LogLevel.Medium);
                Thread.Sleep(delay);
                t.ReceiveData(packet, this);
            });
        }
    }
}
