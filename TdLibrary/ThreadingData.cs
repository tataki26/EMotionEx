using System.Threading;
using VtpLibrary;

namespace TdLibrary
{
    public class ThreadingData
    {
        public int lUpdatedAddr;
        public int iUpdatedAddr;

        public object lUpdateData;
        public object iUpdateData;

        public bool flag=false;

        IMcs virtualTableProtocol;
        Thread thread;

        public ThreadingData(IMcs imcs)
        {
            virtualTableProtocol = imcs;
        }

        public void Connect(string host, int port)
        {
            if (!flag)
            {
                flag = true;
                virtualTableProtocol.Connect_Udp_Client(host, port);
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
                virtualTableProtocol.Set_LR_Var(lUpdatedAddr, ref lUpdateData);
                virtualTableProtocol.Set_I_Var(iUpdatedAddr, ref iUpdateData);

            }

        }

    }
}
