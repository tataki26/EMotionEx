using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkFrame01
{
    public partial class Form1 : Form
    {
        private VirtualTableProtocol virtualTableProtocol = new VirtualTableProtocol();

        public Form1()
        {
            InitializeComponent();
        }

        int data;
        int addr;

        private void write_Qp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Qp.Text, out addr);
            int.TryParse(data_Qp.Text, out data);
            
            string tempText = string.Empty;
            byte[] msgArr = virtualTableProtocol.Set_Q_Var(addr, data);
            foreach(byte tempByte in msgArr)
            {
                tempText += tempByte.ToString("X2");
                tempText += " ";
            }

            qp_Result.Text = tempText;

        }

        private void write_Lp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Lpw.Text, out addr);
            int.TryParse(data_Lpw.Text, out data);

            string tempText = string.Empty;
            byte[] msgArr = virtualTableProtocol.Set_LW_Var(addr, data);
            foreach (byte tempByte in msgArr)
            {
                tempText += tempByte.ToString("X2");
                tempText += " ";
            }

            lpw_Result.Text = tempText;

        }

        private void read_Lp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Lpr.Text, out addr);

            string tempText = string.Empty;
            
            byte[] msgArr = virtualTableProtocol.Set_LR_Var(addr);
            foreach (byte tempByte in msgArr)
            {
                tempText += tempByte.ToString("X2");
                tempText += " ";
            }

            lpr_Result.Text = tempText;

            string tempData = string.Empty;

            byte[] dtArr = virtualTableProtocol.Get_LR_Var(addr);

            int[] numArr = new int[16];
            char[] charArr = new char[16];

            for (int i = 3; i <=6; i++)
            {
                numArr[i - 3] = Convert.ToInt32(dtArr[i+4]);
                charArr[i - 3] = Convert.ToChar(numArr[i - 3]);

                tempData += charArr[i - 3].ToString();
            }

            for (int i = 7; i < (dtArr.Length) - 2; i++)
            {
                numArr[i - 3] = Convert.ToInt32(dtArr[i-4]);
                charArr[i - 3] = Convert.ToChar(numArr[i - 3]);

                tempData += charArr[i - 3].ToString();
            }

            data_Lpr.Text = Convert.ToInt32(tempData, 16).ToString();
        }

        private void read_Ip_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Ip.Text, out addr);

            string tempText = string.Empty;

            byte[] msgArr = virtualTableProtocol.Set_I_Var(addr);
            foreach (byte tempByte in msgArr)
            {
                tempText += tempByte.ToString("X2");
                tempText += " ";
            }

            ip_Result.Text = tempText;

            string tempData = string.Empty;

            byte[] dtArr = virtualTableProtocol.Get_I_Var(addr);

            int[] numArr = new int[4];
            char[] charArr = new char[4];

            for (int i = 3; i <(dtArr.Length)-2; i++)
            {
                numArr[i-3] = Convert.ToInt32(dtArr[i]);
                charArr[i-3] = Convert.ToChar(numArr[i-3]);

                tempData += charArr[i-3].ToString();
            }

            data_Ip.Text = Convert.ToInt32(tempData, 16).ToString();
        }

        private void cnnt_Btn_Click(object sender, EventArgs e)
        {
            string host = "192.168.240.2";
            int port = 2025;

            virtualTableProtocol.Connect_Udp_Client(host, port);

            MessageBox.Show("접속 완료!");
        }
    }
}
