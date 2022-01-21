using System.Threading;
using VtpLibrary;

namespace TdLibrary
{
    public class ThreadingData
    {
        public int lnum;
        public string lstr;
        public bool lflag = true;

        public int inum;
        public string istr;
        public bool iflag = true;

        public int lUpdatedAddr;
        public int iUpdatedAddr;

        public bool flag=false;

        IMcs dllProtocol;
        Thread thread;

        public ThreadingData(IMcs imcs)
        {
            dllProtocol = imcs;
        }

        public void Connect(string host, int port)
        {
            if (!flag)
            {
                flag = true;
                dllProtocol.Connect_Udp_Client(host, port);
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
                dllProtocol.Set_LR_Var(lUpdatedAddr, ref lflag, ref lnum, ref lstr);
                dllProtocol.Set_I_Var(iUpdatedAddr, ref iflag, ref inum, ref istr);

            }

        }

    }
}
