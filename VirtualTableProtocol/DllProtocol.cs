using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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

        public int Set_I_Var(int addr)
        {
            int data = 0;
            int ret = 0;

            ret = eMcsGetIPoint(addr, out data);

            return data;
        }

        public int Set_LR_Var(int addr)
        {
            int data = 0;
            int ret = 0;

            ret = eMcsGetLVariable(addr, out data);

            return data;
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
