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
            int.TryParse(data_Lpr.Text, out data);

            string tempText = string.Empty;
            byte[] msgArr = virtualTableProtocol.Set_LR_Var(addr, data);
            foreach (byte tempByte in msgArr)
            {
                tempText += tempByte.ToString("X2");
                tempText += " ";
            }

            lpr_Result.Text = tempText;

        }

        private void read_Ip_Click(object sender, EventArgs e)
        {

            int.TryParse(addr_Ip.Text, out addr);
            int.TryParse(data_Ip.Text, out data);

            string tempText = string.Empty;
            byte[] msgArr = virtualTableProtocol.Set_I_Var(addr, data);
            foreach (byte tempByte in msgArr)
            {
                tempText += tempByte.ToString("X2");
                tempText += " ";
            }

            ip_Result.Text = tempText;

        }

        private void cnntBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
