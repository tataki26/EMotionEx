using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMotionSnetBase;
using EMotionUniBase;

namespace DllTestProgram
{
    public partial class mainForm : Form
    {
        private SnetDevice _snetDevice = new SnetDevice();
        private UniDevice _uniDevice = new UniDevice();

        public bool _sFlag = true;
        public bool _uFlag = true;

        public mainForm()
        {
            InitializeComponent();
        }

        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint timeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint timeEndPeriod(uint uMilliseconds);

        #region Events
        private void btnConnect_Click(object sender, EventArgs e)
        {
            int s_net = 1;
            bool reConnect = false;
            
            int s_status = 0;

            try
            {
                s_status = _snetDevice.Connect(s_net, reConnect);
                if (s_status == (int)UniDevice.eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("[SNET] Connection Success");
                    MessageBox.Show("[SNET] Connection Success");
                    reConnect = true;
                }
                else Debug.WriteLine("[SNET] Connection Fail");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

            int u_net = 4;
            int port = 10025;

            int u_status = 0;

            try
            {
                u_status = _uniDevice.Connect(u_net, port);
                if (u_status == (int)UniDevice.eUniApiReturnCode.Success)
                {
                    MessageBox.Show("[UNI] Connection Success");
                    Debug.WriteLine("[UNI] Connection Success");
                }
                else Debug.WriteLine("[UNI] Connection Fail");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            int s_status = 0;
      
            s_status = _snetDevice.Disconnect();

            if(s_status == (int)SnetDevice.eSnetApiReturnCode.Success)
            {
                MessageBox.Show("[SNET] Disconnection Success");
                Debug.WriteLine("[SNET] Disconnection Success");
            }

            int u_status = 0;
            u_status = _uniDevice.Disconnect();

            if (u_status == (int)UniDevice.eUniApiReturnCode.Success)
            {
                MessageBox.Show("[UNI] Disconnection Success");
                Debug.WriteLine("[UNI] Disconnection Success");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int s_axis = 2;
            int s_pos = 0;

            int s_status = 0;

            int s_success = 0;
            int s_fail = 0;

            int u_axis = 0;
            int u_pos = 0;

            int u_status = 0;

            int u_success = 0;
            int u_fail = 0;

            var sThread = new Thread(() =>
            {
                int i = 0;
                int avg = 0;
                int time = 0;
                int total = 0;

                List<int> timeList = new List<int>();
                timeList.Clear();

                // while (_sFlag)
                while(i<1000)
                {
                    i++;

                    time = 0;

                    Stopwatch s_sw = new Stopwatch();

                    s_sw.Start();

                    s_status = _snetDevice.GetCommandPosition(s_axis, ref s_pos);

                    s_sw.Stop();

                    time = (int)s_sw.ElapsedTicks/100;
                    timeList.Add(time);

                    total += time;
                    avg = total / i;

                    if (s_status == (int)SnetDevice.eSnetApiReturnCode.Success)
                    {
                        s_success++;
                    }
                    else s_fail++;

                    timeBeginPeriod(1);
                    Thread.Sleep(1);
                    timeEndPeriod(1);

                }
                MessageBox.Show("======<SNET>======\n"+"success: " + s_success.ToString() + '\n' + "fail: " + s_fail.ToString());
                MessageBox.Show("[SNET] avg: " + avg+"nsec");

                Logger.WriteLog("======<SNET>======\n" + "success: " + s_success.ToString() + '\n' + "fail: " + s_fail.ToString());
                Logger.WriteLogList(timeList);
                
            }
            );

            var uThread = new Thread(() =>
            {
                int i = 0;
                int avg = 0;
                int time = 0;
                int total = 0;

                List<int> timeList = new List<int>();
                timeList.Clear();

                // while (_uFlag)
                while (i < 1000)
                {
                    i++;

                    time = 0;

                    Stopwatch u_sw = new Stopwatch();

                    u_sw.Start();

                    u_status = _uniDevice.GetCommandPosition(u_axis, ref u_pos);

                    u_sw.Stop();

                    time = (int)u_sw.ElapsedTicks / 100;
                    timeList.Add(time);

                    total += time;
                    avg = total / i;

                    if (u_status == (int)UniDevice.eUniApiReturnCode.Success)
                    {
                        u_success++;
                    }
                    else u_fail++;

                    timeBeginPeriod(1);
                    Thread.Sleep(1);
                    timeEndPeriod(1);
                }
                // MessageBox.Show("======<UNI>======\n"+"success: " +u_success.ToString()+'\n'+"fail: "+u_fail.ToString());
                // MessageBox.Show("[UNI] avg: " + avg + "nsec");

                // Logger.WriteLog("======<UNI>======\n" + "success: " + s_success.ToString() + '\n' + "fail: " + s_fail.ToString());
                // Logger.WriteLogList(timeList);
            }
            );

            sThread.Start();
            uThread.Start();

            sThread.Join();
            // uThread.Join();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _sFlag = false;
            _uFlag = false;

        }

        #endregion
    }
}
