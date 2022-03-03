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

namespace SnetTestProgram.Forms
{
    public partial class FormAxis : Form
    {
        internal int maxSelectAxisGridCol = 2;
        public int[] checkedArr = new int[32];

        public FormAxis()
        {
            InitializeComponent();
            
            DataTable dt = new DataTable();

            dt.Columns.Add("no.", typeof(int));
            dt.Columns.Add("hardware no.", typeof(string));
            dt.Columns.Add("use", typeof(bool));
            dt.Columns.Add("name", typeof(string));

            for(int i = 0; i < 32; i++)
            {
                dt.Rows.Add(i,string.Format("axis_{0}", i), false, string.Format("ax{0}", i));
            }

            dgvAxis.DataSource = dt;
            
        }

        // 클래스 만들어서 값 넘기기
        private void buttonConfig_Click(object sender, EventArgs e)
        {
            bool check = false;

            for (int i = 0; i < 32; i++)
            {
                DataRowView a = dgvAxis.Rows[i].DataBoundItem as DataRowView;

                check = (a.Row.ItemArray[2] as bool?) ?? false;

                if (check == true)
                {
                    checkedArr[i] = (a.Row.ItemArray[0] as int?) ?? 0;
                }
            }

        }

    }
}
