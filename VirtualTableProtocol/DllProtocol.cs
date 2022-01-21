using System;
using System.Runtime.InteropServices;
using VtpLibrary;

namespace McsProgram
{
    public class DllProtocol : IMcs
    {
        [DllImport("EMotionMcsDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int eMcsConnectByUDP(int ipAddr1, int ipAddr2, int ipAddr3, int ipAddr4, int portNo);

        [DllImport("EMotionMcsDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int eMcsGetIPoint(int address, out int data);

        [DllImport("EMotionMcsDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int eMcsGetLVariable(int address, out int data);

        [DllImport("EMotionMcsDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int eMcsSetLVariable(int address, int data);

        [DllImport("EMotionMcsDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int eMcsSetQPoint(int address, int data);


        public void Connect_Udp_Client(string host, int port)
        {
            string[] hosts = host.Split('.');
            eMcsConnectByUDP(Convert.ToInt32(hosts[0]), Convert.ToInt32(hosts[1]), Convert.ToInt32(hosts[2]), Convert.ToInt32(hosts[3]), port);
        }

        public void Set_I_Var(int addr, ref object result)
        {
            int data;
            int ret;

            ret = eMcsGetIPoint(addr, out data);

            if (ret != 0)
            {
                if (ret == 20) result = "NACK";
                else result = "Invaild Argument";
            }
            else result = data;
            
        }

        public void Set_LR_Var(int addr, ref object result)
        {
            int data;
            int ret;

            ret = eMcsGetLVariable(addr, out data);

            if (ret == 0)
            {
                result = "NACK";
            }
            else result = data;

        }

        public void Set_LW_Var(int addr, int data)
        {
            eMcsSetLVariable(addr, data);
        }

        public void Set_Q_Var(int addr, int data)
        {
            eMcsSetQPoint(addr, data);
        }
    }
}
