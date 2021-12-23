using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace NetworkFrame01
{
    class Network
    {
        UdpClient cli = new UdpClient();

        public void Connect(string host, int port)
        {
            cli.Connect(host, port);
        }

        public void send_Udp_Client(byte[] netFrame)
        {
            cli.Send(netFrame, netFrame.Length);
            MessageBox.Show("전송 완료!");
        }

        public byte[] receive_Udp_Client()
        {
            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);

            byte[] dataBytes = cli.Receive(ref epRemote);

            return dataBytes;
        }
        
    }
}
