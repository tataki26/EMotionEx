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
using System.Threading;

namespace SnetTestProgram.Forms
{
    public partial class FormInterrupt : Form
    {
        private SnetDevice _snetDevice;
        private Job _job;

        public FormInterrupt(SnetDevice snetDevice, Job job)
        {
            InitializeComponent();

            _snetDevice = snetDevice;
            _job = job;

            timer.Start();

        }

        // start 버튼: single axis move에 대한 왕복 실험
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            // Form에 입력된 data 가져오기
            int velocity;
            int accTime;
            int decTime;
            int startPos;
            int endPos;
            int repeatNum;
            int dwell;

            int.TryParse(tbVelocity.Text, out velocity);
            int.TryParse(tbAccTime.Text, out accTime);
            int.TryParse(tbDecTime.Text, out decTime);
            int.TryParse(tbRepeatPosition1.Text, out startPos);
            int.TryParse(tbRepeatPosition2.Text, out endPos);
            int.TryParse(tbRepeatNumber.Text, out repeatNum);
            int.TryParse(tbDwell.Text, out dwell);

            // JobQueue 생성하기
            Queue<Action> jobQueue = _job.CreateJobQueue();

            // JobQueue에 Job 할당하기 - 사용자 영역
            for(int i = 0; i <= repeatNum; i++)
            {
                int axis = 0;
                SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Trapezoidal;
                
                int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
                
                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis, moveType, velocity, accTime, decTime, 66, startPos);
                });

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis, moveType, velocity, accTime, decTime, 66, endPos);
                });

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis, moveType, velocity, accTime, decTime, 66, startPos);
                });

            }

            string time=null;
            // Job 실행 함수 람다식 선언
            Action action = () => { time=_job.DoJobPolling(jobQueue); };
            // 스레드로 action 실행
            Task task = Task.Factory.StartNew(action);
            // task 끝날 때까지 대기
            await task;

            MessageBox.Show(time+"msec");

            Logger.WriteLog(time+"msec");

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int position = 0;
            _snetDevice.GetCommandPosition(0, ref position);
            tbResult.Text = position.ToString();
        }
    }
}
