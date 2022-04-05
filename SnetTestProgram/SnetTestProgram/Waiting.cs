using System.Runtime.InteropServices;
using EMotionSnetBase;
using System.Threading;
using System.Diagnostics;

namespace SnetTestProgram
{ 
    public interface IWait
    {
        int WaitMotionDone(int axis);
    }

    public class PollingWait : IWait
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

    public class InterruptEventWait : IWait
    {
        private SnetDevice _snetDevice;
        SnetDevice.InterruptEventTableInfo interruptEventTableInfo = new SnetDevice.InterruptEventTableInfo();

        public InterruptEventWait(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;

        }

        public void InitInterruptEventTable()
        {
            bool enable = true;

            _snetDevice.ClearInterruptEventTable();

            interruptEventTableInfo.oneshot = 0;
            interruptEventTableInfo.axis_index = 2;
            interruptEventTableInfo.axis_type = (int)SnetDevice.InterruptEventAxisType.MotionDone;
            interruptEventTableInfo.input_channel = -1;
            interruptEventTableInfo.input_type = -1;
            interruptEventTableInfo.input_port = -1;
            interruptEventTableInfo.input_point = -1;
            interruptEventTableInfo.input_active = 0;
           
            _snetDevice.SetInterruptEventTable(0, enable, interruptEventTableInfo);
            _snetDevice.EnableInterruptEvent(enable);

        }

        public int WaitMotionDone(int axis)
        {
            bool motionDone = false;
            
            int returnCode = _snetDevice.GetMotionDone(axis, ref motionDone);

            if (returnCode == (int)SnetDevice.eSnetApiReturnCode.Success)
            {
                if (!motionDone) returnCode = _snetDevice.WaitInterruptEvent(0, 0);
            }
            else if (returnCode == (int)SnetDevice.eSnetApiReturnCode.InterruptEventFailedWaiting)
            {
                Debug.WriteLine("TimeOut!!!");
            }

            return returnCode;
        }
    }

    public class InterruptFunctionWait : IWait
    {
        private SnetDevice _snetDevice;
        SnetDevice.InterruptEventTableInfo interruptEventTableInfo = new SnetDevice.InterruptEventTableInfo();
        SnetDevice.InterruptEventHandler interruptEventHandler;
        EventWaitHandle eventWaitHanlde = new EventWaitHandle(false, EventResetMode.ManualReset);

        public InterruptFunctionWait(SnetDevice snetDevice)
        {
            _snetDevice = snetDevice;
        }

        public void OnIntteruptEventFunction(int tableIdx)
        {
            eventWaitHanlde.Set();
        }

        public void InitInterruptEventTable()
        {
            bool enable=true;

            interruptEventHandler = OnIntteruptEventFunction;

            _snetDevice.ClearInterruptEventTable();

            interruptEventTableInfo.oneshot = 0;
            interruptEventTableInfo.axis_index = 2;
            interruptEventTableInfo.axis_type = (int)SnetDevice.InterruptEventAxisType.MotionDone;
            interruptEventTableInfo.input_channel = -1;
            interruptEventTableInfo.input_type = -1;
            interruptEventTableInfo.input_port = -1;
            interruptEventTableInfo.input_point = -1;
            interruptEventTableInfo.input_active = 0;
            
            _snetDevice.SetInterruptEventTable(0, enable, interruptEventTableInfo);
            _snetDevice.SetInterruptEventFunction(interruptEventHandler);
            _snetDevice.EnableInterruptEvent(enable);
            
        }

        public int WaitMotionDone(int axis)
        {
            bool motionDone = false;
            
            int returnCode = _snetDevice.GetMotionDone(axis, ref motionDone);

            if (returnCode == (int)SnetDevice.eSnetApiReturnCode.Success)
            {
                if (!motionDone)
                {
                    eventWaitHanlde.WaitOne();
                    eventWaitHanlde.Reset();
                }
            }

            return returnCode;
        }

    }

}
