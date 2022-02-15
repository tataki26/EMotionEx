using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMotionSnetBase;

namespace SnetTestProgram
{
    public partial class FormMainMenu : Form
    {
        #region Fields
        private Button currentButton;
        private Form activeForm;

        private SnetDevice _snetDevice = new SnetDevice();
        private Job _job = new Job();
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

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            ActivateButton(btnSender);

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            this.panelDesktopPanel.Controls.Add(childForm);
            this.panelDesktopPanel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();

            lbTitle.Text = childForm.Text;

        }
        #endregion

        #region Events
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // ActivateButton(sender);
            OpenChildForm(new Forms.FormConnect(_snetDevice), sender);
        }

        private void btnInterrupt_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormInterrupt(_snetDevice, _job), sender);
        }
        #endregion
    }
}
