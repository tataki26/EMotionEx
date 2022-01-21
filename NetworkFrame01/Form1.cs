using System;
using System.Windows.Forms;
using VtpLibrary;
using TdLibrary;

namespace NetworkFrame01
{
    public partial class Form1 : Form
    {
        private IMcs vtp = new VirtualTableProtocol();
        private ThreadingData td;

        public Form1()
        {
            InitializeComponent();
            td = new ThreadingData(vtp);
            timer1.Start();
        }

        int data;
        int addr;

        private void write_Qp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Qp.Text, out addr);
            int.TryParse(data_Qp.Text, out data);
            
            vtp.Set_Q_Var(addr, data);
        }

        private void write_Lp_Click(object sender, EventArgs e)
        {
            int.TryParse(addr_Lpw.Text, out addr);
            int.TryParse(data_Lpw.Text, out data);

            vtp.Set_LW_Var(addr, data);
           
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
            
            td.lUpdatedAddr = addr;

            data_Lpr.Text = data.ToString();
        }

        private void addr_Ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDt = addr_Ip.SelectedIndex.ToString();
            int.TryParse(selectedDt, out addr);
            
            td.iUpdatedAddr = addr;

            data_Ip.Text = data.ToString();
        }
        private void cnnt_Btn_Click(object sender, EventArgs e)
        {
            string host = "192.168.240.2";
            // string host = "192.168.120.3"; // >> 연결 오류 확인용 코드
            int port = 2025;

            td.Connect(host, port);

            // MessageBox.Show("접속 완료!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (td.lUpdateData!=null) data_Lpr.Text = td.lUpdateData.ToString();
            if (td.iUpdateData!=null) data_Ip.Text = td.iUpdateData.ToString();

        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            td.Disconnect();
            MessageBox.Show("접속 종료!");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            td.Disconnect();
        }
    }
}
