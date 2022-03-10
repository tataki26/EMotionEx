using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using EMotionSnetBase;

namespace SnetTestProgram.Forms
{
    public partial class FormConnect : Form
    {
        private int _net = 1;
        private bool _bConnect = false;

        private SnetDevice _snetDevice;

        public FormConnect(SnetDevice snetDevice)
        {
            InitializeComponent();
            _snetDevice = snetDevice;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            int status = 0;
            try
            {
                status = _snetDevice.Connect(_net, false);
                if (status == 0)
                {
                    Debug.WriteLine("Connect Success!!!");
                    MessageBox.Show("Connect Success!!!");
                    _bConnect = true;
                }
                else Debug.WriteLine("Connect Fail!!!");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
           

        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            int status = 0;
            status = _snetDevice.Disconnect();

            if (status == 0)
            {
                Debug.WriteLine("Disconnect Success!!!");
                MessageBox.Show("Disconnect Fail!!!");
            }
            else if (status == 50) Debug.WriteLine("Disconnected");
            else if (status == 51) Debug.WriteLine("Disconnecting...");
            else Debug.WriteLine("Disconnect Fail!!!: " + status);
            
        }

        private void buttonAxis_Click(object sender, EventArgs e)
        {
            FormAxis formAxis = new FormAxis();
            formAxis.Show();
        }
    }
}
