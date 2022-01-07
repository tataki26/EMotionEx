using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using McsProgram;

namespace NetworkFrame01
{
    class ThreadingData
    {
        public int lVar;
        public int iVar; // 타이머

        public int lVarAddress;
        public int iVarAddress; // 버튼

        bool flag=true;

        IMcs virtualTableProtocol;
        Thread thread;

        public ThreadingData(IMcs imcs)
        {
            virtualTableProtocol = imcs;
        }

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
                lVar = virtualTableProtocol.Set_LR_Var(lVarAddress);
                iVar = virtualTableProtocol.Set_I_Var(iVarAddress);
            }

        }

    }
}
