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

        public byte[] Set_Q_Var(int addr, int data)
        {
            int virtualAddr = addr + 130000;

            byte[] result = network.make_Net_Frame(16, virtualAddr, data);

            network.send_Udp_Client(result);

            return result;
        }

        public byte[] Set_LW_Var(int addr, int data)
        {
            int virtualAddr = (2 * addr) + 400000;

            byte[] result = network.make_Net_Frame(32, virtualAddr, data);

            network.send_Udp_Client(result);

            return result;
        }
        public byte[] Set_LR_Var(int addr)
        {
            int virtualAddr = (2 * addr) + 400000;
            
            byte[] result = network.make_Net_Frame(32, virtualAddr);

            network.send_Udp_Client(result);

            return result;
        }

        public byte[] Set_I_Var(int addr)
        {
            int virtualAddr = addr + 120000;
            byte[] result = network.make_Net_Frame(16, virtualAddr);

            network.send_Udp_Client(result);

            return result;
        }

        public void Connect_Udp_Client(string host, int port)
        {
            network.Connect(host, port);
         
        }

    }
    
}
