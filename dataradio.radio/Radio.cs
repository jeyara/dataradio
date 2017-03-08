using System;
using dataradio.medium;
using dataradio.core;
using System.Collections.Generic;

namespace dataradio.radio
{    public class Radio : ITransReceiver
    {
        private Ilogger _Log;
        private string _Id;
        private List<ITransReceiver> _TransReceiversInRange = new List<ITransReceiver>();
        private Packet _LastPacket = null;

        public Radio(Ilogger log)
        {
            if (string.IsNullOrWhiteSpace(_Id))
            {
                _Id = Guid.NewGuid().ToString();
            }

            _Log = log;
        }

        public Radio(string id, Ilogger log)
        {
            _Id = id;
            _Log = log;
        }

        public string ReceiverId
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public List<ITransReceiver> TransReceiversInRange
        {
            get
            {
                return _TransReceiversInRange;
            }

            set
            {
                _TransReceiversInRange = value;
            }
        }

        public void Broadcast(IMedium medium, Packet packet)
        {
            _Log.Log($"Broadcast > { this._Id}", $"{packet.ToString()}", LogLevel.Broadcast);
            medium.Broadcast(packet, _TransReceiversInRange);
            _LastPacket = packet;
        }

        public int GetDelay()
        {
            return new Random().Next(20, 300);
        }

        public void ReceiveData(Packet packet, IMedium medium)
        {
            if (packet.SourceId != _Id && !packet.AckIds.Contains(this.ReceiverId))
            {
                _Log.Log($"Receive > { this._Id}", $"{packet.ToString()}", LogLevel.Receive);
                packet.AckIds.Add(this.ReceiverId);
                packet.BroadcasterId = this.ReceiverId;
                _LastPacket = packet;
                this.Broadcast(medium, packet);
            }
            else
            {
                _Log.Log($"Ingored > { this._Id}", $"{packet.ToString()}", LogLevel.Spam);
            }
        }

        public Packet GetLastPacket()
        {
            return _LastPacket;
        }
    }
}
