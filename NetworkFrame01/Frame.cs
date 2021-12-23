using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFrame01
{
    class Frame
    {
        private byte frame_Len(int bit)
        {
            byte frmLen;

            if (bit == 16)
            {
                frmLen = 0x0E;
            }
            else
            {
                frmLen = 0x12;
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

        public byte[] make_Net_Frame(int bit, int addr, int data)
        {
            byte[] netFrame = new byte[1500];

            netFrame[0] = 0x07;
            netFrame[1] = frame_Len(bit);
            netFrame[2] = 0x77;
            netFrame[3] = 0x54;

            byte[] newAddr = change_Addr(addr);

            for (int i = 4; i <= 9; i++)
            {
                netFrame[i] = cnvt_To_Ascii(newAddr)[i - 4];
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

            if (bit == 32)
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

                netFrame = netFrame.Take(22).ToArray();
            }

            return netFrame;
        }

        public byte[] make_Net_Frame(int bit, int addr)
        {
            byte[] netFrame = new byte[1500];

            netFrame[0] = 0x07;
            netFrame[1] = 0x0A;
            netFrame[2] = 0x72;
            netFrame[3] = 0x54;

            byte[] newAddr = change_Addr(addr);

            for (int i = 4; i <= 9; i++)
            {
                netFrame[i] = cnvt_To_Ascii(newAddr)[i - 4];
            }

            netFrame[10] = 0x30;
            netFrame[11] = data_Len(bit);
            netFrame[12] = cal_Chksum(netFrame);
            netFrame[13] = 0xF0;

            netFrame = netFrame.Take(14).ToArray();

            return netFrame;
        }
    }
}
