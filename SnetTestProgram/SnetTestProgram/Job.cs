using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMotionSnetBase;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SnetTestProgram
{
    public class Job
    {
        [DllImport("winmm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint timeGetTime();

        private SnetDevice _snetDevice;
        private PollingWait _pollingWait;

        public Job(SnetDevice snetDevice, PollingWait pollingWait)
        {
            _snetDevice = snetDevice;
            _pollingWait = pollingWait;
        }

        public class JobItem
        {
            public int axis;
            public int velocity;
            public int accTime;
            public int decTime;
            public int dwell;
            public int startPos;
            public int endPos;
            public int repeatNum;

            // 매개변수를 통해 Job Item을 생성하기 위한 설정
            public JobItem(int axis, int velocity, int accTime, int decTime, int dwell, int startPos, int endPos, int repeatNum)
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

        public Queue<Action> CreateJobQueue()
        {
            Queue<Action> jobQueue = new Queue<Action>();
            return jobQueue;
        }

        public uint DoJobPolling(Queue<Action> jobQueue)
        {
            uint startTime = timeGetTime();

            while (jobQueue.Count > 0)
            {
                int motionDone = _pollingWait.WaitMotionDone(0);

                if (motionDone == 0)
                {
                    Action action = jobQueue.Dequeue();
                    action.Invoke();
                }
            }

            _pollingWait.WaitMotionDone(0);

            uint endTime = timeGetTime();
            
            return endTime-startTime;
        }
    
        #endregion
    }

}
