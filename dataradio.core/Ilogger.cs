using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.core
{
    public interface Ilogger
    {
        void Log(string message);

        void Log(Packet packet);
    }
}
