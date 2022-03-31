using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMotionSnetBase;

namespace DllTestProgram
{
    public partial class mainForm : Form
    {
        private int _net = 1;
        private bool _bConnect = false;
        private SnetDevice _snetDevice = new SnetDevice();

        public mainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int status = 0;
            try
            {
                status = _snetDevice.Connect(_net, _bConnect);
                if (status == 0)
                {
                    Debug.WriteLine("Connect Success!!!");
                    MessageBox.Show("Connect Success!!!");
                    _bConnect = true;
                }
                else Debug.WriteLine("Connect Fail!!!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
