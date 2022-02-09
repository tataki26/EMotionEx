using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EMotionSnetBase;

namespace EMotionSnetTest
{
    class SnetCommunicator
	{
		int NetID = 1;

		#region Administrator API Import

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetSetMonitorMotionStart(int net, int MAxis, int MMethod, int MTime);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetMonitorMotionRunningStatus(int net, out int spin_cnt, out int axis_id, out int method, out int m_time);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetMonitorMotionData(int net, int begin_index, int cnt, out int data);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetSetRepeatStart(int net, int axis);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetSetRepeatInfo(int net, int axis, int vel, int acc, int dec, int jerk, int pos0, int pos1, int dwell, int conti);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetOSVerify(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetOSVerify2(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetOSUpdate(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetOSUpdate2(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetFPGAUpdate(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetFPGAUpdate2(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetFPGAVerify(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetFPGAVerify2(int net, char[] fileName, int deviceId);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetOsDownloadProgressStep(int net, out int get, out int isOk);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetOsVerifyProgressStep(int net, out int get, out int isOk);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetFpgaDownloadProgressStep(int net, out int get, out int isOk);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetFpgaVerifyProgressStep(int net, out int get, out int isOk);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetSetDownloadProgressStepClear(int net);

		//[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		//protected static extern int eSnetReadSystemMemory(int net, int addr, short len, out int data);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetWriteSystemMemory(int net, int addr, int data);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetWriteSystemFlashMemory(int net, int addr, int data);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetFloatFromHex(int net, int input, out float output);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetGetFloatFromInt(int net, int input, out float output);

		[DllImport("EMotionSnetDeviceEx.dll", CallingConvention = CallingConvention.Cdecl)]
		protected static extern int eSnetSigmoide(int net, double dTime, out double dValue);

		#endregion

		#region Administrator API Operation

		/// <summary>
		/// Start data logging of motion profile data in device
		/// </summary>
		/// <param name="MAxis">
		/// The axis number to start data log about motion
		/// </param>
		/// <param name="MMethod">
		/// Method number
		/// </param>
		/// <param name="MTime">
		/// logging time
		/// </param>
		/// <returns>
		/// see enum "eSnetApiReturnCode"
		/// </returns>
		public int SetMonitorMotionStart(int MAxis, int MMethod, int MTime)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetSetMonitorMotionStart(NetID, MAxis, MMethod, MTime);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetSetMonitorMotionStart'. error : " + returnCode,
						"SnetCommunicator.SetMonitorMotionStart");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.SetMonitorMotionStart");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		/// <summary>
		/// Get status while logging of motion profile in device
		/// </summary>
		/// <param name="spin_cnt">
		/// </param>
		/// <param name="axis_id">
		/// The axis number while logging of motion profile data
		/// </param>
		/// <param name="method">
		/// Method number while logging of motion profile data
		/// </param>
		/// <param name="m_time">
		/// logging time while logging of motion profile data
		/// </param>
		/// <returns>
		/// see enum "eSnetApiReturnCode"
		/// </returns>
		public int GetMonitorMotionRunningStatus(ref int spin_cnt, ref int axis_id, ref int method, ref int m_time)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;
			int spinTemp = 0;
			int axisTemp = 0;
			int methodTemp = 0;
			int timeTemp = 0;

			try
			{
				returnCode = eSnetGetMonitorMotionRunningStatus(NetID, out spinTemp, out axisTemp, out methodTemp, out timeTemp);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetMonitorMotionRunningStatus'. error : " + returnCode,
						"SnetCommunicator.GetMonitorMotionRunningStatus");
				}
				else
				{
					spin_cnt = spinTemp;
					axis_id = axisTemp;
					method = methodTemp;
					m_time = timeTemp;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetMonitorMotionRunningStatus");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetMonitorMotionData(int begin_index, int count, ref int data)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetMonitorMotionData(NetID, begin_index, count, out data);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetMonitorMotionData'. error : " + returnCode,
						"SnetCommunicator.SetRepeatInfo");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetMonitorMotionData");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int SetRepeatStart(int axis)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetSetRepeatStart(NetID, axis);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetSetRepeatStart'. error : " + returnCode,
						"SnetCommunicator.SetRepeatStart");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.SetRepeatStart");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int SetRepeatInfo(int axis, int vel, int acc, int dec, int jerk, int pos0, int pos1, int dwell, int conti)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetSetRepeatInfo(NetID, axis, vel, acc, dec, jerk, pos0, pos1, dwell, conti);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetSetRepeatInfo'. error : " + returnCode,
						"SnetCommunicator.SetRepeatInfo");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.SetRepeatInfo");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int OsVerify(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetOSVerify(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetOsVerify'. error : " + returnCode,
						"SnetCommunicator.OsVerify");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.OsVerify");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int OsVerify2(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetOSVerify2(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetOsVerify2'. error : " + returnCode,
						"SnetCommunicator.OsVerify2");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.OsVerify2");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int OsUpdate(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetOSUpdate(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetOSUpdate'. error : " + returnCode,
						"SnetCommunicator.OsUpdate");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.OsUpdate");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int OsUpdate2(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetOSUpdate2(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetOSUpdate2'. error : " + returnCode,
						"SnetCommunicator.OsUpdate2");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.OsUpdate2");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int FpgaUpdate(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetFPGAUpdate(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetFPGAUpdate'. error : " + returnCode,
						"SnetCommunicator.FpgaUpdate");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.FpgaUpdate");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int FpgaUpdate2(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetFPGAUpdate2(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetFPGAUpdate2'. error : " + returnCode,
						"SnetCommunicator.FpgaUpdate2");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.FpgaUpdate2");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int FpgaVerify(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetFPGAVerify(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetFPGAVerify'. error : " + returnCode,
						"SnetCommunicator.FpgaVerify");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.FpgaVerify");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int FpgaVerify2(char[] fileName, int deviceId)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetFPGAVerify2(NetID, fileName, deviceId);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetFPGAVerify2'. error : " + returnCode,
						"SnetCommunicator.FpgaVerify2");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.FpgaVerify2");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetOsDownloadProgressStep(ref int get, ref int isOk)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetOsDownloadProgressStep(NetID, out int getValue, out int getOk);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetOsDownloadProgressStep'. error : " + returnCode,
						"SnetCommunicator.GetOsDownloadProgressStep");
				}
				else
				{
					get = getValue;
					isOk = getOk;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetOsDownloadProgressStep");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetOsVerifyProgressStep(ref int get, ref int isOk)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetOsVerifyProgressStep(NetID, out int getValue, out int getOk);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetOsVerifyProgressStep'. error : " + returnCode,
						"SnetCommunicator.GetOsVerifyDownloadProgressStep");
				}
				else
				{
					get = getValue;
					isOk = getOk;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetOsVerifyDownloadProgressStep");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetFpgaDownloadProgressStep(ref int get, ref int isOk)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetFpgaDownloadProgressStep(NetID, out int getValue, out int getOk);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetFpgaDownloadProgressStep'. error : " + returnCode,
						"SnetCommunicator.GetFpgaDownloadProgressStep");
				}
				else
				{
					get = getValue;
					isOk = getOk;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetFpgaDownloadProgressStep");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetFpgaVerifyProgressStep(ref int get, ref int isOk)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetFpgaVerifyProgressStep(NetID, out int getValue, out int getOk);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetFpgaVerifyProgressStep'. error : " + returnCode,
						"SnetCommunicator.GetFpgaVerifyProgressStep");
				}
				else
				{
					get = getValue;
					isOk = getOk;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetFpgaVerifyProgressStep");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int SetDownloadProgressStepClear()
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetSetDownloadProgressStepClear(NetID);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetSetDownloadProgressStepClear'. error : " + returnCode,
						"SnetCommunicator.SetDownloadProgressStepClear");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.SetDownloadProgressStepClear");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		/*public int ReadSystemMemory(int addr, int len, ref int[] data)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetReadSystemMemory(NetID, addr, (short)len, out data[0]);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetReadSystemMemory'. error : " + returnCode,
						"SnetCommunicator.ReadSystemMemory");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.ReadSystemMemory");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}*/

		public int WriteSystemMemory(int addr, int data)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetWriteSystemMemory(NetID, addr, data);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetWriteSystemMemory'. error : " + returnCode,
						"SnetCommunicator.WriteSystemMemory");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.WriteSystemMemory");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int WriteSystemFlashMemory(int addr, int len, ref int data)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetWriteSystemFlashMemory(NetID, addr, data);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetWriteSystemFlashMemory'. error : " + returnCode,
						"SnetCommunicator.WriteSystemFlashMemory");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.WriteSystemFlashMemory");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetFloatFromHex(int input, ref float output)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetFloatFromHex(NetID, input, out float getValue);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetFloatFromHex'. error : " + returnCode,
						"SnetCommunicator.GetFloatFromHex");
				}
				else
				{
					output = getValue;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetFloatFromHex");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int GetFloatFromInt(int input, ref float output)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetGetFloatFromInt(NetID, input, out float getValue);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetGetFloatFromInt'. error : " + returnCode,
						"SnetCommunicator.GetFloatFromInt");
				}
				else
				{
					output = getValue;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.GetFloatFromInt");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		public int Sigmoide(double dTime, ref double dValue)
		{
			int returnCode = (int)SnetDevice.eSnetApiReturnCode.Success;

			try
			{
				returnCode = eSnetSigmoide(NetID, dTime, out double dGetValue);
				if (returnCode != (int)SnetDevice.eSnetApiReturnCode.Success)
				{
					Debug.WriteLine("Failed to call 'eSnetSigmoide'. error : " + returnCode,
						"SnetCommunicator.Sigmoide");
				}
				else
				{
					dValue = dGetValue;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message, "SnetCommunicator.Sigmoide");
				returnCode = (int)SnetDevice.eSnetApiReturnCode.Exception;
			}

			return returnCode;
		}

		#endregion		
	}
}
