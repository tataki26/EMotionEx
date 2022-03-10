﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMotionSnetBase;

namespace SnetTestProgram.Forms
{
    public partial class FormInterrupt : Form
    {
        private SnetDevice _snetDevice;
        private Job _job;
        private InterruptWait _interruptWait;

        public FormInterrupt(SnetDevice snetDevice, Job job, InterruptWait interruptWait)
        {
            InitializeComponent();

            _snetDevice = snetDevice;
            _job = job;
            _interruptWait = interruptWait;
        }

        private void btnMoveSingle_Click(object sender, EventArgs e)
        {
            FormSinglePTP singlePTP = new FormSinglePTP(_snetDevice, _job);
            singlePTP.Show();
        }

        private void btnMoveMulti_Click(object sender, EventArgs e)
        {
            FormMultiPTP multiPTP = new FormMultiPTP(_snetDevice, _job);
            multiPTP.Show();
        }

        private void btnSingleLine_Click(object sender, EventArgs e)
        {
            FormSingleLine singleLine = new FormSingleLine(_snetDevice, _job);
            singleLine.Show();
        }

        private void btnMultiLine_Click(object sender, EventArgs e)
        {
            FormAxis formAxis = new FormAxis();
            formAxis.Show();
        }

        private void btnRadius_Click(object sender, EventArgs e)
        {
            FormAxis formAxis = new FormAxis();
            formAxis.Show();
        }

        private void btnAngle_Click(object sender, EventArgs e)
        {
            FormAxis formAxis = new FormAxis();
            formAxis.Show();
        }

        private void btnViaPos_Click(object sender, EventArgs e)
        {
            FormAxis formAxis = new FormAxis();
            formAxis.Show();
        }


        private void cbPolling_CheckedChanged(object sender, EventArgs e)
        {
            bool check = cbInterrupt.Checked;
            _interruptWait.InitInterruptTable(check); //false
        }

        private void cbInterrupt_CheckedChanged(object sender, EventArgs e)
        {
            bool check = cbInterrupt.Checked;
            _interruptWait.InitInterruptTable(check);
        }
    }
}
