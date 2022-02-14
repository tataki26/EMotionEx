using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EMotionSnetBase;

namespace SnetTestProgram
{
    public class PollingThread
    {
        private SnetDevice _snetDevice;

        [DllImport("winmm.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint timeGetTime();

        public PollingThread(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public uint PollingMoveTime(int velocity, int accTime, int decTime, int position)
        {
            int axis = 0;

            SnetDevice.eSnetMoveType moveType = SnetDevice.eSnetMoveType.Scurve;

            bool moving = true;

            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            uint startTime = timeGetTime();
            returnCode = _snetDevice.MoveSingleEx(axis, moveType, velocity, accTime, decTime, 66, position);

            bool motionDone = false;

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

    }
}
