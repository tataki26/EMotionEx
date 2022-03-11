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

        public int axis;
        bool enable = true;

        #region Methods

        public void SetAxis(int ax)
        {
            axis = ax;
        }
        
        public void AddJob(Queue<Action>jobQueue, Action action)
        {
            jobQueue.Enqueue(action);
            
            // return jobQueue;
        }

        public string RepeatJob(int repeatNum, int dwell, Queue<Action>jobQueue, int axis)
        {
            int total=0;
            int time = 0;

            if (repeatNum >= 0)
            {
                for (int i = 0; i <= repeatNum; i++)
                {
                    //List<Queue<Action>> queueList = new List<Queue<Action>>();
                    Queue<Action> tempJobQueue = new Queue<Action>(jobQueue);

                    int.TryParse(DoJob(tempJobQueue, axis), out time);
                    total += time;
                    Thread.Sleep(dwell);
                }
            }
            else if(repeatNum==-1)
            { 
                while (enable)
                {
                    if (enable == false) break;

                    Queue<Action> tempJobQueue = new Queue<Action>(jobQueue);

                    int.TryParse(DoJob(tempJobQueue, axis), out time);
                    total += time;
                    Thread.Sleep(dwell);
                }
            }

            return total.ToString();
        }

        public void StopJob()
        {
            enable = false;
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
