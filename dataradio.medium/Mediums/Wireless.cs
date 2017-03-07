using dataradio.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.medium
{
    public class Wireless : IEnvironment, IMedium
    {
        private IList<IList<IReceiver>> _radios;

        public void Broadcast(Packet packet)
        {
            if (_radios == null) return;
            if (packet == null) return;



        }

        public void SetRadios(IList<IList<IReceiver>> radios)
        {
            _radios = radios;
        }
    }
}
