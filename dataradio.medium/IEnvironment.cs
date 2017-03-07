using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataradio.medium
{
    interface IEnvironment
    {
        void SetRadios(IList<IList<IReceiver>> radios);
    }
}
