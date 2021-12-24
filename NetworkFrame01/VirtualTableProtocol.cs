using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFrame01
{
    class VirtualTableProtocol
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
        
        public int Set_LR_Var(int addr)
        {
            int virtualAddr = (2*addr) + 400000;
            byte[] netFrame = frame.make_Net_Frame(32, virtualAddr);

            network.send_Udp_Client(netFrame);
            byte[] data = network.receive_Udp_Client();

            int deData = frame.decode_Net_Frame(32, data);

            return deData;
        }
        
        public int Set_I_Var(int addr)
        {
            int virtualAddr = addr + 120000;
            byte[] netFrame = frame.make_Net_Frame(16, virtualAddr);

            network.send_Udp_Client(netFrame);
            byte[] data = network.receive_Udp_Client();

            int deData = frame.decode_Net_Frame(16, data);

            return deData;
        }
        
        public void Connect_Udp_Client(string host, int port)
        {
            network.Connect(host, port);
         
        }

    }
    
}
