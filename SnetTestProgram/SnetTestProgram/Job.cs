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
        
        public void AddJob(Queue<Action>jobQueue, Action action)
        {
            jobQueue.Enqueue(action);

        }

        List<int> _timeList = new List<int>();

        int _avg = 0;

        int _cnt = 0;

        public string RepeatJob(int repeatNum, int dwell, Queue<Action>jobQueue, int axis, ref List<int> timeList, ref List<int> maxList, ref int min, ref int avg, ref int cnt)
        {
            int total=0;
            int time = 0;

            _timeList.Clear();

            if (repeatNum >= 0)
            {
                for (int i = 0; i <= repeatNum; i++)
                {
                    cnt = i;

                    Queue<Action> tempJobQueue = new Queue<Action>(jobQueue);

                    int.TryParse(DoJob(tempJobQueue, axis), out time);
                    total += time;

                    _timeList.Add(time);

                    Thread.Sleep(dwell);
                }

                _avg = (total-_timeList[0]) / repeatNum;
            }
            else if(repeatNum<0)
            { 
                while (enable)
                {
                    cnt = _cnt;

                    if (enable == false) break;

                    Queue<Action> tempJobQueue = new Queue<Action>(jobQueue);

                    int.TryParse(DoJob(tempJobQueue, axis), out time);
                    total += time;
                    _cnt++;

                    _timeList.Add(time);

                    Thread.Sleep(dwell);
                }

                _avg = (total-_timeList[0]) / cnt;
                
            }

            timeList = _timeList.Skip(1).ToList();

            maxList = (from element in timeList
                       orderby element descending select element).ToList();

            if (maxList.Count > 5)
                maxList.Take(5).ToList();

            min = _timeList.Skip(1).Min();
            avg = _avg;

            return (total-_timeList[0]).ToString();
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
