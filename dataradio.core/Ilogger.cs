using System;

namespace dataradio.core
{
    public interface Ilogger
    {
        void Log(string source, string message, LogLevel logLevel);

        void Log(string source, Packet packet, LogLevel logLevel);
    }

    public enum LogLevel
    {
        Medium,
        Broadcast,
        Receive,
        Spam,
        Debug,
        Error
    }
}
