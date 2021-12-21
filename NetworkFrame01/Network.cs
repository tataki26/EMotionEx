﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace NetworkFrame01
{
    class Network
    {   
        private byte frame_Len(bool isWrite, int bit)
        {
            byte frmLen;

            if (isWrite == true)
            {
                if (bit == 16)
                {
                    frmLen = 0x0E;
                }
                else
                {
                    frmLen = 0x12;
                }
            }
            else
            {
                frmLen = 0x0A;
            }

            return frmLen;
        }
        private byte[] change_Addr(int addr) // 자리 교환
        {
            byte[] tempBytes = BitConverter.GetBytes(addr);

            return tempBytes;
        }
        private byte[] cnvt_To_Ascii(byte[] hex)
        {
            string hexString = BitConverter.ToString(hex);
            hexString = hexString.Replace("-", "");
            byte[] hexBytes = Encoding.UTF8.GetBytes(hexString);

            return hexBytes;
        }

        private byte data_Len(int bit)
        {
            byte dtlen;

            if (bit == 16)
            {
                dtlen = 0x31;
            }
            else
            {
                dtlen = 0x32;
            }

            return dtlen;
        }
        
        private byte cal_Chksum(byte[] netFrame)
        {
            int sum = 0;

            for (int i = 0; i < netFrame.Length; i++)
            {
                sum += netFrame[i]; 
            }

            byte[] chksum = BitConverter.GetBytes(sum);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(chksum);

            return chksum[3];
        }

        public byte[] make_Net_Frame(bool isWrite, int bit, int addr, int data)
        {
            byte[] netFrame = new byte[1500];

            netFrame[0] = 0x07;
            netFrame[1] = frame_Len(isWrite, bit);

            if (isWrite == true)
            {
                netFrame[2] = 0x77;
            }
            else
            {
                netFrame[2] = 0x72;
            }

            netFrame[3] = 0x54;

            byte[] newAddr = change_Addr(addr);

            for (int i=4; i<=9; i++)
            {
                netFrame[i] = cnvt_To_Ascii(newAddr)[i-4];
            }

            netFrame[10] = 0x30;
            netFrame[11] = data_Len(bit);

            byte[] dtBytes = BitConverter.GetBytes(data);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(dtBytes);

            byte[] newData = cnvt_To_Ascii(dtBytes);

            if (bit == 16)
            {
                for (int i = 4; i <= 7; i++)
                {
                    netFrame[i + 8] = newData[i];
                }

                netFrame[16] = cal_Chksum(netFrame);
                netFrame[17] = 0xF0;
                netFrame = netFrame.Take(18).ToArray();
            }
            else
            {
                for (int i = 4; i <= 7; i++)
                {
                    netFrame[i + 8] = newData[i];
                }

                for (int i = 0; i <= 3; i++)
                {
                    netFrame[i + 16] = newData[i];
                }

                netFrame[20] = cal_Chksum(netFrame);
                netFrame[21] = 0xF0;
            }

            return netFrame;
        }

        public void connect_Udp_Client(byte[] netFrame)
        {
            UdpClient cli = new UdpClient();

            string host = "192.168.240.2";
            int port = 2050;

            cli.Send(netFrame, netFrame.Length, host, port);
            Console.WriteLine("[Send] {0}:{1}로 {2} 바이트 전송", host,port,netFrame.Length);

            IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
            byte[] dataBytes = cli.Receive(ref epRemote);
            Console.WriteLine("[Receive] {0}로부터 {1} 바이트 전송", epRemote.ToString(), dataBytes.Length);

            cli.Close();
        }

    }
}
