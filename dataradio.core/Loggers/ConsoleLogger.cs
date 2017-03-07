using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.core
{
    public class ConsoleLogger : Ilogger
    {
        public void Log(Packet packet)
        {
            Console.WriteLine(packet.ToString());
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
