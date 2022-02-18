using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EMotionSnetBase;
using System.Threading;

namespace SnetTestProgram
{ 
    interface IControllerWait
    {
        int WaitMotionDone(int axis);
    }

    public class PollingWait : IControllerWait
    {
        private SnetDevice _snetDevice;

        public PollingWait(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public int WaitMotionDone(int axis)
        {
            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            
            bool motionDone = false;
            bool moving = true;

            while (moving)
            {
                returnCode = _snetDevice.GetMotionDone(axis, ref motionDone);

                if (returnCode == (int)SnetDevice.eSnetApiReturnCode.Success)
                {
                    if (motionDone == true) moving = false;
                }

                Thread.Sleep(1);
            }

            return returnCode;
        }

    }

    public class InterruptWait : IControllerWait
    {
        private SnetDevice _snetDevice;

        public InterruptWait(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public int WaitMotionDone(int axis)
        {
            return axis;
        }
    }
        
}
