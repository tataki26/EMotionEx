using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EMotionSnetBase;
using System.Threading;
using System.Security;
using System.Diagnostics;

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
                ieti.axis_index = 2;
                ieti.axis_type = (int)SnetDevice.InterruptEventAxisType.MotionDone;
                ieti.input_channel = -1;
                ieti.input_type = -1;
                ieti.input_port = -1;
                ieti.input_point = -1;
                ieti.input_active = 0;

                _snetDevice.SetInterruptEventTable(0, true, ieti);
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
            bool motionDone = false;
            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            returnCode = _snetDevice.GetMotionDone(axis, ref motionDone);

            if ((!motionDone) && returnCode == (int)SnetDevice.eSnetApiReturnCode.Success)
            {
                returnCode = _snetDevice.WaitInterruptEvent(0, 0);

                if(returnCode == (int)SnetDevice.eSnetApiReturnCode.InterruptEventFailedWaiting)
                {
                    Debug.WriteLine("TimeOut!!!");
                }
            }

            return returnCode;
        }
    }

    public class InterruptFunction : IControllerWait
    {
        private SnetDevice _snetDevice;
        SnetDevice.InterruptEventTableInfo ieti = new SnetDevice.InterruptEventTableInfo();
        SnetDevice.InterruptEventHandler ieh;
        EventWaitHandle eventWaitHanlde = new EventWaitHandle(false, EventResetMode.ManualReset);

        bool motionDone = false;

        public InterruptFunction(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public void OnRoutineIntteruptEvent(int tableIndex)
        {
            eventWaitHanlde.Set();
            Debug.WriteLine("Table Index" + tableIndex + " Interrupt!!!");
        }

        public void InitInterruptTable()
        {
            ieh = OnRoutineIntteruptEvent;

            ieti.oneshot = 0;
            ieti.axis_index = 2;
            ieti.axis_type = (int)SnetDevice.InterruptEventAxisType.MotionDone;
            ieti.input_channel = -1;
            ieti.input_type = -1;
            ieti.input_port = -1;
            ieti.input_point = -1;
            ieti.input_active = 0;

            _snetDevice.SetInterruptEventTable(0, true, ieti);
            _snetDevice.SetInterruptEventFunction(ieh);
            _snetDevice.EnableInterruptEvent(true);
            
        }

        public int WaitMotionDone(int axis)
        {
            int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
            returnCode = _snetDevice.GetMotionDone(axis, ref motionDone);

            if ((!motionDone) && returnCode == (int)SnetDevice.eSnetApiReturnCode.Success)
            {
                eventWaitHanlde.WaitOne();
                eventWaitHanlde.Reset();
            }

            return returnCode;
        }

    }

}
