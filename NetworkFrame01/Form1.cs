using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using McsProgram;

namespace NetworkFrame01
{
    public partial class Form1 : Form
    {
        // private IMcs virtualTableProtocol = new VirtualTableProtocol();
        private IMcs virtualTableProtocol = new DllProtocol();
        private ThreadingData threadingData;

        public Form1()
        {
            InitializeComponent();
            threadingData = new ThreadingData(virtualTableProtocol);
            timer1.Start();
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
        private void Form1_Load(object sender, EventArgs e)
        {
            addr_Lpr.SelectedIndex = 0;
            addr_Ip.SelectedIndex = 0;
        }

        private void addr_Lrw_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDt = addr_Lpr.SelectedIndex.ToString();
            int.TryParse(selectedDt, out addr);
            
            threadingData.lVarAddress = addr;

            data_Lpr.Text = data.ToString();
        }

        private void addr_Ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDt = addr_Ip.SelectedIndex.ToString();
            int.TryParse(selectedDt, out addr);
            
            threadingData.iVarAddress = addr;

            data_Ip.Text = data.ToString();
        }
        private void cnnt_Btn_Click(object sender, EventArgs e)
        {
            string host = "192.168.240.2";
            int port = 2025;

            //virtualTableProtocol.Connect_Udp_Client(host, port); >> 중복
            threadingData.Connect(host, port);

            MessageBox.Show("접속 완료!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            data_Lpr.Text = threadingData.lVar.ToString();
            data_Ip.Text = threadingData.iVar.ToString();
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            threadingData.Disconnect();
            MessageBox.Show("접속 종료!");
        }

    }
}
