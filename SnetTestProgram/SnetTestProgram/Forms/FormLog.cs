using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnetTestProgram.Forms
{
    public partial class FormLog : Form
    {
        Logger logger = new Logger();

        public FormLog()
        {
            InitializeComponent();

            lvLog.View = View.Details;
            lvLog.FullRowSelect = true;
            lvLog.GridLines = true;

        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            string currentDirectoryPath = Environment.CurrentDirectory.ToString();
            string directoryPath = Path.Combine(currentDirectoryPath, "interruptLog");

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = directoryPath;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lvLog.Items.Clear();

                string fileName = "";
                fileName = ofd.FileName;

                if (!File.Exists(fileName))
                {
                    MessageBox.Show("No such file or no selected file", "check", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    string[] strValue = File.ReadAllLines(fileName);

                    for (int i = 0; i < strValue.Length; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = (i + 1).ToString();
                        lvi.SubItems.Add(strValue[i]);

                        lvLog.Items.Add(lvi);
                    }
                }
            }
            // string filePath = directoryPath + @"\MotionDoneTime_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
        }
    }
    
}
