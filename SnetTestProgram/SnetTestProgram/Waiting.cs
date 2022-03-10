using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EMotionSnetBase;
using System.Threading;
using System.Security;

namespace SnetTestProgram
{ 
    public interface IControllerWait
    {
        int WaitMotionDone(int axis);
    }

    public class PollingWait : IControllerWait
    {
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint timeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint timeEndPeriod(uint uMilliseconds);

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

                timeBeginPeriod(1);
                Thread.Sleep(1);
                timeEndPeriod(1);
            }

            return returnCode;
        }

    }

    public class InterruptWait : IControllerWait
    {
        private SnetDevice _snetDevice;
        SnetDevice.InterruptEventTableInfo ieti = new SnetDevice.InterruptEventTableInfo();

        public InterruptWait(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public void InitInterruptTable(bool enable)
        {
            if (enable == true)
            {
                ieti.oneshot = 0;
                ieti.axis_index = 0;
                ieti.axis_type = 1;
                ieti.input_channel = -1;
                ieti.input_type = -1;
                ieti.input_port = -1;
                ieti.input_point = -1;
                ieti.input_active = 0;

                _snetDevice.SetInterruptEventTable(0, true, ieti);
                // Event 방식
                _snetDevice.EnableInterruptEvent(true);

            }
            else
            {
                ieti.oneshot = 0;
                ieti.axis_index = 0;
                ieti.axis_type = 0;
                ieti.input_channel = -1;
                ieti.input_type = -1;
                ieti.input_port = -1;
                ieti.input_point = -1;
                ieti.input_active = 0;

                _snetDevice.EnableInterruptEvent(false);

            }

        }

        public int WaitMotionDone(int axis)
        {
            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            returnCode = _snetDevice.WaitInterruptEvent(0, 1000);

            return returnCode;
        }
    }
        
}
