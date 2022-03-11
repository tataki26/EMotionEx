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
        private IControllerWait _iControllerWait;

        public Job(IControllerWait controllerWait)
        {
            _iControllerWait = controllerWait;
        }

        #region Methods

        public Queue<Action> CreateJobQueue()
        {
            Queue<Action> jobQueue = new Queue<Action>();
            return jobQueue;
        }

        public string DoJob(Queue<Action> jobQueue, int axis)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            while (jobQueue.Count > 0)
            {
                int motionDone = _iControllerWait.WaitMotionDone(axis);

                if (motionDone == 0)
                {
                    Action action = jobQueue.Dequeue();
                    action.Invoke();
                }
            }

            _iControllerWait.WaitMotionDone(axis);

            stopWatch.Stop();

            return (stopWatch.ElapsedMilliseconds).ToString();
        }

        public void SetWait(IControllerWait cw)
        {
            _iControllerWait = cw;
        }
    
        #endregion
    }

}
