using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkFrame01
{
    class ThreadingData
    {
        int lVariable;
        int iVariable;
       

        VirtualTableProtocol virtualTableProtocol;

        public void Connect(string host, int port)
        {
            virtualTableProtocol.Connect_Udp_Client(host, port);
            
            Thread thread = new Thread(ThreadGetData);
            thread.Start();
        }

        public void ThreadGetData()
        {
            
        }

        // data_Ip.Invoke(new Action(() => { data_Ip.Text = count.ToString(); }));
    }
}
