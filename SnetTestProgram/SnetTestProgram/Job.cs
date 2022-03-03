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

        public string DoJobPolling(Queue<Action> jobQueue, int axis)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            while (jobQueue.Count > 0)
            {
                int motionDone = _pollingWait.WaitMotionDone(axis);

                if (motionDone == 0)
                {
                    Action action = jobQueue.Dequeue();
                    action.Invoke();
                }
            }

            _pollingWait.WaitMotionDone(axis);

            stopWatch.Stop();

            return (stopWatch.ElapsedMilliseconds).ToString();
        }
    
        #endregion
    }

}
