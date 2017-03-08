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

        public void Log(string source, Packet packet, LogLevel logLevel)
        {
            Console.ForegroundColor = GetLogLevelColor(logLevel);
            var msg = $"[{DateTime.Now.ToString("hh:mm:ss.fff tt")}] [{source}] \t {packet.ToString()}";
            Console.WriteLine(msg);
        }

        public void Log(string source, string message, LogLevel logLevel)
        {
            Console.ForegroundColor = GetLogLevelColor(logLevel);
            var msg = $"[{DateTime.Now.ToString("hh: mm:ss.fff tt")}] [{source}] \t {message}";
            Console.WriteLine(msg);
        }

        private ConsoleColor GetLogLevelColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Spam:
                    return ConsoleColor.Gray;
                case LogLevel.Debug:
                    return ConsoleColor.Magenta;
                case LogLevel.Broadcast:
                    return ConsoleColor.Yellow;
                case LogLevel.Receive:
                    return ConsoleColor.Green;
                case LogLevel.Medium:
                    return ConsoleColor.White;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
