using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMotionSnetBase;
using System.Threading;

namespace SnetTestProgram
{
    public class Job
    {
        private SnetDevice _snetDevice;
        private PollingThread _pollingThread;

        public Job(SnetDevice snetDevice, PollingThread pollingThread)
        {
            _snetDevice = snetDevice;
            _pollingThread = new PollingThread(_snetDevice);
        }

        public class JobList
        {
            public int axis;
            public int velocity;
            public int accTime;
            public int decTime;
            public int dwell;
            public int startPos;
            public int endPos;
            public int repeatNum;

            // 매개변수를 통해 Job List를 생성하기 위한 설정
            public JobList(int axis, int velocity, int accTime, int decTime, int dwell, int startPos, int endPos, int repeatNum)
            {
                this.axis = axis;
                this.velocity = velocity;
                this.accTime = accTime;
                this.decTime = decTime;
                this.dwell = dwell;
                this.startPos = startPos;
                this.endPos = endPos;
                this.repeatNum = repeatNum;
            }
        }

        #region Methods

        public JobList CreateJobList(int axis, int velocity, int accTime, int decTime, int dwell, int startPos, int endPos, int repeatNum)
        {
            JobList jobList = new JobList(axis, velocity, accTime, decTime, dwell, startPos, endPos, repeatNum);

            return jobList;
        }

        public void UpdateJobListArray(JobList jobList, int idx, ref JobList[] jobListArray)
        {
            jobListArray[idx] = jobList;
        }

        public void PrintJobListArray(JobList[] jobListArray, int idx)
        {
            Console.WriteLine("     axis: " + jobListArray[idx].axis);
            Console.WriteLine(" velocity: " + jobListArray[idx].velocity);
            Console.WriteLine("  accTime: " + jobListArray[idx].accTime);
            Console.WriteLine("  decTime: " + jobListArray[idx].decTime);
            Console.WriteLine("    dwell: " + jobListArray[idx].dwell);
            Console.WriteLine(" startPos: " + jobListArray[idx].startPos);
            Console.WriteLine("   endPos: " + jobListArray[idx].endPos);
            Console.WriteLine("repeatNum: " + jobListArray[idx].repeatNum);
        }

        public void JobMove(JobList[] jobListArray, ref uint time)
        {
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        time= _pollingThread.PollingMoveTime(jobListArray[0].axis, jobListArray[0].velocity, jobListArray[0].accTime, jobListArray[0].decTime, jobListArray[0].startPos);
                         break;
                    case 1:
                        time = _pollingThread.PollingMoveTime(jobListArray[1].axis, jobListArray[1].velocity, jobListArray[1].accTime, jobListArray[1].decTime, jobListArray[1].endPos);
                        break;
                    // 2번축(amp fault) 오류로 인한 3번 축 구동
                    case 2:
                        time = _pollingThread.PollingMoveTime(3, jobListArray[2].velocity, jobListArray[2].accTime, jobListArray[2].decTime, jobListArray[2].startPos, jobListArray[2].endPos, jobListArray[2].repeatNum, jobListArray[2].dwell);
                        break;
                }
            }
        }

        #endregion
    }

}
