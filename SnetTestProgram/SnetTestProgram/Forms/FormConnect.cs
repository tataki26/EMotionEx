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

            status = _snetDevice.Connect(_net, false);
            if (status == 0)
            {
                Debug.WriteLine("Connect Success!!!");
                _bConnect = true;
            }
            else Debug.WriteLine("Connect Fail!!!");
        }
    }
}
