using System;

namespace dataradio.core
{
    public interface Ilogger
    {
        void Log(string source, string message, ConsoleColor color);

        void Log(string source, Packet packet, ConsoleColor color);
    }
}
