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

        int _max = 0;
        int _min = 999999999;

        int _sum = 0;
        int _avg = 0;

        public string RepeatJob(int repeatNum, int dwell, Queue<Action>jobQueue, int axis, ref int max, ref int min, ref int avg)
        {
            int total=0;
            int time = 0;
            int cnt = 1;

            List<int> maxList = new List<int>();

            if (repeatNum >= 0)
            {
                for (int i = 0; i <= repeatNum; i++)
                {
                    Queue<Action> tempJobQueue = new Queue<Action>(jobQueue);

                    int.TryParse(DoJob(tempJobQueue, axis), out time);
                    total += time;

                    // maxList.Add(CalcTimeMax(total));
                    _max = 0;
                    _avg = CalcTimeAvg(time,i+1);
                    _min = CalcTimeMin(time);
                    // avg=CalcTimeAvg(total, repeatNum);

                    Thread.Sleep(dwell);
                }
            }
            else if(repeatNum<0)
            { 
                while (enable)
                {
                    if (enable == false) break;

                    Queue<Action> tempJobQueue = new Queue<Action>(jobQueue);

                    int.TryParse(DoJob(tempJobQueue, axis), out time);
                    total += time;
                    cnt++;

                    maxList.Add(CalcTimeMax(total));
                    _min =CalcTimeMin(total);
                    _avg =CalcTimeAvg(total, cnt);

                    Thread.Sleep(dwell);
                }
            }

            min = _min;
            avg = _avg;

            return total.ToString();
        }

        public int CalcTimeMax(int time)
        {
            if (time > _max) _max = time;

            return _max;
        }

        public int CalcTimeMin(int time)
        {
            if (time < _min) _min = time;

            return _min;
        }

        public int CalcTimeAvg(int time, int cnt)
        {
            _sum += time;
            _avg = _sum / cnt;

            return _avg;
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
