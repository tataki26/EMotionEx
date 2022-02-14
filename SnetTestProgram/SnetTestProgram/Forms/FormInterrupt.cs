using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EMotionSnetBase;

namespace SnetTestProgram.Forms
{
    public partial class FormInterrupt : Form
    {
        private SnetDevice _snetDevice;
        private PollingThread _pollingThread;

        [DllImport("winmm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint timeGetTime();

        public FormInterrupt(SnetDevice snetDevice)
        {
            InitializeComponent();
            _snetDevice = snetDevice;
            _pollingThread = new PollingThread(_snetDevice);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int velocity;
            int accTime;
            int decTime;
            int startPos;
            int endPos;

            int.TryParse(tbVelocity.Text, out velocity);
            int.TryParse(tbAccTime.Text, out accTime);
            int.TryParse(tbDecTime.Text, out decTime);
            int.TryParse(tbRepeatPosition1.Text, out startPos);
            int.TryParse(tbRepeatPosition2.Text, out endPos);

            uint ret = _pollingThread.PollingMoveTime(velocity, accTime, decTime, startPos, endPos);

            MessageBox.Show("Motion Done: "+ret + "ms");

        }
    }
}
