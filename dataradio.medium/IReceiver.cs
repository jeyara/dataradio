using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.medium
{
    public interface IReceiver
    {
        string ReceiverId { get; }
        ReceiveData(string data);
    }
}
