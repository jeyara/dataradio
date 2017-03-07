using dataradio.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.medium
{
    public interface IMedium
    {
        void Broadcast(Packet packet);
    }
}
