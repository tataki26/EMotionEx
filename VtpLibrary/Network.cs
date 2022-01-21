using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;

namespace VtpLibrary
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
        }

        public byte[] receive_Udp_Client()
        {
            cli.Client.ReceiveTimeout = 5000;
            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);

            byte[] dataBytes = null;

            try
            {
                dataBytes = cli.Receive(ref epRemote);
            }
            catch(SocketException se)
            {
                MessageBox.Show("통신 에러 - TimeOut");
                Debug.WriteLine($"{se.Message}: {se.ErrorCode}");
            }

            return dataBytes;
        }
        
    }
}
