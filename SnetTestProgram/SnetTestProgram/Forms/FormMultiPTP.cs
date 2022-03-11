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
    public partial class FormMultiPTP : Form
    {
        private SnetDevice _snetDevice;
        private Job _job;

        int axis_1, axis_2, axis_3, axis_4;

        int[] axis;
        string[] text;

        public FormMultiPTP(SnetDevice snetDevice, Job job)
        {
            InitializeComponent();

            _snetDevice = snetDevice;
            _job = job;

            timerMPTP.Start();

        }

        private static int TryToParse(string txt)
        {
            int ax;
            bool result = int.TryParse(txt, out ax);

            if (result)
                return ax;
            else
                return -1;
        }

        private void timerMPTP_Tick(object sender, EventArgs e)
        {
            text = new string[] { tbAxis_1.Text, tbAxis_2.Text, tbAxis_3.Text, tbAxis_4.Text };
            axis = new int[] { axis_1, axis_2, axis_3, axis_4 };

            for (int i = 0; i < 4; i++)
            {
                axis[i] = TryToParse(text[i]);
            }

            int[] currentPos = new int[4];

            for (int i = 0; i < 4; i++)
            {
                _snetDevice.GetCommandPosition(axis[i], ref currentPos[i]);
            }

            tbResult_1.Text = currentPos[0].ToString();
            tbResult_2.Text = currentPos[1].ToString();
            tbResult_3.Text = currentPos[2].ToString();
            tbResult_4.Text = currentPos[3].ToString();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            text = new string[] { tbAxis_1.Text, tbAxis_2.Text, tbAxis_3.Text, tbAxis_4.Text };
            axis = new int[] { axis_1, axis_2, axis_3, axis_4 };
            
            // Form에 입력된 data 가져오기
            int position_1, position_2, position_3, position_4;
            int velocity_1, velocity_2, velocity_3, velocity_4;
            int accTime_1, accTime_2, accTime_3, accTime_4;
            int decTime_1, decTime_2, decTime_3, decTime_4;

            int repeatNum;
            int dwell;

            int.TryParse(tbPosition_1.Text, out position_1);
            int.TryParse(tbPosition_2.Text, out position_2);
            int.TryParse(tbPosition_3.Text, out position_3);
            int.TryParse(tbPosition_4.Text, out position_4);

            int.TryParse(tbVelocity_1.Text, out velocity_1);
            int.TryParse(tbVelocity_2.Text, out velocity_2);
            int.TryParse(tbVelocity_3.Text, out velocity_3);
            int.TryParse(tbVelocity_4.Text, out velocity_4);

            int.TryParse(tbAccTime_1.Text, out accTime_1);
            int.TryParse(tbAccTime_2.Text, out accTime_2);
            int.TryParse(tbAccTime_3.Text, out accTime_3);
            int.TryParse(tbAccTime_4.Text, out accTime_4);

            int.TryParse(tbDecTime_1.Text, out decTime_1);
            int.TryParse(tbDecTime_2.Text, out decTime_2);
            int.TryParse(tbDecTime_3.Text, out decTime_3);
            int.TryParse(tbDecTime_4.Text, out decTime_4);

            int.TryParse(tbRepeatNumber.Text, out repeatNum);
            int.TryParse(tbDwell.Text, out dwell);

            // JobQueue 생성하기
            // Queue<Action> jobQueue = _job.CreateJobQueue();
            Queue<Action> jobQueue = new Queue<Action>();

            // JobQueue에 Job 할당하기 - 사용자 영역

            // 다축 동시 운동
            for (int i = 0; i <= repeatNum; i++)
            {
                int axisCnt = 0;

                for (int j = 0; j < 4; j++)
                {
                    axis[j] = TryToParse(text[j]);
                }

                for (int k = 0; k < 4; k++)
                {
                    if (axis[k] != -1) 
                        axisCnt++;
                }

                int[] moveType = Enumerable.Repeat<int>(1001, 4).ToArray<int>();

                int[] velocity = { velocity_1, velocity_2, velocity_3, velocity_4 };

                int[] accTime = { accTime_1, accTime_2, accTime_3, accTime_4 };

                int[] decTime = { decTime_1, decTime_2, decTime_3, decTime_4};

                int[] jerk_acc = Enumerable.Repeat<int>(66, 4).ToArray<int>();
                int[] jerk_dec = Enumerable.Repeat<int>(66, 4).ToArray<int>();

                int[] position = { position_1, position_2, position_3, position_4 };
                
                int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
                
                jobQueue.Enqueue(() =>
                {
                    returnCode = _snetDevice.MoveMultiAxis(axisCnt, axis, moveType, velocity, accTime, decTime, jerk_acc, jerk_dec, position);
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

    }
}
