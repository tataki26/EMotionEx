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
        private Job _job;
        private PollingThread _pollingThread;

        [DllImport("winmm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint timeGetTime();

        public FormInterrupt(SnetDevice snetDevice, Job job)
        {
            InitializeComponent();
            _snetDevice = snetDevice;
            _job = job;

            _pollingThread = new PollingThread(_snetDevice, _job);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int velocity;
            int accTime;
            int decTime;
            int startPos;
            int endPos;
            int dwell;

            int.TryParse(tbVelocity.Text, out velocity);
            int.TryParse(tbAccTime.Text, out accTime);
            int.TryParse(tbDecTime.Text, out decTime);
            int.TryParse(tbRepeatPosition1.Text, out startPos);
            int.TryParse(tbRepeatPosition2.Text, out endPos);
            int.TryParse(tbDwell.Text, out dwell);

            Job.JobList[] jobListArray = new Job.JobList[32];

            for (int i = 0; i < 3; i++)
            {
                Job.JobList jobList = _job.CreateJobList(i, velocity, accTime, decTime, dwell, startPos, endPos);
                _job.UpdateJobListArray(jobList, i, ref jobListArray);
            }

            _job.JobMove(jobListArray);

            /*
            uint ret = _pollingThread.PollingMoveTime(velocity, accTime, decTime, startPos, endPos, dwell);

            MessageBox.Show("Motion Done: "+ret + "ms");
            */

        }
    }
}
