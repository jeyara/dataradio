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
            _Log.Log($"Broadcast > { this._Id}", $"{packet.ToString()}", ConsoleColor.Yellow);
            medium.Broadcast(packet, _TransReceiversInRange);
        }

        public int GetDelay()
        {
            return new Random().Next(20, 300);
        }

        public void ReceiveData(Packet packet, IMedium medium)
        {
            _Log.Log($"Receive > { this._Id}", $"{packet.ToString()}", ConsoleColor.Green);

            if (packet.SourceId != _Id && !packet.AckIds.Contains(this.ReceiverId))
            {
               packet.AckIds.Add(this.ReceiverId);
               this._TransReceiversInRange.ForEach(t => t.Broadcast(medium, packet));
            }
        }
    }
}
