using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnetTestProgram
{
    public class Job
    {
        public class JobList
        {
            public int axis;
            public int velocity;
            public int accTime;
            public int decTime;
            public int dwell;
            public int startPos;
            public int endPos;

            // 매개변수를 통해 Job List를 생성하기 위한 설정
            public JobList(int axis, int velocity, int accTime, int decTime, int dwell, int startPos, int endPos)
            {
                this.axis = axis;
                this.velocity = velocity;
                this.accTime = accTime;
                this.decTime = decTime;
                this.dwell = dwell;
                this.startPos = startPos;
                this.endPos = endPos;
            }
        }

        #region Methods

        public JobList CreateJobList(int axis, int velocity, int accTime, int decTime, int dwell, int startPos, int endPos)
        {
            JobList jobList = new JobList(axis, velocity, accTime, decTime, dwell, startPos, endPos);

            return jobList;
        }

        public JobList[] UpdateJobListArray(JobList jobList, int idx)
        {
            JobList[] jobListArray = new JobList[32];
            jobListArray[idx] = jobList;

            return jobListArray;
        }

        public void PrintJobListArray(JobList[] jobListArray, int idx)
        {
            Console.WriteLine("    axis: " + jobListArray[idx].axis);
            Console.WriteLine("velocity: " + jobListArray[idx].velocity);
            Console.WriteLine(" accTime: " + jobListArray[idx].accTime);
            Console.WriteLine(" decTime: " + jobListArray[idx].decTime);
            Console.WriteLine("   dwell: " + jobListArray[idx].dwell);
            Console.WriteLine("startPos: " + jobListArray[idx].startPos);
            Console.WriteLine("  endPos: " + jobListArray[idx].endPos);
        }

        #endregion
    }

}
