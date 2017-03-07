using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.core
{
    public class ConsoleLogger : Ilogger
    {
        public ConsoleLogger()
        {

        }

        public void Log(string source, Packet packet, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            var msg = $"[{DateTime.Now.ToString()}] [{source}] \t {packet.ToString()}";
            Console.WriteLine(msg);
        }

        public void Log(string source, string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            var msg = $"[{DateTime.Now.ToString()}] [{source}] \t {message}";
            Console.WriteLine(msg);
        }
    }
}
