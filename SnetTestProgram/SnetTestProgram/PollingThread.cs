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
        private SnetDevice _snetDevice = new SnetDevice();

        [DllImport("winmm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint timeGetTime();

        public PollingThread(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public uint PollingMoveTime(int axis, int velocity, int accTime, int decTime, int position)
        {
            SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Scurve;

            bool moving = true;

            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            uint startTime = timeGetTime();

            bool motionDone = false;

            returnCode = _snetDevice.MoveSingleEx(axis, moveType, velocity, accTime, decTime, 66, position);

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

            uint endTime = timeGetTime();

            return endTime - startTime;
        }

        public uint PollingMoveTime(int axis, int velocity, int accTime, int decTime, int startPos, int endPos, int dwell)
        {
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
