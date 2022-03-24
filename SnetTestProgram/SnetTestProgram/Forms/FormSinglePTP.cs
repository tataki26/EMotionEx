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

namespace SnetTestProgram.Forms
{
    public partial class FormSinglePTP : Form
    {
        private SnetDevice _snetDevice;
        private Job _job;

        public FormSinglePTP(SnetDevice snetDevice, Job job)
        {
            InitializeComponent();

            _snetDevice = snetDevice;
            _job = job;

            timerSPTP.Start();
            timerCount.Start();

            tbType.Text = _job.Type;

        }

        int axis_1;
        int cnt = 0;
        bool flag = false;

        #region Method
        public bool isSamePos(int startPos)
        {
            int position = 0;
            _snetDevice.GetCommandPosition(axis_1, ref position);

            if (position == startPos)
            {
                MessageBox.Show("지령 위치가 현재 위치와 같습니다" + '\n' + "다른 값을 입력하세요");
                flag = true;
            }
            return flag;
        }
        #endregion

        #region Events
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            // Form에 입력된 data 가져오기
            int startPos;
            int endPos;

            int velocity;
            int accTime;
            int decTime;
            int repeatNum;
            int dwell;

            int.TryParse(tbAxis_1.Text, out axis_1);
            
            int.TryParse(tbPosition_1.Text, out startPos);
            
            flag=isSamePos(startPos);
            
            if (flag)
            {
                Close();
                return;
            }

            int.TryParse(tbPosition_2.Text, out endPos);

            int.TryParse(tbVelocity.Text, out velocity);
            int.TryParse(tbAccTime.Text, out accTime);
            int.TryParse(tbDecTime.Text, out decTime);
            int.TryParse(tbRepeatNumber.Text, out repeatNum);
            int.TryParse(tbDwell.Text, out dwell);

            Queue<Action> jobQueue = new Queue<Action>();

            // JobQueue에 Job 할당하기 - 사용자 영역

            // 단축 왕복 운동
            SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Trapezoidal;

            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

            Action job1 = () =>
            {
                returnCode = _snetDevice.MoveSingleEx(axis_1, moveType, velocity, accTime, decTime, 66, startPos);
            };

            Action job2 = () =>
            {
                returnCode = _snetDevice.MoveSingleEx(axis_1, moveType, velocity, accTime, decTime, 66, endPos);
            };

            _job.AddJob(jobQueue, job1);
            _job.AddJob(jobQueue, job2);

            List<int> timeList = new List<int>();
            List<int> maxList = new List<int>();
            
            int min = 0; 
            int avg=0;

            string time = null;
            // Job 실행 함수 람다식 선언
            Action action = () => { time = _job.RepeatJob(repeatNum, dwell, jobQueue, axis_1, ref timeList, ref maxList, ref min, ref avg, ref cnt); };
            // 스레드로 action 실행
            Task task = Task.Factory.StartNew(action);
            // task 끝날 때까지 대기
            await task;

            MessageBox.Show("total: " +time + "msec"+'\n'+"min: "+min+"msec"+'\n'+"avg: "+avg+"msec"+'\n');
            MessageBox.Show("=====Max=====" + '\n' + maxList[0] + "msec" + '\n' + maxList[1] + "msec" + '\n' + maxList[2] + "msec" + '\n');

            Logger.WriteLog("==========<"+_job._type+">===================================================================");
            Logger.WriteLog("total: "+time + "msec, "+ "min: " + min + "msec, " + "avg: " + avg + "msec, "+"count: " + repeatNum);
            Logger.WriteLog("TOP5: " + maxList[0] + "msec, "+ maxList[1] + "msec, " + maxList[2] + "msec, " + maxList[3] + "msec, " + maxList[4] + "msec");
            Logger.WriteLogList(timeList);
            Logger.WriteLog("====================================================================================");
        }

        private void timerSPTP_Tick(object sender, EventArgs e)
        {
            int position = 0;

            _snetDevice.GetCommandPosition(axis_1, ref position);

            tbResult.Text = position.ToString();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _job.StopJob();
        }

        private void timerCount_Tick(object sender, EventArgs e)
        {
            tbResult2.Text = cnt.ToString();
        }
        #endregion
    }
}
