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
        public object lresult;
        public object iresult;

        public int lVarAddress;
        public int iVarAddress; // 버튼

        bool flag=true;     

        VirtualTableProtocol virtualTableProtocol = new VirtualTableProtocol();
        Thread thread;

        public void Connect(string host, int port)
        {
            virtualTableProtocol.Connect_Udp_Client(host, port);

            thread = new Thread(new ThreadStart(ThreadGetData));
            thread.Start();
        }

        public void Disconnect()
        {
            flag = false;
            thread?.Join(); // Null이면 무시
        }

        public void ThreadGetData()
        {
            while(flag)
            {
                virtualTableProtocol.Set_LR_Var(lVarAddress, ref lresult);
                virtualTableProtocol.Set_I_Var(iVarAddress, ref iresult);
            }

        }

    }
}
