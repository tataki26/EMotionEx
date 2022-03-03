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

        int axis0;
        int axis1;
        int axis2;
        int axis3;

        // start 버튼: single axis move에 대한 왕복 실험
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            // Form에 입력된 data 가져오기
            int axis0Pos;
            int axis1Pos;
            int axis2Pos;
            int axis3Pos;

            int velocity;
            int accTime;
            int decTime;
            int repeatNum;
            int dwell;

            int.TryParse(tbAxis0.Text, out axis0);
            int.TryParse(tbAxis1.Text, out axis1);
            int.TryParse(tbAxis2.Text, out axis2);
            int.TryParse(tbAxis3.Text, out axis3);

            int.TryParse(tbPosition0.Text, out axis0Pos);
            int.TryParse(tbPosition1.Text, out axis1Pos);
            int.TryParse(tbPosition2.Text, out axis2Pos);
            int.TryParse(tbPosition3.Text, out axis3Pos);

            int.TryParse(tbVelocity.Text, out velocity);
            int.TryParse(tbAccTime.Text, out accTime);
            int.TryParse(tbDecTime.Text, out decTime);
            int.TryParse(tbRepeatNumber.Text, out repeatNum);
            int.TryParse(tbDwell.Text, out dwell);

            // JobQueue 생성하기
            Queue<Action> jobQueue = _job.CreateJobQueue();

            // JobQueue에 Job 할당하기 - 사용자 영역

            
            // 단축 왕복 운동
            for(int i = 0; i <= repeatNum; i++)
            {
                FormAxis formaxis = new FormAxis();

                SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Trapezoidal;
                
                int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
                
                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis0, moveType, velocity, accTime, decTime, 66, axis0Pos);
                });

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis0, moveType, velocity, accTime, decTime, 66, axis1Pos);
                });

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveSingleEx(axis0, moveType, velocity, accTime, decTime, 66, axis0Pos);
                });

            }
            

            /*
            // 다축 직선 보간 운동
            for (int i = 0; i <= repeatNum; i++)
            {
                int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveLine(axis0, axis1, axis2, axis3,
                        axis0Pos, axis1Pos, axis2Pos, axis3Pos, velocity, accTime, decTime, 66, 66);
                });
            }
            */

            /*
            // 다축 원호 보간 운동
            for(int i = 0; i <= repeatNum; i++) {

                int cwCcw = -1;
                int radius = 10000;

                int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveArcRadius(axis0, axis1, axis2,
                        axis0Pos, axis1Pos, axis2Pos, cwCcw, radius, velocity, accTime, decTime, 66, 66);
                });
                
            }
            */

            string time=null;
            // Job 실행 함수 람다식 선언
            Action action = () => { time=_job.DoJobPolling(jobQueue,axis0); };
            // 스레드로 action 실행
            Task task = Task.Factory.StartNew(action);
            // task 끝날 때까지 대기
            await task;

            MessageBox.Show(time+"msec");

            Logger.WriteLog(time+"msec");

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int[] position = new int[4];

            for (int i = 0; i < 4; i++)
            {
                _snetDevice.GetCommandPosition(i, ref position[i]);
            }

            tbResult0.Text = position[0].ToString();
            tbResult1.Text = position[1].ToString();
            tbResult2.Text = position[2].ToString();
            tbResult3.Text = position[3].ToString();
        }
    }
}
