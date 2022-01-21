using System.Threading;
using VtpLib;

namespace TdLibrary
{
    public class ThreadingData
    {
        public int lUpdatedAddr;
        public int iUpdatedAddr;

        public object lUpdatedData;
        public object iUpdatedData;

        public bool flag=false;

        Thread thread;

        VirtualTableProtocol vtp = new VirtualTableProtocol();

        public void Connect(string host, int port)
        {
            if (!flag)
            {
                flag = true;
                vtp.Connect_Udp_Client(host, port);
                thread = new Thread(new ThreadStart(ThreadGetData));
                thread.Start();
            }

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
                vtp.Set_LR_Var(lUpdatedAddr, ref lUpdatedData);
                vtp.Set_I_Var(iUpdatedAddr, ref iUpdatedData);

            }

        }

    }
}
