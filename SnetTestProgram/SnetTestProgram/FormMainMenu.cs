﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnetTestProgram
{
    public partial class FormMainMenu : Form
    {
        #region Fields
        private Button currentButton;
        #endregion

        public FormMainMenu()
        {
            InitializeComponent();
        }

        #region Methods
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(140, 200, 255);
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Bahnschrift", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.CornflowerBlue;
                    previousBtn.ForeColor = Color.AliceBlue;
                    previousBtn.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        #endregion

        #region Events
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btnInterrupt_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }
        #endregion
    }
}
