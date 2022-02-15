﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using EMotionSnetBase;

namespace SnetTestProgram
{
    public class PollingThread
    {
        private SnetDevice _snetDevice;
        private Job _job;

        [DllImport("winmm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint timeGetTime();

        public PollingThread(SnetDevice snetDevice, Job job)
        {
            _snetDevice = snetDevice;
            _job = job;
        }

        public uint PollingMoveTime(int velocity, int accTime, int decTime, int startPos, int endPos, int dwell)
        {
            int axis = 0;
            int[] position = new int[] { startPos, endPos }; 

            SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Scurve;

            bool moving = true;

            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            uint startTime = timeGetTime();
           
            bool motionDone = false;

            for (int i = 0; i < 2; i++)
            {
                returnCode = _snetDevice.MoveSingleEx(axis, moveType, velocity, accTime, decTime, 66, position[i]);

                while (moving)
                {
                    returnCode = _snetDevice.GetMotionDone(axis, ref motionDone);

                    if (returnCode == (int)SnetDevice.eSnetApiReturnCode.Success)
                    {
                        if (motionDone == true)
                        {
                            moving = false;
                        }
                    }
                }
                
                Thread.Sleep(dwell);

            }

            uint endTime = timeGetTime();

            return endTime - startTime;
        }

    }
}
