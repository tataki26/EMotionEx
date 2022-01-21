using System;

namespace VtpLibrary
{
    public interface IMcs
    {
        void Set_Q_Var(int addr, int data);
        void Set_LW_Var(int addr, int data);
        void Set_LR_Var(int addr, ref bool flag, ref int num, ref string str);
        void Set_I_Var(int addr, ref bool flag, ref int num, ref string str);
        void Connect_Udp_Client(string host, int port);
    }
    public class VirtualTableProtocol : IMcs
    {
        private Network network = new Network();
        private Frame frame = new Frame();

        public void Set_Q_Var(int addr, int data)
        {
            int virtualAddr = addr + 130000;

            byte[] netFrame = frame.make_Net_Frame(16, virtualAddr, data);

            network.send_Udp_Client(netFrame);
            network.receive_Udp_Client();
        }

        public void Set_LW_Var(int addr, int data)
        {
            int virtualAddr = (2 * addr) + 400000;

            byte[] netFrame = frame.make_Net_Frame(32, virtualAddr, data);

            network.send_Udp_Client(netFrame);
            network.receive_Udp_Client();
        }

        public void Set_LR_Var(int addr, ref bool flag, ref int num, ref string str)
        {
            int virtualAddr = (2 * addr) + 400000;
            byte[] netFrame = frame.make_Net_Frame(32, virtualAddr);

            network.send_Udp_Client(netFrame);
            byte[] data = network.receive_Udp_Client();

            if (data[0] != 7)
            {
                flag = false;

                if (data[0] != 245) str = "NACK";
                else str = "Wrong Type";
            }
            else num = frame.decode_Net_Frame(32, data);

        }

        public void Set_I_Var(int addr, ref bool flag, ref int num, ref string str)
        {
            int virtualAddr = addr + 120000;
            byte[] netFrame = frame.make_Net_Frame(16, virtualAddr);

            network.send_Udp_Client(netFrame);
            byte[] data = network.receive_Udp_Client();

            if (data[0] == 7)
            {
                flag = false;
                str = "NACK";
            }
            else num = frame.decode_Net_Frame(16, data);

        }

        public void Connect_Udp_Client(string host, int port)
        {
            network.Connect(host, port);

        }
    }
}
