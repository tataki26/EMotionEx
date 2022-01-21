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

        public void Set_I_Var(int addr, ref bool flag, ref int num, ref string str)
        {
            int ret;

            ret = eMcsGetIPoint(addr, out num);

            if (ret != 0)
            {
                flag = false;
                
                if (ret == 20) str = "NACK";
                else str = "Invaild Argument";
            }
            
        }

        public void Set_LR_Var(int addr, ref bool flag, ref int num, ref string str)
        {
            int ret;

            ret = eMcsGetLVariable(addr, out num);

            if (ret == 0)
            {
                flag = false;
                str = "NACK";
            }

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
