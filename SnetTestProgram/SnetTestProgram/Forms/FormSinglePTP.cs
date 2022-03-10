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

            // JobQueue 생성하기
            Queue<Action> jobQueue = _job.CreateJobQueue();

            // JobQueue에 Job 할당하기 - 사용자 영역

            // 단축 왕복 운동
            for (int i = 0; i <= repeatNum; i++)
            {
                SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Trapezoidal;

                int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis_1, moveType, velocity, accTime, decTime, 66, startPos);
                });

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis_1, moveType, velocity, accTime, decTime, 66, endPos);
                });

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis_1, moveType, velocity, accTime, decTime, 66, startPos);
                });
            }

            string time = null;
            // Job 실행 함수 람다식 선언
            Action action = () => { time = _job.DoJob(jobQueue, axis_1); };
            // 스레드로 action 실행
            Task task = Task.Factory.StartNew(action);
            // task 끝날 때까지 대기
            await task;

            MessageBox.Show(time + "msec");

            Logger.WriteLog(time + "msec");
        }

        private void timerSPTP_Tick(object sender, EventArgs e)
        {
            int position = 0;

            _snetDevice.GetCommandPosition(axis_1, ref position);

            tbResult.Text = position.ToString();
        }
    }
}
