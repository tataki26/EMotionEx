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
            
            virtualTableProtocol.Set_Q_Var(addr, data);
        }

        private void write_Lp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Lpw.Text, out addr);
            int.TryParse(data_Lpw.Text, out data);

            virtualTableProtocol.Set_LW_Var(addr, data);
        }

        private void read_Lp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Lpr.Text, out addr);

            data = virtualTableProtocol.Set_LR_Var(addr);

            data_Lpr.Text = data.ToString();
        }
        private void read_Ip_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Ip.Text, out addr);

            data = virtualTableProtocol.Set_I_Var(addr);

            data_Ip.Text = data.ToString();

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
