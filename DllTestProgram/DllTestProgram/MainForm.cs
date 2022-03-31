using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMotionSnetBase;
using EMotionUniBase;

namespace DllTestProgram
{
    public partial class mainForm : Form
    {
        private SnetDevice _snetDevice = new SnetDevice();
        private UniDevice _uniDevice = new UniDevice();

        public mainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int s_net = 1;
            bool reConnect = false;
            
            int s_status = 0;

            try
            {
                s_status = _snetDevice.Connect(s_net, reConnect);
                if (s_status == (int)UniDevice.eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("[SNET] Connection Success");
                    MessageBox.Show("[SNET] Connection Success");
                    reConnect = true;
                }
                else Debug.WriteLine("[SNET] Connection Fail");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

            int u_net = 4;
            int port = 10025;

            int u_status = 0;

            try
            {
                u_status = _uniDevice.Connect(u_net, port);
                if (u_status == (int)UniDevice.eUniApiReturnCode.Success)
                {
                    MessageBox.Show("[UNI] Connection Success");
                    Debug.WriteLine("[UNI] Connection Success");
                }
                else Debug.WriteLine("[UNI] Connection Fail");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            int s_status = 0;
      
            s_status = _snetDevice.Disconnect();

            if(s_status == (int)SnetDevice.eSnetApiReturnCode.Success)
            {
                MessageBox.Show("[SNET] Disconnection Success");
                Debug.WriteLine("[SNET] Disconnection Success");
            }

            int u_status = 0;
            u_status = _uniDevice.Disconnect();

            if (u_status == (int)UniDevice.eUniApiReturnCode.Success)
            {
                MessageBox.Show("[UNI] Disconnection Success");
                Debug.WriteLine("[UNI] Disconnection Success");
            }
        }
    }
}
