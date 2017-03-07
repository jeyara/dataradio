using System;
using dataradio.medium;

namespace dataradio.radio
{
    public class Radio : IReceiver
    {
        private string _id;

        public Radio()
        {
            if (string.IsNullOrWhiteSpace(_id))
            {
                _id = Guid.NewGuid().ToString();
            }
        }

        public Radio(string id)
        {
            _id = id;
        }

        public string ReceiverId
        {
            get
            {
                return _id;
            }
        }

        public void ReceiveData(string data)
        {
            throw new NotImplementedException();
        }
    }
}
