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

        }

        int axis_1;

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
            int.TryParse(tbPosition_2.Text, out endPos);

            int.TryParse(tbVelocity.Text, out velocity);
            int.TryParse(tbAccTime.Text, out accTime);
            int.TryParse(tbDecTime.Text, out decTime);
            int.TryParse(tbRepeatNumber.Text, out repeatNum);
            int.TryParse(tbDwell.Text, out dwell);

            _job.SetAxis(axis_1);

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

            Action job3 = () =>
            {
                returnCode = _snetDevice.MoveSingleEx(axis_1, moveType, velocity, accTime, decTime, 66, startPos);
            };

            _job.AddJob(jobQueue, job1);
            _job.AddJob(jobQueue, job2);
            _job.AddJob(jobQueue, job3);

            int max = 0;
            int min = 0; // calc 함수 만들고 min 값 넘기다가 끝남 0으로 출력
            int avg=0;

            string time = null;
            // Job 실행 함수 람다식 선언
            Action action = () => { time = _job.RepeatJob(repeatNum, dwell, jobQueue, axis_1, ref max, ref min, ref avg); };
            // 스레드로 action 실행
            Task task = Task.Factory.StartNew(action);
            // task 끝날 때까지 대기
            await task;

            MessageBox.Show(time + "msec"+'\n'+"min: "+min+"msec"+'\n'+"avg: "+avg+"msec"+'\n');
            MessageBox.Show(time + "msec" + '\n' + "min: " + min + "msec" + '\n' +"avg: "+avg+"msec"+'\n');


            Logger.WriteLog(time + "msec"+", "+ "min: " + min + "msec");
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
    }
}
