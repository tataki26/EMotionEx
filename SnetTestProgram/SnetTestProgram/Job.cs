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

        private PollingWait _pollingWait;

        public Job(PollingWait pollingWait)
        {
            _pollingWait = pollingWait;
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
