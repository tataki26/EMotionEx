using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace EMotionUniBase
{
    /// <summary>
    /// Uni device API wrapper class
    /// </summary>
    public class UniDevice
    {
        #region API Define

        /// <summary>
        /// Return code for EMOTION UNI API
        /// </summary>
        public enum eUniApiReturnCode
        {
            Success                                 = 0,

            ConnectionBase                          = 0,
            NotConnected                            = ConnectionBase + 1,
            AlreadyConnected                        = ConnectionBase + 2,
            Disconnected                            = ConnectionBase + 3,
            Disconnecting                           = ConnectionBase + 4,
            FailedCommunication                     = ConnectionBase + 5,
            InvalidMessage                          = ConnectionBase + 6,
            InvalidSequenceNumber                   = ConnectionBase + 7,
            InvalidCommunicationFormat              = ConnectionBase + 8,
            InvalidNetworkId                        = ConnectionBase + 9,

            OperationBase                           = 127,
            InvalidAxisNumber                       = OperationBase + 1,
            OperationTimeout                        = OperationBase + 2,
            NotOperation                            = OperationBase + 3,
            FailedOperation                         = OperationBase + 4,
            InvalidAddress                          = OperationBase + 5,
            InvalidDataCount                        = OperationBase + 6,
            InvalidDataValue                        = OperationBase + 7,
            AlreadyOperation                        = OperationBase + 8,
            AlreadySet                              = OperationBase + 9,
            InvalidCommand                          = OperationBase + 17,
            SameSequenceNumber                      = OperationBase + 18,
            NotConfiguration                        = OperationBase + 33,
            NotDriveReady                           = OperationBase + 34,
            NotServoOn                              = OperationBase + 35,
            NotSet                                  = OperationBase + 36,
            FailedErase                             = OperationBase + 49,
            FailedWrite                             = OperationBase + 50,
            FailedRead                              = OperationBase + 51,

            MotionBase                              = 200,
            InvalidTargetPosition                   = MotionBase,
            RadiusTooSmall                          = MotionBase + 1,

            HomingBase                              = 300,
            HomingFailed                            = HomingBase + 1,
            HomingInvalidAxisNumber                 = HomingBase + 2,
            HomingInvalidStepNumber                 = HomingBase + 3,
            HomingAbort                             = HomingBase + 4,
            HomingDataEmpty                         = HomingBase + 5,
            HomingDataFull                          = HomingBase + 6,
            HomingStepFailed                        = HomingBase + 10,
            HomingOffsetFailed                      = HomingBase + 11,
            HomingDetectLimitNegative               = HomingBase + 12,
            HomingDetectLimitPositive               = HomingBase + 13,
            HomingSleep                             = HomingBase + 20,
            HomingMoving                            = HomingBase + 21,
            HomingMovedDelay                        = HomingBase + 22,
            HomingMoveHomeNegative                  = HomingBase + 50,
            HomingMoveHomePositive                  = HomingBase + 51,

            ApiBase                                 = 1000,
            InvalidArgument                         = ApiBase + 0,
            InvalidDirectory                        = ApiBase + 1,
            FailedLogic                             = ApiBase + 2,
            NoData                                  = ApiBase + 3,
            DataOverflow                            = ApiBase + 4,
            ArgumentNullPointer                     = ApiBase + 5,
            Environment                             = ApiBase + 6,
            NotSupported                            = ApiBase + 7,

            System                                  = 10000,
        }

        /// <summary>
        /// Error code for EMOTION UNI Device
        /// </summary>
        /// <remarks>
        /// The error code is read using 'GetErrorCode'
        /// </remarks>
        public enum eUniDeviceErrorCode
        {
            Success             = 0,
            DriveOverCurrent    = 1,
            MotorOverCurrent    = 2,
            DriveOverTemper     = 3,
            DriveUnderTemper    = 4,
            CpuOverTemper       = 5,
            CpuUnderTemper      = 6,
            DcLinkOverVolt      = 7,
            DcLinkUnderVolt     = 8,
            DriveOverLoad       = 9,
            MotorOverRpm        = 10,
            InvalidMotor        = 11,
            CommandOverRpm      = 12,
            LimitPositive       = 40,
            LimitNegative       = 41,
            LimitSwPositive     = 42,
            LimitSwNegative     = 43,
            MotionNotFollowing  = 44,
            InpositionTimeout   = 45,
        }

        /// <summary>
        /// Data type for EMOTION UNI API
        /// </summary>
        public enum eUniDataType
        {
            Int32   = 1,
            Float32 = 2,
        }

        /// <summary>
        /// Move Type for EMOTION UNI API
        /// </summary>
        enum eUniMoveType
        {
            SCurve			= 1000,		// Position Profile Type
	        Trapezoidal		= 1001,		// Position Profile Type
	        Velocity		= 1005,		// Velocity Move Type
	        ScurveIo		= 1100,		// Position Profile Type(Check input option)
	        TrapezoidalIo	= 1101,		// Position Profile Type(Check input option)
	        VelocityIo		= 1105,		// Velocity Move Type(Check input option)
        }

        /// <summary>
        /// Motion(Moving) mode for EMOTION UNI API
        /// </summary>
        public enum eUniMotionMode
        {
            Absolute = 0,
            Incremental = 1,
        }

        /// <summary>
        /// Input number of EMOTION UNI Device
        /// </summary>
        enum eUniAxisInputNumber
        {
            Input0 = 0,		// The input default setting is negative(-) limit switch
	        Input1 = 1,		// The input default setting is home switch
	        Input2 = 2,		// The input default setting is positive(+) limit switch
	        Input3 = 3,
        }

        /// <summary>
        /// Output number of EMOTION UNI Device
        /// </summary>
        enum eUniAxisOutputNumber
        { 
            Output0 = 0,
	        Output1 = 1,
        }

        /// <summary>
        /// Homing input(sensor) type for EMOTION UNI API
        /// </summary>
        enum eUniAxisHomingSensor
        {
            CheckLimitNegative	= 0,
	        CheckLimitPositive	= 1,
	        CheckHome			= 2,
        }

        /// <summary>
        /// Axis monitoring source type for EMOTION UNI API
        /// </summary>
        public enum eUniMonitoringSource
        {
            Position = 0,
            Velocity = 2,
            Accel = 3,
        }

        /*** Axis monitoring max data count of UNI Device ***/
        public readonly int UNI_MONITORING_DATA_MAX_COUNT = 1500;

        /*** Ethernet information of UNI Device ***/
        public readonly int UNI_IPV4_DEFAULT_ADDRESS1 = 192;
        public readonly int UNI_IPV4_DEFAULT_ADDRESS2 = 168;
        public readonly int UNI_IPV4_DEFAULT_ADDRESS3 = 240;
        public readonly int UNI_IPV4_DEFAULT_UDP_PORT = 10025;

        #endregion

        #region API Import

        /*** Connection ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniConnect(int netId, int port);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniConnectEx(int ip1, int ip2, int ip3, int netId, int port);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniDisconnect(int netId);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniReconnect(int netId);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniIsConnected(int netId, [MarshalAs(UnmanagedType.I1)] out bool connected);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniCheckConnection(int netId);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetCommunicationConfig(int netId, int time, int retryCount);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetCommunicationConfig(int netId, out int time, out int retryCount);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetSystemErrorCode(int netId, out int error);


        /*** Log ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetLogInfo(int netId, [MarshalAs(UnmanagedType.I1)] out bool loggable);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniEnableLog(int netId, [MarshalAs(UnmanagedType.I1)] bool enable, StringBuilder logPath);


        /*** Version ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetOsVersion(int netId, out int version);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetApiVersion();


        /*** Info ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetDeviceType(int netId, out int type);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetAxisCount(int netId, out int count);


        /*** Parameter ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniWriteParameter(int netId, int paramNo, int axisNo, int dataType, int data, float fdata);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniReadParameter(int netId, int paramNo, int axisNo, int dataType, out int data, IntPtr fdata);


        /*** Status ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetErrorCode(int netId, int axisNo, out int error);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetDriveStatus(int netId, int axisNo, out int status, IntPtr fstatus);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetDriveAlarm(int netId, int axisNo, out int alarm);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMotionStatus(int netId, int axisNo, out int status);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMotionAlarm(int netId, int axisNo, out int alarm);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetConfiguration(int netId, int axisNo, out int configuration);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMotionDone(int netId, int axisNo, out int done);


        /*** RPM, Voltage and Current ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetCommandRpm(int netId, int axisNo, IntPtr rpm);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMotorRpm(int netId, int axisNo, IntPtr rpm);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMotorCurrent(int netId, int axisNo, IntPtr current);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetDcLinkVoltage(int netId, int axisNo, IntPtr voltage);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetDeviceTemperature(int netId, int axisNo, IntPtr temperature);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetCpuTemperature(int netId, int axisNo, IntPtr temperature);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetAllPulse(int netId, out int commandPulse, out int actualPulse, out int errorPulse);

        /*** Position ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetCommandPosition(int netId, int axisNo, int position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetCommandPosition(int netId, int axisNo, out int position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetActualPosition(int netId, int axisNo, int position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetActualPosition(int netId, int axisNo, out int position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetHomePosition(int netId, int axisNo, int offsetPosition);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetAllPosition(int netId, out int commandPosition, out int actualPosition);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetAllPositionEx(int netId, out int commandPosition, out int actualPosition, out int errorPosition);


        /*** Servo Enable ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetServoOn(int netId, int axisNo, int enable);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetServoOn(int netId, int axisNo, out int enable);


        /*** Operation ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniReset(int netId, int axisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSlowStop(int netId, int axisNo, int decelTime);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniEmergencyStop(int netId, int axisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniOverrideVelocity(int netId, int axisNo, int rate);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetSyncAxis(int netId, int axisNo, int enable, int syncAxisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetSyncAxis(int netId, int axisNo, out int enable, out int syncAxisNo);


        /*** Motion ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMove(int netId, int axisNo, int moveType, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                int target, int point, int edge, int preCheck, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveTrapezoidal(int netId, int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int position, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveTrapezoidalIo(int netId, int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int position, int point, int edge, int preCheck, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveSCurve(int netId, int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int position, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveSCurveIo(int netId, int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int position, int point, int edge, int preCheck, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveVelocity(int netId, int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int direction);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveVelocityIo(int netId, int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int direction, int point, int edge, int preCheck);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveLine(int netId, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int axisA_Position, int axisB_Position, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveArcRadius(int netId, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int axis0_position, int axis1_position, int cwCcw, int radius, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveArcAngle(int netId, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int centerX, int centerY, int angle, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniMoveCircleAngle(int netId, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                                int cwCcw, int centerX, int centerY, int rotateCount, int angle, int mode = (int)eUniMotionMode.Absolute);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetSyncVelocityFromCircleAngle(int netId, int centerX, int centerY, int rotateCount, int angle, int velocity,
                                                                            int position, out int syncVelocity);


        /*** Homing ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniAddHomingStep(int netId, int axisNo, int stepNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                    int direction, int point, int edge, int preCheck, int delay);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniRemoveHomingStep(int netId, int axisNo, int stepNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniRemoveAllHomingStep(int netId, int axisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetHomingStep(int netId, int axisNo, int stepNo, out int velocity, out int accel, out int decel, out int jerkAcc, out int jerkDec,
                                                            out int direction, out int point, out int edge, out int preCheck, out int delay);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetHomingStepCount(int netId, int axisNo, out int count);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetHomingShift(int netId, int axisNo, int use, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetHomingShift(int netId, int axisNo, out int use, out int velocity, out int accel, out int decel, out int jerkAcc, out int jerkDec, out int position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStartHoming(int netId, int axisNo, int homePosition);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStartHomingMethod(int netId, int axisNo, int methodNo, int switchVelocity, int switchAccel, int switchDecel, int homingVelocity, int homingAccel, int homingDecel, int offsetPosition, int useMoveOffset);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStopHoming(int netId, int axisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniIsHoming(int netId, int axisNo, out int isHoming, out int isStep, out int isOffset);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetHomingResult(int netId, int axisNo, out int stepNo, out int rate, out int statusCode);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetHomingResultEx(int netId, int axisNo, out int stepNo, out int rate, out int statusCode, out int status);


        /*** In-position ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetInpositionPulseRange(int netId, int axisNo, int pulse);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetInpositionPulseRange(int netId, int axisNo, out int pulse);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetInpositionDuration(int netId, int axisNo, int time);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetInpositionDuration(int netId, int axisNo, out int time);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetInpositionTimeout(int netId, int axisNo, int time);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetInpositionTimeout(int netId, int axisNo, out int time);


        /*** Software Position Limit ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetSoftwarePositionLimit(int netId, int axisNo, int negativePosition, int positivePosition);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetSoftwarePositionLimit(int netId, int axisNo, out int negativePosition, out int positivePosition);


        /*** Latch ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStartLatchPosition(int netId, int axisNo, int edge);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStopLatchPosition(int netId, int axisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetLatchPosition(int netId, int axisNo, out int position, out int firstPosition);


        /*** I/O ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetOutput(int netId, int axisNo, int outputNo, int onOff);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetIo(int netId, int axisNo, out int input, out int output);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetSignalStatus(int netId, int axisNo, out int limitN, out int limitP, out int home, out int alarm, out int ready, out int on);


        /*** Trigger ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetTriggerOn(int netId, int axisNo, int enable);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetTriggerOn(int netId, int axisNo, out int enable);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetTriggerCount(int netId, int axisNo, out int count);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniResetTrigger(int netId, int axisNo);


        /*** Loopback ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniSetLoopback(int netId, int axisNo, int enable);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetLoopback(int netId, int axisNo, out int enable);


        /*** Monitoring ***/

        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStartMonitoring(int netId, int axisNo, int interval, int type = (int)eUniMonitoringSource.Position);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniStopMonitoring(int netId, int axisNo);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMonitoringInfo(int netId, out int axisNo, out int command, out int interval, out int dataIndex);
        [DllImport("EMotionUniDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern int eUniGetMonitoringData(int netId, int dataIndex, int dataCount, out int commandData, out int actualData, out int status);

        #endregion

        #region Define

        /// <summary>
        /// The max count of data while monitoring axis
        /// </summary>
        public enum UniMonitoring
        {
            DATA_MAX_COUNT = 1500,
        }

        /// <summary>
        /// The max count of axis in uni device
        /// </summary>
        public readonly int AxisMaxCount = 2;

        /// <summary>
        /// Device communication data count
        /// </summary>
        public readonly int ComDataCount = 12;

        #endregion

        #region Property

        /// <summary>
        /// Device IPv4 address 1
        /// </summary>
        public int Ipv4Addr1 { get; set; } = 192;
        /// <summary>
        /// Device IPv4 address 2
        /// </summary>
        public int Ipv4Addr2 { get; set; } = 168;
        /// <summary>
        /// Device IPv4 address 3
        /// </summary>
        public int Ipv4Addr3 { get; set; } = 240;
        /// <summary>
        /// Device Network ID(IPv4 address 4)
        /// </summary>
        public int NetID { get; protected set; } = 1;
        /// <summary>
        /// Device Network Port
        /// </summary>
        public int Port { get; set; } = 10025;

        #endregion

        #region API Wrapper Function

        /// <summary>
        /// Connect to device using ethernet(UDP)
        /// </summary>
        /// <param name="netId">	: Network ID(IP address 4)</param>
        /// <param name="port">		: Network Port</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success				: Operation Success 
        /// @ eUniApiReturnCode.AlreadyConnected	: Already connected to device
        /// @ eUniApiReturnCode.Disconnected		: Failed to connect to device
        /// @ eUniApiReturnCode.Disconnecting		: Currently disconnecting state
        /// @ eUniApiReturnCode.Environment			: Not initialized API environment
        /// @ eUniApiReturnCode.FailedCommunication : Failed to try to communicate to device
        /// </returns>
        public virtual int Connect(int netId, int port)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniConnect(netId, port);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to connect device" +
                            "(Network ID: " + netId.ToString() +
                            ", Port number: " + port.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.Connect");
                }
                this.NetID = netId;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.Connect");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Connect to device using ethernet(UDP)
        /// </summary>
        /// <remarks>
        /// This API is extension function 'Connect' included arguments about IP address
        /// </remarks>
        /// <param name="ip1">		: IPv4 address 1</param>
        /// <param name="ip2">		: IPv4 address 2</param>
        /// <param name="ip3">		: IPv4 address 3</param>
        /// <param name="netId">	: Network ID(IP address 4)</param>
        /// <param name="port">		: Network Port</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success				: Operation Success 
        /// @ eUniApiReturnCode.AlreadyConnected	: Already connected to device
        /// @ eUniApiReturnCode.Disconnected		: Failed to connect to device
        /// @ eUniApiReturnCode.Disconnecting		: Currently disconnecting state
        /// @ eUniApiReturnCode.Environment			: Not initialized API environment
        /// @ eUniApiReturnCode.FailedCommunication : Failed to try to communicate to device
        /// </returns>
        public virtual int Connect(int ip1, int ip2, int ip3, int netId, int port)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniConnectEx(ip1, ip2, ip3, netId, port);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to connect device" +
                            "(Network: " + ip1 + "." + ip2 + "." + ip3 + "." + netId.ToString() +
                            ", Port number: " + port.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.Connect");
                }
                this.NetID = netId;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.Connect");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Disconnect to device
        /// </summary>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: Not supported current communication type
        /// @ eUniApiReturnCode.Environment					: Not initialized API environment
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to disconnect to device
        /// </returns>
        public virtual int Disconnect()
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniDisconnect(this.NetID);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to disconnect device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.Disconnect");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.Disconnect");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Reconnect to device with current connection setting
        /// </summary>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: Not supported current communication type
        /// @ eUniApiReturnCode.Environment					: Not initialized API environment
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to disconnect to device
        /// </returns>
        public virtual int Reconnect()
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniReconnect(this.NetID);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to reconnect to device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.Reconnect");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.Reconnect");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        ///  Get whether API is connected or disconnected to device
        /// </summary>
        /// <param name="connected">	: Whether the API is connected or disconnected to device
        /// @ true	: Connected to device
        /// @ false	: Disconnected to device
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.InvalidNetworkId			: Invalid network id(IP address 4)
        /// </returns>
        public virtual int IsConnected(ref bool connected)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniIsConnected(this.NetID, out bool isConnected);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to check connected to device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.IsConnected");
                }
                else
                {
                    connected = isConnected;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.IsConnected");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Check connection to device
        /// </summary>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to check communication to device
        /// </returns>
        public virtual int CheckConnection()
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniCheckConnection(this.NetID);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to check connection to device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.CheckConnection");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.CheckConnection");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set communication(waiting to replying data) configuration(timeout, retry count)
        /// </summary>
        /// <param name="time">	        : timeout(msec)</param>
        /// <param name="retryCount">	: The count of retries to send message</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// </returns>
        public virtual int SetCommunicationConfig(int time, int retryCount)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetCommunicationConfig(this.NetID, time, retryCount);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set connection config to device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetCommunicationConfig");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetCommunicationConfig");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get communication(waiting to replying data) configuration(timeout, retry count)
        /// </summary>
        /// <param name="time">	        : timeout(msec)</param>
        /// <param name="retryCount">	: The count of retries to send message</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetCommunicationConfig(ref int time, ref int retryCount)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetCommunicationConfig(this.NetID, out int timeTemp, out int countTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get connection config from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetCommunicationTimeout");
                }
                else
                {
                    time = timeTemp;
                    retryCount = countTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetCommunicationTimeout");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get system(windows) error when to be occurred communication failure
        /// </summary>
        /// <param name="error">		: System error code</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// </returns>
        public virtual int GetSystemErrorCode(ref int error)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetSystemErrorCode(this.NetID, out int errorCode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get system error code" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSystemErrorCode");
                }
                else
                {
                    error = errorCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSystemErrorCode");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Check to be enable writing log file
        /// </summary>
        /// <param name="useLog">	: Whether writing log file is enable or disable
        /// @ true	: Enable writing log file
        /// @ false	: Disable writing log file
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.InvalidNetworkId			: Invalid network id(IP address 4)
        /// </returns>
        public virtual int GetLogInfo(ref bool useLog)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetLogInfo(this.NetID, out bool isLog);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get to be enable log from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.IsLoggable");
                }
                else
                {
                    useLog = isLog;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.IsLoggable");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Enable to write log file
        /// </summary>
        /// <param name="enable">	: Whether writing log file is enable or disable</param>
        /// <param name="logPath">	: The directory path to be written log file</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.InvalidArgument				: The argument 'logPath' is invalid
        /// @ eUniApiReturnCode.DataOverflow				: The argument 'logPath' is small size
        /// @ eUniApiReturnCode.Environment					: Not initialized API environment
        /// </returns>
        public virtual int EnableLog(bool enable, string logPath)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniEnableLog(this.NetID, enable, new StringBuilder(logPath));
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to enable log to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Path: " + logPath.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.EnableLog");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.EnableLog");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get OS(firmware) version from device
        /// </summary>
        /// <param name="version">	: Software version information.
        /// * Index 0: Drive software version
        ///		# bit 31~24 : Major
        ///		# bit 23~16 : Minor
        ///		# bit 15~09 : Year
        ///		# bit 08~05 : Month
        ///		# bit 04~00 : Day
        /// * Index 1 : Motion software version
        ///		# bit 31~24 : Major
        ///		# bit 23~16 : Minor
        ///		# bit 15~09 : Year
        ///		# bit 08~05 : Month
        ///		# bit 04~00 : Day
        /// * Index 2 : Communication software version
        ///		# bit 31~24 : Major
        ///		# bit 23~16 : Minor
        ///		# bit 15~09 : Year
        ///		# bit 08~05 : Month
        ///		# bit 04~00 : Day
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetOsVersion(int[] version)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (version.Length < 3)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetOsVersion(this.NetID, out version[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get os version from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetOsVersion");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetOsVersion");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get API version
        /// </summary>
        /// <returns>
        /// API version. 
        ///	# 31~24 bit : Major
        ///	# 23~16 bit : Minor
        ///	# 15~08 bit : Patch
        ///	# 07~00 bit : Fix
        /// </returns>
        public virtual int GetApiVersion()
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetApiVersion();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetApiVersion");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get device(product) type information.
        /// </summary>
        /// <param name="type">		: Information buffer
        /// * Index 0 : Hardware type.
        ///		# Bit 31~28 bit : Hardware type. 1 : 20~60mm motor drive, 2 : 86mm motor drive
        ///		# Bit 27~24 bit : Inverter type. 1 : Stepper drive
        ///		# Bit 23~16 bit : Communication type. 1 : Ethernet, 2 : EtherCAT
        ///		# Bit 15~08 bit : Axis type. 1 : 1 axis, 2 : 2 axis
        ///		# Bit 07~00 bit : Reserved
        /// * Index 1 : Product information
        ///		# Bit 31~23 bit : Major type(Motor drive, I / O board, Controller, ETC...)
        ///		# Bit 23~16 bit : Reserved
        ///		# Bit 15~08 bit : Middle type(Motor drive->Step Ethernet Drive, Step EtherCAT Drive, ETC...)
        ///		# Bit 07~00 bit : Minor type(Motor drive->Motor size, Axis count, ETC...)
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetDeviceType(int[] type)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (type.Length < 2)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetDeviceType(this.NetID, out type[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get type from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetDeviceType");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetDeviceType");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get axis count from device
        /// </summary>
        /// <param name="count"> : Max axis count</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetAxisCount(ref int count)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetAxisCount(this.NetID, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get axis count from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetAxisCount");
                }
                else
                {
                    count = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetAxisCount");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Write parameter data to device
        /// </summary>
        /// <param name="paramNo">	: Parameter number(address)</param>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="dataType"> : Data type. 
        ///		@ eUniDataType.Int32	: integer	(32bit) 
        ///		@ eUniDataType.Float32	: floating	(32bit)
        /// </param>
        /// <param name="data">		: Integer data to write to device</param>
        /// <param name="fdata">	: Floating data to write to device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The argument 'axisNo' is invaild
        /// </returns>
        public virtual int WriteParameter(int paramNo, int axisNo, int dataType, int data, float fdata)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniWriteParameter(this.NetID, paramNo, axisNo, dataType, data, fdata);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to write parameter to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Parameter number: " + paramNo.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Type: " + dataType.ToString() +
                            ", Int Data: " + data.ToString() +
                            ", Float Data: " + fdata.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.WriteParameter");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.WriteParameter");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Read parameter data from device
        /// </summary>
        /// <param name="paramNo">	: Parameter number(address)</param>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="dataType"> : Data type. 
        ///		@ eUniDataType.Int32	: integer	(32bit) 
        ///		@ eUniDataType.Float32	: floating	(32bit)
        /// </param>
        /// <param name="data">		: Integer data to read from device</param>
        /// <param name="fdata">	: Floating data to read from device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The argument 'axisNo' is invaild
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int ReadParameter(int paramNo, int axisNo, int dataType, ref int data, ref float fdata)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;
            fdata = 0;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniReadParameter(this.NetID, paramNo, axisNo, dataType, out int dataTemp, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to read parameter from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Parameter number: " + paramNo.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Type: " + dataType.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.ReadParameter");
                }
                else
                {
                    // Get integer data
                    data = dataTemp;
                    // Get floating data
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    fdata = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.ReadParameter");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get error code from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="error">	: enum eUniDeviceErrorCode</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetErrorCode(int axisNo, ref int error)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetErrorCode(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get error code from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetErrorCode");
                }
                else
                {
                    error = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetErrorCode");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get drive status from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="status">	: Drive integer status
        /// * Index 0 : Command pulse	[count]
        /// * Index 1 : Encoder pulse	[count]
        /// * Index 2 : Status(Alarm)
        ///		# Bit 0		: Drive is ready(1: Ready, 0 : Not ready)
        ///		# Bit 1		: Servo(motor) is on(1: On, 0 : Off)
        ///		# Bit 2~3	: Motor is running(11: Running, 00 : Stop)
        ///		# Bit 4		: Drive is over current(1: Over current, 0 : Not over current)
        ///		# Bit 5		: Motor is over current(1: Over current, 0 : Not over current)
        ///		# Bit 6		: Drive is over temperature(1: Over temperature, 0 : Not over temperature)
        ///		# Bit 7		: Drive is under temperature(1: Under temperature, 0 : Not under temperature)
        ///		# Bit 8		: CPU is over temperature(1: Over temperature, 0 : Not over temperature)
        ///		# Bit 9		: CPU is under temperature(1: Under temperature, 0 : Not under temperature)
        ///		# Bit 10	: DC link is over voltage(1: Over voltage, 0 : Not over voltage)
        ///		# Bit 11	: DC link is under voltage(1: Under voltage, 0 : Not under voltage)
        ///		# Bit 12	: Overload(following error) is occured(1: Occured, 0 : Not occured)
        ///		# Bit 13	: Motor is over rpm(1: Over rpm, 0 : Not over rpm)
        ///		# Bit 14	: Encoder detected(1: Dectected, 0 : None)-- > Not currently used
        ///		# Bit 15	: Invalid motor detected(1: Dectected, 0 : None)
        ///		# Bit 16	: Command pulse is over rpm(1: Over rpm, 0 : Not over rpm) </param>
        /// <param name="fstatus">	: Drive floating status
        /// * Index 0: Command rpm						[rotation count / min]
        /// * Index 1: Motor rpm						[rotation count / min]
        /// * Index 2 : Motor current					[A]
        /// * Index 3 : DC link voltage					[V]
        /// * Index 4 : Driver(device) temperature
        /// * Index 5 : DSP(CPU) temperature
        /// * Index 6 : Real inductance
        /// * Index 7 : Real resistance
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetDriveStatus(int axisNo, int[] status, float[] fstatus)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            if (status.Length < 3)
                return (int)eUniApiReturnCode.NoData;

            if (fstatus.Length < 8)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float) * fstatus.Length);
                returnCode = eUniGetDriveStatus(this.NetID, axisNo, out status[0], ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get drive status from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetDriveStatus");
                }
                else
                {
                    Marshal.Copy(ptr, fstatus, 0, fstatus.Length);
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetDriveStatus");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get drive alarm from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="alarm">	: Drive status(alarm)
        /// # Bit 0		: Drive is ready						(1: Ready, 0 : Not ready)
        /// # Bit 1		: Servo(motor) is on					(1: On, 0 : Off)
        /// # Bit 2~3	: Motor is running						(11: Running, 00 : Stop)
        /// # Bit 4		: Drive is over current					(1: Over current, 0 : Not over current)
        /// # Bit 5		: Motor is over current					(1: Over current, 0 : Not over current)
        /// # Bit 6		: Drive is over temperature				(1: Over temperature, 0 : Not over temperature)
        /// # Bit 7		: Drive is under temperature			(1: Under temperature, 0 : Not under temperature)
        /// # Bit 8		: CPU is over temperature				(1: Over temperature, 0 : Not over temperature)
        /// # Bit 9		: CPU is under temperature				(1: Under temperature, 0 : Not under temperature)
        /// # Bit 10	: DC link is over voltage				(1: Over voltage, 0 : Not over voltage)
        /// # Bit 11	: DC link is under voltage				(1: Under voltage, 0 : Not under voltage)
        /// # Bit 12	: Overload(following error) is occured	(1: Occured, 0 : Not occured)
        /// # Bit 13	: Motor is over rpm						(1: Over rpm, 0 : Not over rpm)
        /// # Bit 14	: Encoder detected						(1: Dectected, 0 : None)-- > Not currently used
        /// # Bit 15	: Invalid motor detected				(1: Dectected, 0 : None)
        /// # Bit 16	: Command pulse is over rpm				(1: Over rpm, 0 : Not over rpm)
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetDriveAlarm(int axisNo, ref int alarm)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetDriveAlarm(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get drive alarm from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetDriveAlarm");
                }
                else
                {
                    alarm = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetDriveAlarm");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get motion status from device
        /// </summary>
        /// <param name="axisNo"> : Axis number</param>
        /// <param name="status"> : Motion integer status
        /// * Index 0 : Moving					(1: Moving, 0 : Not moving)
        /// * Index 1 : Profiling				(1: Profiling, 0 : Not profiling)
        /// * Index 2 : In - position success	(1: OK, 0 : Not)
        /// * Index 3 : Command position		[um]
        /// * Index 4 : Actual position			[um]
        /// * Index 5 : Command velocity		[mm / min]
        /// * Index 6 : Actual velocity			[mm / min]
        /// * Index 7 : Input value
        ///		# Bit 0 : Input0				(1: On, 0 : Off)
        ///		# Bit 1 : Input1				(1: On, 0 : Off)
        ///		# Bit 2 : Input2				(1: On, 0 : Off)
        ///		# Bit 3 : Input3				(1: On, 0 : Off)
        /// * Index 8 : Output value
        ///		# Bit 0 : Output0				(1: On, 0 : Off)
        ///		# Bit 1 : Output1				(1: On, 0 : Off)
        /// * Index 9 : Speed(Velocity) overriding rate(10~200 %)
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMotionStatus(int axisNo, int[] status)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (status.Length < 10)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetMotionStatus(this.NetID, axisNo, out status[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get motion status from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetMotionStatus");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetMotionStatus");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get motion alarm from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="alarm">	: Motion alarm bit
        ///		# Bit 0 : (+)limit switch error				(1: Detected, 0 : None)
        ///		# Bit 1 : (-)limit switch error				(1: Detected, 0 : None)
        ///		# Bit 2 : (+)limit software position error	(1: Occured, 0 : None)
        ///		# Bit 3 : (-)limit software position error	(1: Occured, 0 : None)
        ///		# Bit 4 : Following error					(1: Occured, 0 : None)
        ///		# Bit 5 : In-position timeout				(1: Timeout, 0 : None) 
        ///		# Bit 6 : Invalid pulse						(1: Occured, 0 : None) 
        ///		# Bit 7 : Safety signal						(1: Detected, 0 : None) 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMotionAlarm(int axisNo, ref int alarm)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetMotionAlarm(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get motion alarm from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.eUniGetMotionAlarm");
                }
                else
                {
                    alarm = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.eUniGetMotionAlarm");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Check whether the axis motion is done or not done
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="done">		: Whether the motion is done or not done</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMotionDone(int axisNo, ref bool done)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetMotionDone(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get motion done from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetMotionDone");
                }
                else
                {
                    if (data > 0)
                        done = true;
                    else
                        done = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetMotionDone");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get configuration from device
        /// </summary>
        /// <param name="axisNo">			: Axis number</param>
        /// <param name="configuration">	: Drive integer information
        /// * Index 0 : Motor configuration							(1: Done, 0 : Not configuration)
        /// * Index 1 : Whether the driver connect invalid motor	(1: Invalid motor, 0 : Normal motor)
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetConfiguration(int axisNo, int[] configuration)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (configuration.Length < 2)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetConfiguration(this.NetID, axisNo, out configuration[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get configuration from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetConfiguration");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetConfiguration");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get command rpm from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="rpm">		: Commnad RPM data	[rotation count / min]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetCommandRpm(int axisNo, ref float rpm)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniGetCommandRpm(this.NetID, axisNo, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get command RPM from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetCommandRpm");
                }
                else
                {
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    rpm = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetCommandRpm");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get motor rpm from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="rpm">		: Motor RPM data	[rotation count / min]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMotorRpm(int axisNo, ref float rpm)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniGetMotorRpm(this.NetID, axisNo, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get motor RPM from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetMotorRpm");
                }
                else
                {
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    rpm = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetMotorRpm");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get motor current from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="current">	: Motor current	[A]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMotorCurrent(int axisNo, ref float current)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniGetMotorCurrent(this.NetID, axisNo, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get motor currnet from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetCommandRpm");
                }
                else
                {
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    current = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetMotorCurrent");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get DC link voltage from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="voltage">	: DC link voltage	[V]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetDcLinkVoltage(int axisNo, ref float voltage)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniGetDcLinkVoltage(this.NetID, axisNo, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get DC Link voltage from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetDcLinkVoltage");
                }
                else
                {
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    voltage = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetDcLinkVoltage");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get drive(device) temperature
        /// </summary>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="temperature">	: Drive(device) temperature</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetDeviceTemperature(int axisNo, ref float temperature)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniGetDeviceTemperature(this.NetID, axisNo, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get device temperature from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetDeviceTemperature");
                }
                else
                {
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    temperature = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetDeviceTemperature");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get CPU temperature from device
        /// </summary>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="temperature">	: CPU temperature</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetCpuTemperature(int axisNo, ref float temperature)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            IntPtr ptr;

            try
            {
                ptr = Marshal.AllocHGlobal(sizeof(float));
                returnCode = eUniGetCpuTemperature(this.NetID, axisNo, ptr);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get CPU temperature from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetCpuTemperature");
                }
                else
                {
                    float[] temp = new float[1];
                    Marshal.Copy(ptr, temp, 0, 1);
                    temperature = temp[0];
                    temp = null;
                }
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetCpuTemperature");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get all(command, actual, error) axes pulse from device.
        /// </summary>
        /// <remarks>
        /// This function is supported from communication OS version 2.5.
        /// </remarks>
        /// <param name="commandPulse">	: Command pulse buffer to read from device. the data count is axis count in device</param>
        /// <param name="actualPulse">	: Actual pulse buffer to read from device. the data count is axis count in device</param>
        /// <param name="errorPulse">	: Error(delta) pulse buffer to read from device. the data count is axis count in device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetAllPulse(int[] commandPulse, int[] actualPulse, int[] errorPulse)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (commandPulse.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            if (actualPulse.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            if (errorPulse.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetAllPulse(this.NetID, out commandPulse[0], out actualPulse[0], out errorPulse[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get all pulse from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            "). " +
                            "error : " + returnCode,
                            "UniDevice.GetAllPulse");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetAllPulse");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set command position to device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="position"> : Position to write to device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetCommandPosition(int axisNo, int position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetCommandPosition(this.NetID, axisNo, position);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set command position to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Position: " + position.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetCommandPosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetCommandPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get command position from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="position"> : Position to read from device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetCommandPosition(int axisNo, ref int position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetCommandPosition(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get command position from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetCommandPosition");
                }
                else
                {
                    position = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetCommandPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set actual(encoder) position to device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="position"> : Position to write to device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetActualPosition(int axisNo, int position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetActualPosition(this.NetID, axisNo, position);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set actual position to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Position: " + position.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetActualPosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetActualPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get actual(encoder) position from device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="position"> : Position to read from device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetActualPosition(int axisNo, ref int position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetActualPosition(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get actual position from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetActualPosition");
                }
                else
                {
                    position = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetActualPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set home position to device
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="offsetPosition"> : Position to write to device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetHomePosition(int axisNo, int offsetPosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetHomePosition(this.NetID, axisNo, offsetPosition);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set home position to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Position: " + offsetPosition.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetHomePosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetHomePosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get all(command, actual) axes position from device
        /// </summary>
        /// <param name="commandPosition">	: Command position buffer to read from device. the data count is axis count in device</param>
        /// <param name="actualPosition">	: Actual position buffer to read from device. the data count is axis count in device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetAllPosition(int[] commandPosition, int[] actualPosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (commandPosition.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            if (actualPosition.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetAllPosition(this.NetID, out commandPosition[0], out actualPosition[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get all position from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetAllPosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetAllPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get all(command, actual, error) axes position from device.
        /// This function is extension function about 'GetAllPosition'
        /// </summary>
        /// <remarks>
        /// This function is supported from communication OS version 2.5.
        /// </remarks>
        /// <param name="commandPosition">	: Command position buffer to read from device. the data count is axis count in device</param>
        /// <param name="actualPosition">	: Actual position buffer to read from device. the data count is axis count in device</param>
        /// <param name="errorPosition">	: Error(delta) position buffer to read from device. the data count is axis count in device</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetAllPosition(int[] commandPosition, int[] actualPosition, int[] errorPosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (commandPosition.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            if (actualPosition.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            if (errorPosition.Length < AxisMaxCount)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetAllPositionEx(this.NetID, out commandPosition[0], out actualPosition[0], out errorPosition[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get all position from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetAllPositionEx");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetAllPositionEx");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Enable or disable servo(motor)
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="enable">	: Servo(motor) enable or disable	(enable: true, disable: false)</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetServoOn(int axisNo, bool enable)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int data = 0;

            try
            {
                if (enable)
                    data = 1;
                else
                    data = 0;

                returnCode = eUniSetServoOn(this.NetID, axisNo, data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set servo(motor) ON / OFF data to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Enable: " + enable.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetServoOn");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetServoOn");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get whether servo(motor) is enable or disable
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="enable">	: Servo(motor) enable or disable	(enable: true, disable: false)</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetServoOn(int axisNo, ref bool enable)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int data = 0;

            try
            {
                returnCode = eUniGetServoOn(this.NetID, axisNo, out data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get servo(driver) ON / OFF data from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetServoOn");
                }
                else
                {
                    if (data > 0)
                        enable = true;
                    else
                        enable = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetServoOn");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Reset axis operation(motion, alarm) to device
        /// </summary>
        /// <param name="axisNo"> : Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int Reset(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniReset(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to reset to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.Reset");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.Reset");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Stop axis motion to device using decleration time.
        /// </summary>
        /// <remarks>
        /// 1) Acceleration period : Stop motion applied deceleration time from current velocity
        /// 2) Constant period : Stop motion applied deceleration time from command velocity
        /// 3) Deceleration period : No stop motion.just the axis motion will be stopped applied to motion profiling
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="decelTime">	: Deceleration time to stop moving</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SlowStop(int axisNo, int decelTime)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSlowStop(this.NetID, axisNo, decelTime);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to stop to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SlowStop");
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SlowStop");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Emergency stop axis motion to device.
        /// </summary>
        /// <remarks>
        /// Emergency stop deceleration is applied using parameter 'Max velocity', 'Emergency stop deceleration time'
        /// </remarks>
        /// <param name="axisNo">   : Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int EmergencyStop(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniEmergencyStop(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to emergency stop to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.EmergencyStop");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.EmergencyStop");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Override speed(velocity) to motion
        /// </summary>
        /// <remarks>
        /// If the axis is not running motion, the speed overriding rate is applied to target velocity when to start motion
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="rate">		: Speed overriding rate(Default: 100%)</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int OverrideVelocity(int axisNo, int rate)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniOverrideVelocity(this.NetID, axisNo, rate);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set speed overriding rate to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.OverrideVelocity");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.OverrideVelocity");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set synchronnizing(sync master) mode to specific axis number
        /// </summary>
        /// <remarks>
        /// The axis number is set master axis. sync(another) axis number is set slave axis.
        /// Activation of operation is only on the master axis. the slave axis is disable operation
        /// </remarks>
        /// <param name="axisNo">		: Axis number to set synchronizing mode</param>
        /// <param name="enable">		: Whether synchronizing mode is enable or disable</param>
        /// <param name="syncAxisNo">	: Another axis number to be synchronized by the axis</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetSyncAxis(int axisNo, bool enable, int syncAxisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int data = 0;

            try
            {
                if (enable)
                    data = 1;
                else
                    data = 0;

                returnCode = eUniSetSyncAxis(this.NetID, axisNo, data, syncAxisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set sync axis to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Enable: " + enable.ToString() +
                            ", Sync axis number : " + syncAxisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetSyncAxis");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetSyncAxis");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get axis number which is activated synchronnizing(sync master) mode
        /// </summary>
        /// <param name="axisNo">		: Axis number to get synchronnizing operation setting</param>
        /// <param name="enable">		: Whether synchronizing mode is enable or disable</param>
        /// <param name="syncAxisNo">	: Another axis number to be synchronized by the axis</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetSyncAxis(int axisNo, ref bool enable, ref int syncAxisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int data = 0;

            try
            {
                returnCode = eUniGetSyncAxis(this.NetID, axisNo, out data, out syncAxisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get sync axis from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSyncAxis");
                }
                else
                {
                    if (data > 0)
                        enable = true;
                    else
                        enable = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSyncAxis");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis
        /// </summary>
        /// <remarks>
        /// If the input signal comes in according to the arguments, the axis motion stops
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="moveType">		: Move(Profile) type (enum eUniMoveType)</param>
        /// <param name="velocity">		: Target velocity to move			[mm/min]</param>
        /// <param name="accel">		: Acceleration time					[msec]</param>
        /// <param name="decel">		: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">		: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">		: Jerk ratio in deceleration period [%]</param>
        /// <param name="target">
        /// @ Position Move Type : Target position to move.					[um]
        /// @ Velocity Move Type : Moving direction.
        /// 1 : Positive(+) direction
        /// 0 : Negative(-) direction	
        /// </param>
        /// <param name="point">		: Axis input number to set stop option(enum eUniAxisInputNumber).
        /// 0 : Input0(Limit(-) switch)
        /// 1 : Input1(Home switch)
        /// 2 : Input2(Limit(+) switch )
        /// 3 : Input3(User In) 
        /// </param>
        /// <param name="edge">			: The edge type to check input signal.
        /// 1 : Rising edge
        /// 0 : Falling edge
        /// </param>
        /// <param name="preCheck">		: The option to check whether the input signal has already been come in before motion is executed.
        /// 1 : If the input signal according to the arguments has been already come in, no execute the motion
        /// 0 : Not used the option
        /// </param>
        /// <param name="mode">			: Moving mode (enum eUniMotionMode). The argument is only valid to 'Position Move Type'
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int Move(int axisNo, int moveType, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                int target, int point, int edge, int preCheck, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMove(this.NetID, axisNo, moveType, velocity, accel, decel, jerkAcc, jerkDec, target, point, edge, preCheck, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.Move");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.Move");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis using trapezoidal profile
        /// </summary>
        /// <remarks>
        /// The shape of the motion velocity is trapezoidal
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="velocity"> : Target velocity to move		[mm/min]</param>
        /// <param name="accel">	: Acceleration time				[msec]</param>
        /// <param name="decel">	: Deceleration time				[msec]</param>
        /// <param name="jerkAcc">	: Not used. insert value '0'</param>
        /// <param name="jerkDec">	: Not used. insert value '0'</param>
        /// <param name="position"> : Target position to move		[um]</param>
        /// <param name="mode">		: Moving mode(enum eUniMotionMode).
        /// 0 : Absolute mode
        /// 1 : Incremetal mode
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveTrapezoidal(int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int position,
                                int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveTrapezoidal(this.NetID, axisNo, velocity, accel, decel, jerkAcc, jerkDec, position, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move trapezoidal to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveTrapezoidal");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveTrapezoidal");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis using trapezoidal profile until input signal comes in
        /// </summary>
        /// <remarks>
        /// If the input signal comes in according to the arguments, the axis motion stops
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="velocity"> : Target velocity to move		[mm/min]</param>
        /// <param name="accel">	: Acceleration time				[msec]</param>
        /// <param name="decel">	: Deceleration time				[msec]</param>
        /// <param name="jerkAcc">	: Not used. insert value '0'</param>
        /// <param name="jerkDec">	: Not used. insert value '0'</param>
        /// <param name="position"> : Target position to move		[um]</param>
        /// <param name="point">	: Axis input number to set stop option(enum eUniAxisInputNumber).
        /// 0 : Input0(Limit(-) switch)
        /// 1 : Input1(Home switch)
        /// 2 : Input2(Limit(+) switch )
        /// 3 : Input3(User In) 
        /// </param>
        /// <param name="edge">		: The edge type to check input signal.
        /// 1 : Rising edge
        /// 0 : Falling edge
        /// </param>
        /// <param name="preCheck"> : The option to check whether the input signal has already been come in before motion is executed.
        /// 1 : If the input signal according to the arguments has been already come in, no execute the motion
        /// 0 : Not used the option
        /// </param>
        /// <param name="mode">		: Moving mode(enum eUniMotionMode). 
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveTrapezoidalIo(int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                    int position, int point, int edge, int preCheck, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveTrapezoidalIo(this.NetID, axisNo, velocity, accel, decel, jerkAcc, jerkDec, position, point, edge, preCheck, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move trapezoidal and to stop by input to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveTrapezoidalIo");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveTrapezoidalIo");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis using s-curve profile
        /// </summary>
        /// <remarks>
        /// The shape of the motion velocity is s-curve
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="velocity"> : Target velocity to move			[mm/min]</param>
        /// <param name="accel">	: Acceleration time					[msec]</param>
        /// <param name="decel">	: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">	: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">	: Jerk ratio in deceleration period [%]</param>
        /// <param name="position"> : Target position to move			[um]</param>
        /// <param name="mode">		: Moving mode(enum eUniMotionMode).
        /// 0 : Absolute mode
        /// 1 : Incremetal mode
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveSCurve(int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int position,
            int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveSCurve(this.NetID, axisNo, velocity, accel, decel, jerkAcc, jerkDec, position, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move s-curve to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveSCurve");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveSCurve");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis using s-curve profile until input signal comes in
        /// </summary>
        /// <remarks>
        /// If the input signal comes in according to the arguments, the axis motion stops
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="velocity"> : Target velocity to move			[mm/min]</param>
        /// <param name="accel">	: Acceleration time					[msec]</param>
        /// <param name="decel">	: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">	: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">	: Jerk ratio in deceleration period [%]</param>
        /// <param name="position"> : Target position to move			[um]</param>
        /// <param name="point">	: Axis input number to set stop option(enum eUniAxisInputNumber).
        /// 0 : Input0(Limit(-) switch)
        /// 1 : Input1(Home switch)
        /// 2 : Input2(Limit(+) switch )
        /// 3 : Input3(User In) 
        /// </param>
        /// <param name="edge">		: The edge type to check input signal.
        /// 1 : Rising edge
        /// 0 : Falling edge
        /// </param>
        /// <param name="preCheck"> : The option to check whether the input signal has already been come in before motion is executed.
        /// 1 : If the input signal according to the arguments has been already come in, no execute the motion
        /// 0 : Not used the option
        /// </param>
        /// <param name="mode">		: Moving mode(enum eUniMotionMode). 
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveSCurveIo(int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                int position, int point, int edge, int preCheck, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveSCurveIo(this.NetID, axisNo, velocity, accel, decel, jerkAcc, jerkDec, position, point, edge, preCheck, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move SCurve and to stop by input to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveSCurveIo");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveSCurveIo");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis using velocity
        /// </summary>
        /// <remarks>
        /// The shape of the motion velocity is trapezoidal. the motion is infinitely moving until the stop motion is executed.
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="velocity">		: Target velocity to move		[mm/min]</param>
        /// <param name="accel">		: Acceleration time				[msec]</param>
        /// <param name="decel">		: Deceleration time				[msec]</param>
        /// <param name="jerkAcc">		: Not used. insert value '0'</param>
        /// <param name="jerkDec">		: Not used. insert value '0'</param>
        /// <param name="direction">	: Moving direction.
        /// 1 : Positive(+) direction
        /// 0 : Negative(-) direction
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveVelocity(int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int direction)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveVelocity(this.NetID, axisNo, velocity, accel, decel, jerkAcc, jerkDec, direction);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move velocity to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveVelocity");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveVelocity");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move axis using velocity until input signal comes in
        /// </summary>
        /// <remarks>
        /// If the input signal comes in according to the arguments, the axis motion stops
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="velocity"> : Target velocity to move			[mm/min]</param>
        /// <param name="accel">	: Acceleration time					[msec]</param>
        /// <param name="decel">	: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">	: Not used. insert value '0'</param>
        /// <param name="jerkDec">	: Not used. insert value '0'</param>
        /// <param name="direction">	: Moving direction.
        /// 1 : Positive(+) direction
        /// 0 : Negative(-) direction
        /// </param> 
        /// <param name="point">	: Axis input number to set stop option(enum eUniAxisInputNumber).
        /// 0 : Input0(Limit(-) switch)
        /// 1 : Input1(Home switch)
        /// 2 : Input2(Limit(+) switch )
        /// 3 : Input3(User In) 
        /// </param>
        /// <param name="edge">		: The edge type to check input signal.
        /// 1 : Rising edge
        /// 0 : Falling edge
        /// </param>
        /// <param name="preCheck"> : The option to check whether the input signal has already been come in before motion is executed.
        /// 1 : If the input signal according to the arguments has been already come in, no execute the motion
        /// 0 : Not used the option
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveVelocityIo(int axisNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                                            int direction, int point, int edge, int preCheck)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveVelocityIo(this.NetID, axisNo, velocity, accel, decel, jerkAcc, jerkDec, direction, point, edge, preCheck);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move velocity and to stop by input to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveVelocityIo");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveVelocityIo");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move 2 axes to target position while moving the motion path in a line
        /// </summary>
        /// <remarks>
        /// If jerk ratio is '0', motion profile is trapezoidal.
        /// If jerk ratio is valid, motion profile is s-curve.
        /// </remarks>
        /// <param name="velocity">			: Target velocity to move			[mm/min]</param>
        /// <param name="accel">			: Acceleration time					[msec]</param>
        /// <param name="decel">			: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">			: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">			: Jerk ratio in deceleration period [%]</param>
        /// <param name="axis0_position">	: Target position of axis0(Target X coordinate)</param>
        /// <param name="axis1_position">	: Target position of axis1(Target Y coordinate)</param>
        /// <param name="mode">				: Moving mode(enum eUniMotionMode). 
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveLine(int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                            int axis0_position, int axis1_position, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveLine(this.NetID, velocity, accel, decel, jerkAcc, jerkDec, axis0_position, axis1_position, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move interpolation line to device. " +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveLine");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveLine");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move 2 axes to target position using radius while moving the motion path in a arc
        /// </summary>
        /// <remarks>
        /// If jerk ratio is '0', motion profile is trapezoidal.
        /// If jerk ratio is valid, motion profile is s-curve.
        /// </remarks>
        /// <param name="velocity">			: Target velocity to move			[mm/min]</param>
        /// <param name="accel">			: Acceleration time					[msec]</param>
        /// <param name="decel">			: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">			: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">			: Jerk ratio in deceleration period [%]</param>
        /// <param name="axis0_position">	: Target position of axis0(Target X coordinate)</param>
        /// <param name="axis1_position">	: Target position of axis1(Target Y coordinate)</param>
        /// <param name="cwCcw">			: Moving direction
        /// 1 : Clock wise
        /// 0 : Counter clock wise
        /// </param>
        /// <param name="radius">			: Radius							[um]</param>
        /// <param name="mode">				: Moving mode(enum eUniMotionMode). 
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// @ eUniApiReturnCode.InvalidTargetPosition		: The target position is invalid
        /// @ eUniApiReturnCode.RadiusTooSmall				: The radius is too small
        /// </returns>
        public virtual int MoveArcRadius(int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                int axis0_position, int axis1_position, int cwCcw, int radius, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveArcRadius(this.NetID, velocity, accel, decel, jerkAcc, jerkDec, axis0_position, axis1_position, cwCcw, radius, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move interpolation arc radius to device. " +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveArcRadius");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveArcRadius");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move 2 axes from center position using angle while moving the motion path in a arc
        /// </summary>
        /// <remarks>
        /// If jerk ratio is '0', motion profile is trapezoidal.
        /// If jerk ratio is valid, motion profile is s-curve.
        /// </remarks>
        /// <param name="velocity">			: Target velocity to move			[mm/min]</param>
        /// <param name="accel">			: Acceleration time					[msec]</param>
        /// <param name="decel">			: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">			: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">			: Jerk ratio in deceleration period [%]</param>
        /// <param name="centerX">			: Ceneter position of axis 0(Target X coordinate)</param>
        /// <param name="centerY">			: Ceneter position of axis 1(Target Y coordinate)</param>
        /// <param name="angle">			: Angle								[degree]</param>
        /// <param name="mode">				: Moving mode(enum eUniMotionMode). 
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveArcAngle(int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                int centerX, int centerY, int angle, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveArcAngle(this.NetID, velocity, accel, decel, jerkAcc, jerkDec, centerX, centerY, angle, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move interpolation arc angle to device. " +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveArcAngle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveArcAngle");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Move 2 axes from center position while moving the motion path in a circle
        /// </summary>
        /// <remarks>
        /// If jerk ratio is '0', motion profile is trapezoidal.
        /// If jerk ratio is valid, motion profile is s-curve.
        /// The argument 'angle' is over '0', after moving the motion path in the circle, move using angle while moving motion path in a arc
        /// </remarks>
        /// <param name="velocity">			: Target velocity to move			[mm/min]</param>
        /// <param name="accel">			: Acceleration time					[msec]</param>
        /// <param name="decel">			: Deceleration time					[msec]</param>
        /// <param name="jerkAcc">			: Jerk ratio in acceleration period [%]</param>
        /// <param name="jerkDec">			: Jerk ratio in deceleration period [%]</param>
        /// <param name="cwCcw">			: Moving direction
        /// 1 : Clock wise
        /// 0 : Counter clock wise
        /// </param>
        /// <param name="centerX">			: Ceneter position of axis 0(Target X coordinate)</param>
        /// <param name="centerY">			: Ceneter position of axis 1(Target Y coordinate)</param>
        /// <param name="rotateCount">		: Circle rotation count</param>
        /// <param name="angle">			: Angle after moving circle			[degree]</param>
        /// <param name="mode">				: Moving mode(enum eUniMotionMode). 
        /// 1: Incremetal mode
        /// 0: Absolute mode 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.NotDriveReady				: The drive is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(servo) is not on
        /// </returns>
        public virtual int MoveCircleAngle(int velocity, int accel, int decel, int jerkAcc, int jerkDec,
                                   int cwCcw, int centerX, int centerY, int rotateCount, int angle, int mode = (int)eUniMotionMode.Absolute)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniMoveCircleAngle(this.NetID, velocity, accel, decel, jerkAcc, jerkDec, cwCcw, centerX, centerY, rotateCount, angle, mode);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to move interpolation circle angle to device. " +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.MoveCircleAngle");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.MoveCircleAngle");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get velocity of another axis to syncronize axes which are moving the motion path in a circle
        /// </summary>
        /// <remarks>
        /// To use helical motion(3 axes motion), one axis motion is executed by getting the velocity of the another axis to be synchronized axes with the circular interpolation motion.
        /// </remarks>
        /// <param name="centerX">			: Ceneter position of axis 0(Target X coordinate) about the motion in a circle</param>
        /// <param name="centerY">			: Ceneter position of axis 1(Target Y coordinate) about the motion in a circle</param>
        /// <param name="rotateCount">		: Circle rotation count about the motion in a circle</param>
        /// <param name="angle">			: Angle after moving circle about the motion in a circle			[degree]</param>
        /// <param name="velocity">			: Target velocity about the motion in a circle			[mm/min]</param>
        /// <param name="position">			: Target position of another axis to syncronize axes which are moving the motion path in a circle</param>
        /// <param name="syncVelocity">		: Target velocity of another axis to syncronize axes which are moving the motion path in a circle</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetSyncVelocityFromCircleAngle(int centerX, int centerY, int rotateCount, int angle, int velocity,
                                                  int position, ref int syncVelocity)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetSyncVelocityFromCircleAngle(this.NetID, centerX, centerY, rotateCount, angle, velocity, position, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get velocity to sycronize another axis and interpolation circle. " +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSyncVelocityFromCircleAngle");
                }
                else
                {
                    syncVelocity = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSyncVelocityFromCircleAngle");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Add homing step to homing list
        /// </summary>
        /// <remarks>
        /// The homing operation consists of a set of steps(homing list). 
        /// When all the step motion in the list is completed, the home operation is completed.
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="stepNo">		: Step number to add list</param>
        /// <param name="velocity">		: Homing velocity 			[mm/min]</param>
        /// <param name="accel">		: Acceleration time			[msec]</param>
        /// <param name="decel">		: Deceleration time			[msec]</param>
        /// <param name="jerkAcc">		: Not used. insert value '0'</param>
        /// <param name="jerkDec">		: Not used. insert value '0'</param>
        /// <param name="direction">	: Moving direction.
        /// 1 : Positive(+) direction
        /// 0 : Negative(-) direction
        /// </param>
        /// <param name="point">		: Axis input number to set stop option(enum eUniAxisInputNumber).
        /// 0 : Input0(Limit(-) switch)
        /// 1 : Input1(Home switch)
        /// 2 : Input2(Limit(+) switch )
        /// 3 : Input3(User In) 
        /// </param>
        /// <param name="edge">			: The edge type to check input signal.
        /// 1 : Rising edge
        /// 0 : Falling edge
        /// </param>
        /// <param name="preCheck">		: The option to check whether the input signal has already been come in before motion is executed.
        /// 1 : If the input signal according to the arguments has been already come in, no execute the motion
        /// 0 : Not used the option
        /// </param>
        /// <param name="delay">		: Delay time after moving homing step [msec]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to add homing step
        /// @ eUniApiReturnCode.InvalidAddress				: The homing step number is invalid
        /// @ eUniApiReturnCode.InvalidDataCount			: Homing step list is full
        /// </returns>
        public virtual int AddHomingStep(int axisNo, int stepNo, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int direction, int point, int edge, int preCheck, int delay)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniAddHomingStep(this.NetID, axisNo, stepNo, velocity, accel, decel, jerkAcc, jerkDec, direction, point, edge, preCheck, delay);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to add homing step to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.AddHomingStep");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.AddHomingStep");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Remove specific homing step from homing list
        /// </summary>
        /// <remarks>
        /// If a specific homing step is removed in list, when the homing operation is started, the remaining steps except the step to be removed in list will execute.
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="stepNo">	: Step number to remove list</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to remove homing step
        /// @ eUniApiReturnCode.InvalidAddress				: The homing step number is invalid
        /// @ eUniApiReturnCode.InvalidDataCount			: Homing step list is empty
        /// </returns>
        public virtual int RemoveHomingStep(int axisNo, int stepNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniRemoveHomingStep(this.NetID, axisNo, stepNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to remove homing step from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.RemoveHomingStep");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.RemoveHomingStep");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Remove all homing steps from homing list
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to remove all homing steps
        /// </returns>
        public virtual int RemoveAllHomingStep(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniRemoveAllHomingStep(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to remove homing steps from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.RemoveAllHomingStep");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.RemoveAllHomingStep");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get a specific homing step information from homing list
        /// </summary>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="stepNo">		: Step number to get from list</param>
        /// <param name="velocity">		: Homing velocity 			[mm/min]</param>
        /// <param name="accel">		: Acceleration time			[msec]</param>
        /// <param name="decel">		: Deceleration time			[msec]</param>
        /// <param name="jerkAcc">		: Not used. insert value '0'</param>
        /// <param name="jerkDec">		: Not used. insert value '0'</param>
        /// <param name="direction">	: Moving direction.
        /// 1 : Positive(+) direction
        /// 0 : Negative(-) direction
        /// </param>
        /// <param name="point">		: Axis input number to set stop option(enum eUniAxisInputNumber).
        /// 0 : Input0(Limit(-) switch)
        /// 1 : Input1(Home switch)
        /// 2 : Input2(Limit(+) switch )
        /// 3 : Input3(User In) 
        /// </param>
        /// <param name="edge">			: The edge type to check input signal.
        /// 1 : Rising edge
        /// 0 : Falling edge
        /// </param>
        /// <param name="preCheck">		: The option to check whether the input signal has already been come in before motion is executed.
        /// 1 : If the input signal according to the arguments has been already come in, no execute the motion
        /// 0 : Not used the option
        /// </param>
        /// <param name="delay">		: Delay time after moving homing step [msec]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to get homing step
        /// @ eUniApiReturnCode.InvalidAddress				: The homing step number is invalid
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetHomingStep(int axisNo, int stepNo, ref int velocity, ref int accel, ref int decel, ref int jerkAcc, ref int jerkDec,
                                                            ref int direction, ref int point, ref int edge, ref int preCheck, ref int delay)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetHomingStep(this.NetID, axisNo, stepNo, out int velocityTemp, out int accelTemp, out int decelTemp, 
                    out int jerkAccTemp, out int jerkDecTemp, out int directionTemp, out int pointTemp, out int edgeTemp, out int preCheckTemp, out int delayTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get homing step from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetHomingStep");
                }
                else
                {
                    velocity = velocityTemp;
                    accel = accelTemp;
                    decel = decelTemp;
                    jerkAcc = jerkAccTemp;
                    jerkDec = jerkDecTemp;
                    direction = directionTemp;
                    point = pointTemp;
                    edge = edgeTemp;
                    preCheck = preCheckTemp;
                    delay = delayTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetHomingStep");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get the count of all homing steps from homing list
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="count">	: The count of homing steps</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetHomingStepCount(int axisNo, ref int count)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetHomingStepCount(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get homing step count from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetHomingStepCount");
                }
                else
                {
                    count = data;
                }
            }
            catch (Exception ex)
            {
                count = 0;
                Debug.WriteLine(ex.Message, "UniDevice.GetHomingStepCount");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set homing offset shift after moving homing steps
        /// </summary>
        /// <remarks>
        /// If the homing offset shift is activated, the homing shift which are moving to offset is executed after the all homing steps is completed.
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="use">			: Whether homing offset shift is enable or disable
        /// 1: Enable
        /// 0 : Disable
        /// </param>
        /// <param name="velocity">		: Homing velocity 			[mm/min]</param>
        /// <param name="accel">		: Acceleration time			[msec]</param>
        /// <param name="decel">		: Deceleration time			[msec]</param>
        /// <param name="jerkAcc">		: Not used. insert value '0'</param>
        /// <param name="jerkDec">		: Not used. insert value '0'</param>
        /// <param name="position">		: Offset(target) position to move after moving homing steps</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to set homing offset shift
        /// </returns>
        public virtual int SetHomingShift(int axisNo, bool use, int velocity, int accel, int decel, int jerkAcc, int jerkDec, int position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int useData = 0;

            try
            {
                if (use) useData = 1;
                else useData = 0;

                returnCode = eUniSetHomingShift(this.NetID, axisNo, useData, velocity, accel, decel, jerkAcc, jerkDec, position);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set homing offset shift to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetHomingShift");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetHomingShift");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get homing offset shift information
        /// </summary>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="use">			: Whether homing offset shift is enable or disable
        /// 1: Enable
        /// 0 : Disable
        /// </param>
        /// <param name="velocity">		: Homing velocity 			[mm/min]</param>
        /// <param name="accel">		: Acceleration time			[msec]</param>
        /// <param name="decel">		: Deceleration time			[msec]</param>
        /// <param name="jerkAcc">		: Not used. insert value '0'</param>
        /// <param name="jerkDec">		: Not used. insert value '0'</param>
        /// <param name="position">		: Offset(target) position to move after moving homing steps</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to get homing offset shift
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetHomingShift(int axisNo, ref bool use, ref int velocity, ref int accel, ref int decel, ref int jerkAcc, ref int jerkDec, ref int position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetHomingShift(this.NetID, axisNo, out int useTemp,
                    out int velocityTemp, out int accelTemp, out int decelTemp, out int jerkAccTemp, out int jerkDecTemp, out int positionTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get homing offset shift from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetHomingShift");
                }
                else
                {
                    if (useTemp > 0) use = true;
                    else use = false;
                    velocity = velocityTemp;
                    accel = accelTemp;
                    decel = decelTemp;
                    jerkAcc = jerkAccTemp;
                    jerkDec = jerkDecTemp;
                    position = positionTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetHomingShift");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Start homing operation
        /// </summary>
        /// <remarks>
        /// The homing operation is executed according to the set homing steps and homing offset motion.
        /// If the function is called immediately after executing a specific homing method('StartHomingMethod'), the specific homing method is executed.
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="homePosition"> : The position to be set to coordinate after homing operation is completed</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: Invalid axis number to start homing operation
        /// @ eUniApiReturnCode.InvalidDataCount			: Homing step list is empty
        /// @ eUniApiReturnCode.AlreadyOperation			: The homing operation is already started
        /// @ eUniApiReturnCode.NotDriveReady				: The drive(axis) is not ready
        /// @ eUniApiReturnCode.NotServoOn					: The motor(axis) is not on
        /// @ eUniApiReturnCode.FailedOperation				: Failed to start the homing operation
        /// </returns>
        public virtual int StartHoming(int axisNo, int homePosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniStartHoming(this.NetID, axisNo, homePosition);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to start homing to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StartHoming");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StartHoming");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Start homing operation using homing method
        /// </summary>
        /// <remarks>
        /// The homing method is implemented as defined in CIA402 drive profile - homing mode.
        /// See the CIA402 drive profile related manual or the UNI drive manual.
        /// </remarks>
        /// <param name="axisNo">			: Axis number</param>
        /// <param name="methodNo">			: Homing method number</param>
        /// <param name="switchVelocity">	: The velocity until detecting switch</param>
        /// <param name="switchAccel">		: The acceleration time until detecting switch</param>
        /// <param name="switchDecel">		: The deceleration time until detecting switch</param>
        /// <param name="homingVelocity">	: The velocity until reaching home</param>
        /// <param name="homingAccel">		: The acceleration time until reaching home</param>
        /// <param name="homingDecel">		: The deceleration time until reaching home</param>
        /// <param name="offsetPosition">	: The offset to set or to move position after homing</param>
        /// <param name="useMoveOffset">	: Whether moving to offset(position) is enable or disable. If no use moving offset, the value to set home position</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: Invalid axis number to start homing operation
        /// @ eUniApiReturnCode.AlreadyOperation			: The homing operation is already started
        /// @ eUniApiReturnCode.FailedOperation				: Failed to start the homing operation
        /// @ eUniApiReturnCode.NotSet						: No set limit switch setting(parameter)
        /// @ eUniApiReturnCode.AlreadySet					: Already comes in input(switch) signal
        /// @ eUniApiReturnCode.InvalidAddress				: The homing method number is invalid
        /// </returns>
        public virtual int StartHomingMethod(int axisNo, int methodNo, int switchVelocity, int switchAccel, int switchDecel, int homingVelocity, int homingAccel, int homingDecel, int offsetPosition, bool useMoveOffset)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int useMove = 0;

            try
            {
                if (useMoveOffset) useMove = 1;
                else useMove = 0;

                returnCode = eUniStartHomingMethod(this.NetID, axisNo, methodNo, switchVelocity, switchAccel, switchDecel, homingVelocity, homingAccel, homingDecel, offsetPosition, useMove);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to start homing method to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StartHomingMethod");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StartHomingMethod");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Stop homing operation
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.NotOperation				: The homing operation is not started
        /// @ eUniApiReturnCode.FailedOperation				: Failed to get homing offset motion
        /// </returns>
        public virtual int StopHoming(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniStopHoming(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to stop homing to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StopHoming");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StopHoming");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get homing status(flag)
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="isHoming"> : Whether homing operation is running or not running.
        /// 1 : Running
        /// 0 : None
        /// </param>
        /// <param name="isStep">	: Whether homing step motion is running or not running.
        /// 1 : Running
        /// 0 : None
        /// </param>
        /// <param name="isOffset"> : Whether homing offset motion is running or not running.
        /// 1 : Running
        /// 0 : None
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to get homing status
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int IsHoming(int axisNo, ref bool isHoming, ref bool isStep, ref bool isOffset)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniIsHoming(this.NetID, axisNo, out int homing, out int step, out int offset);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get to be going home from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.IsHoming");
                }
                else
                {
                    if (homing > 0) isHoming = true;
                    else isHoming = false;
                    if (step > 0) isStep = true;
                    else isStep = false;
                    if (offset > 0) isOffset = true;
                    else isOffset = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.IsHoming");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get homing operation information
        /// </summary>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="stepNo">		: Current homing step number to run</param>
        /// <param name="rate">			: Current homing rate</param>
        /// <param name="statusCode">	: Current homing status(error) code</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to get homing status
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetHomingResult(int axisNo, ref int stepNo, ref int rate, ref int statusCode)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetHomingResult(this.NetID, axisNo, out int stepNoTemp, out int rateTemp, out int statusCodeTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get homing result from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetHomingResult");
                }
                else
                {
                    stepNo = stepNoTemp;
                    rate = rateTemp;
                    statusCode = statusCodeTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetHomingResult");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get homing operation extension information
        /// </summary>
        /// <remarks>
        /// The API is extension API about 'GetHomingResult'
        /// </remarks>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="stepNo">		: Current homing step number to run</param>
        /// <param name="rate">			: Current homing rate</param>
        /// <param name="statusCode">	: Current homing status(error) code</param>
        /// <param name="status">		: Current homing flag.
        /// # Bit 0: Whether the total homing sequence is running or not running
        /// # Bit 1: Whether the total homing sequence is done or not done
        /// # Bit 2: Whether the homing error is occured or not occured
        /// # Bit 3: Whether the homing step motion is running or not running
        /// # Bit 4: Whether the all homing steps are done or not done
        /// # Bit 5: Whether the homing reverse step motion is enable or disable
        /// # Bit 6: Whether the homing offset motion is running or not running
        /// # Bit 7: Whether the homing offset motion are done or not done
        /// # Bit 8: Whether setting home position is running or not running
        /// # Bit 9~31: Reserved
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.InvalidAxisNumber			: The axis number is invalid
        /// @ eUniApiReturnCode.FailedOperation				: Failed to get homing status
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetHomingResult(int axisNo, ref int stepNo, ref int rate, ref int statusCode, ref int status)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetHomingResultEx(this.NetID, axisNo, out int stepNoTemp, out int rateTemp, out int statusCodeTemp, out int statusTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get homing result from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetHomingResult");
                }
                else
                {
                    stepNo = stepNoTemp;
                    rate = rateTemp;
                    statusCode = statusCodeTemp;
                    status = statusTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetHomingResult");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set in-position pulse range setting
        /// </summary>
        /// <remarks>
        /// * Pulse range : The range of error pulse count.
        /// * Error pulse count : The delta pulse between command pulse and actual pulse.
        /// The value doesn't set to parameter.
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="pulse">	: Range of pulse count</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetInpositionPulseRange(int axisNo, int pulse)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetInpositionPulseRange(this.NetID, axisNo, pulse);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set in-position pulse range to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", pulse count: " + pulse.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetInpositionPulseRange");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetInpositionPulseRange");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get in-position pulse range setting
        /// </summary>
        /// <remarks>
        /// * Pulse range : The range of error pulse count.
        /// * Error pulse count : The delta pulse between command pulse and actual pulse.
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="pulse">	: Range of pulse count</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetInpositionPulseRange(int axisNo, ref int pulse)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetInpositionPulseRange(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get in-position pulse range from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetInpositionPulseRange");
                }
                else
                {
                    pulse = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetInpositionPulseRange");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set in-position duration time
        /// </summary>
        /// <remarks>
        /// If the moving exists in the duration after motion, the axis is not completed about in-position.
        /// The value doesn't set to parameter.
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="time">		: Duration time	[msec]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetInpositionDuration(int axisNo, int time)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetInpositionDuration(this.NetID, axisNo, time);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set in-position duration to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", pulse count: " + time.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetInpositionDuration");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetInpositionDuration");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get in-position duration time
        /// </summary>
        /// <remarks>
        /// If the moving exists in the duration after motion, the axis is not completed about in-position
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="time">		: Duration time	[msec]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetInpositionDuration(int axisNo, ref int time)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetInpositionDuration(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get in-position duration from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetInpositionDuration");
                }
                else
                {
                    time = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetInpositionDuration");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set in-position timeout
        /// </summary>
        /// <remarks>
        /// If the moving is timeout after motion, the axis is aborted about in-position.
        /// The value doesn't set to parameter.
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="time">		: Timeout	[msec]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetInpositionTimeout(int axisNo, int time)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetInpositionTimeout(this.NetID, axisNo, time);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get in-position duration from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetInpositionTimeout");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetInpositionTimeout");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get in-position timeout
        /// </summary>
        /// <remarks>
        /// If the moving is timeout after motion, the axis is aborted about in-position
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="time">		: Timeout	[msec]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetInpositionTimeout(int axisNo, ref int time)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetInpositionTimeout(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get in-position timeout from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetInpositionTimeout");
                }
                else
                {
                    time = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetInpositionTimeout");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set software position limit value
        /// </summary>
        /// <remarks>
        /// The limit values doesn't set to parameter.
        /// </remarks>
        /// <param name="axisNo">			: Axis number</param>
        /// <param name="negativePosition"> : Negative(-) position limit value [um]</param>
        /// <param name="positivePosition"> : Positive(+) position limit value [um]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetSoftwarePositionLimit(int axisNo, int negativePosition, int positivePosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetSoftwarePositionLimit(this.NetID, axisNo, negativePosition, positivePosition);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set software position limit to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", negative position: " + negativePosition.ToString() +
                            ", positive position: " + positivePosition.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetSoftwarePositionLimit");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetSoftwarePositionLimit");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get software position limit value
        /// </summary>
        /// <param name="axisNo">			: Axis number</param>
        /// <param name="negativePosition"> : Negative(-) position limit value [um]</param>
        /// <param name="positivePosition"> : Positive(+) position limit value [um]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetSoftwarePositionLimit(int axisNo, ref int negativePosition, ref int positivePosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetSoftwarePositionLimit(this.NetID, axisNo, out int negativePositionTemp, out int positivePositionTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get software position limit from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSoftwarePositionLimit");
                }
                else
                {
                    negativePosition = negativePositionTemp;
                    positivePosition = positivePositionTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSoftwarePositionLimit");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Start to latch first position(start to capture first encoder pulse count)
        /// </summary>
        /// <remarks>
        /// The current latched position can be read without calling the function.
        /// This function is only started to latch first position after calling this function.
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="edge">		: Strobe(input3) latch edge type. 
        /// 0: Rising edge
        /// 1: Falling edge
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int StartLatchPosition(int axisNo, int edge)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniStartLatchPosition(this.NetID, axisNo, edge);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to start latch position to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StartLatchPosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StartLatchPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Stop to latch first position(stop capture first encoder pulse count)
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int StopLatchPosition(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniStopLatchPosition(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to stop latch position to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StopLatchPosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StopLatchPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get latched position(captured encoder pulse count)
        /// </summary>
        /// <param name="axisNo">			: Axis number</param>
        /// <param name="position">			: The position of being last latched currently
        /// * Index 0: Motor index(z-phase) latched position
        /// * Index 1 : Strobe(input3) latched position
        /// </param>
        /// <param name="firstPosition">	: The position of being first latched after starting to latch position
        /// * Index 0: Motor index(z-phase) latched position
        /// * Index 1 : Strobe(input3) latched position
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetLatchPosition(int axisNo, int[] position, int[] firstPosition)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (position.Length < 2)
                return (int)eUniApiReturnCode.NoData;

            if (firstPosition.Length < 2)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetLatchPosition(this.NetID, axisNo, out position[0], out firstPosition[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get latched position from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetLatchPosition");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetLatchPosition");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set on/off to output in axis
        /// </summary>
        /// <remarks>
        /// Currently UNI drive axis have 2 output
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="outputNo">	: Output number</param>
        /// <param name="onOff">	: On: 1, Off: 0</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetOutput(int axisNo, int outputNo, int onOff)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniSetOutput(this.NetID, axisNo, outputNo, onOff);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set output data to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", ON/OFF: " + onOff.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetOutput");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetOutput");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get on/off from input, output in axis
        /// </summary>
        /// <remarks>
        /// Currently UNI drive axis have 4 input, 2 output
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="input">	: Input point data
        /// * Index 0 : Input 0 On / Off(On : 1, Off : 0)
        /// * Index 1 : Input 1 On / Off(On : 1, Off : 0)
        /// * Index 2 : Input 2 On / Off(On : 1, Off : 0)
        /// * Index 3 : Input 3 On / Off(On : 1, Off : 0) 
        /// </param>
        /// <param name="output">	: Output point data
        /// * Index 0 : Output 0 On / Off(On : 1, Off : 0)
        /// * Index 1 : Output 1 On / Off(On : 1, Off : 0) 
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetIo(int axisNo, int[] input, int[] output)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            if (input.Length < 32)
                return (int)eUniApiReturnCode.NoData;

            if (output.Length < 32)
                return (int)eUniApiReturnCode.NoData;

            try
            {
                returnCode = eUniGetIo(this.NetID, axisNo, out input[0], out output[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get I/O data from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetIoData");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetIoData");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get signal from input in axis
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="limitN">	: Negative(-) limit switch(Input 0)		[On: 1, Off: 0]</param>
        /// <param name="limitP">	: Positive(+) limit switch(Input 2)		[On: 1, Off: 0]</param>
        /// <param name="home">		: Home switch(Input 1)					[On: 1, Off: 0]</param>
        /// <param name="alarm">	: Servo(Drive) alarm occured			[On: 1, Off: 0]</param>
        /// <param name="ready">	: Servo(Motor) ready					[On: 1, Off: 0]</param>
        /// <param name="on">		: Servo(Motor) On/Off					[On: 1, Off: 0]</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetSignalStatus(int axisNo, ref bool limitN, ref bool limitP, ref bool home, ref bool alarm, ref bool ready, ref bool on)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetSignalStatus(this.NetID, axisNo, out int limitNTemp, out int limitPTemp, out int homeTemp, out int alarmTemp, out int readyTemp, out int onTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get input signal from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSignalStatus");
                }
                else
                {
                    if (limitNTemp > 0) limitN = true;
                    else limitN = false;
                    if (limitPTemp > 0) limitP = true;
                    else limitP = false;
                    if (homeTemp > 0) home = true;
                    else home = false;
                    if (alarmTemp > 0) alarm = true;
                    else alarm = false;
                    if (readyTemp > 0) ready = true;
                    else ready = false;
                    if (onTemp > 0) on = true;
                    else on = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSignalStatus");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set enable or disable to tigger
        /// </summary>
        /// <remarks>
        /// The trigger does use output 0 in axis
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="enable">	: Whether the trigger is enable or disable</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetTriggerOn(int axisNo, bool enable)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int data = 0;

            try
            {
                if (enable)
                    data = 1;
                else
                    data = 0;

                returnCode = eUniSetTriggerOn(this.NetID, axisNo, data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set trigger ON / OFF data to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Enable: " + enable.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSignalStatus");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSignalStatus");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get enable or disable from trigger
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="enable">	: Whether the trigger is enable or disable</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetTriggerOn(int axisNo, ref bool enable)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetTriggerOn(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get trigger ON / OFF data from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetSignalStatus");
                }
                else
                {
                    if (data > 0)
                        enable = true;
                    else
                        enable = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetSignalStatus");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get trigger count
        /// </summary>
        /// <remarks>
        /// The condition of triggering pulse count is set by parameter
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="count">	: The count of triggering output</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetTriggerCount(int axisNo, ref int count)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetTriggerCount(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get trigger pulse count from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetTriggerCount");
                }
                else
                {
                    count = data;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetTriggerCount");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Reset trigger count
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int ResetTrigger(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniResetTrigger(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to reset to trigger" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.ResetTrigger");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.ResetTrigger");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Set loopback mode
        /// </summary>
        /// <remarks>
        /// If the loopback mode is set, command position pass to actual position 
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="enable">	: Whether loopback mode is enable or disable</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int SetLoopback(int axisNo, bool enable)
        {
            int returnCode = (int)eUniApiReturnCode.Success;
            int data = 0;

            try
            {
                if (enable)
                    data = 1;
                else
                    data = 0;

                returnCode = eUniSetLoopback(this.NetID, axisNo, data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to set loopback mode to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() +
                            ", Enable: " + enable.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.SetLoopback");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.SetLoopback");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get loopback mode
        /// </summary>
        /// <remarks>
        /// If the loopback mode is set, command position pass to actual position 
        /// </remarks>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="enable">	: Whether loopback mode is enable or disable</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetLoopback(int axisNo, ref bool enable)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetLoopback(this.NetID, axisNo, out int data);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get loopback mode from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetLoopback");
                }
                else
                {
                    if (data > 0)
                        enable = true;
                    else
                        enable = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetLoopback");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Start to monitor(record) motion profile
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <param name="interval"> : Monitoring inverval time		[msec]</param>
        /// <param name="type">		: The source type of data to monitor(enum eUniMonitoringSource). 
        /// 0: Position
        /// 2: Velocity
        /// 3: Acceleration
        /// </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int StartMonitoring(int axisNo, int interval, int type = (int)eUniMonitoringSource.Position)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniStartMonitoring(this.NetID, axisNo, interval, type);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to start monitoring to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StartMonitoring");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StartMonitoring");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Stop to monitor(record) motion profile
        /// </summary>
        /// <param name="axisNo">	: Axis number</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// </returns>
        public virtual int StopMonitoring(int axisNo)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniStopMonitoring(this.NetID, axisNo);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to stop monitoring to device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.StopMonitoring");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.StopMonitoring");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get monitoring(recording) information about motion profile
        /// </summary>
        /// <param name="axisNo">		: Axis number</param>
        /// <param name="command">		: Current monitoring command. 0: Stop, 1: Start</param>
        /// <param name="interval">		: Current monitoring interval time</param>
        /// <param name="dataIndex">	: Current monitoring data index</param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMonitoringInfo(ref int axisNo, ref int command, ref int interval, ref int dataIndex)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetMonitoringInfo(this.NetID, out int axisNoTemp, out int commandTemp, out int intervalTemp, out int dataIndexTemp);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get monitoring infomation from device" +
                            "(Network ID: " + this.NetID.ToString() +
                            ", Axis number: " + axisNo.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetMonitoringInfo");
                }
                else
                {
                    axisNo = axisNoTemp;
                    command = commandTemp;
                    interval = intervalTemp;
                    dataIndex = dataIndexTemp;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetMonitoringInfo");
                returnCode = -1;
            }

            return returnCode;
        }

        /// <summary>
        /// Get motion profile data to be monitored(recorded)
        /// </summary>
        /// <remarks>
        /// The max count of motion profile data to be monitored(recorded) is 1500
        /// </remarks>
        /// <param name="dataIndex">	: Data index to get motion profile to be recorded</param>
        /// <param name="dataCount">	: Data count to get motion profile to be recorded</param>
        /// <param name="commandData">	: Data buffer to be stored command profile data to be recoreded</param>
        /// <param name="actualData">	: Data buffer to be stored actual profile data to be recoreded</param>
        /// <param name="status">		: Data buffer to be stored status about motion profile.
        /// # Bit 0: Moving					(1: Moving, 0 : None)
        /// # Bit 1: Profiling				(1: Profiling, 0 : None)
        /// # Bit 2: Inposition Completed	(1: Completed, 0 : None)
        /// # Bit 3: Inposition Timeout		(1: Timeout, 0 : None) </param>
        /// <returns>
        /// API Return Code(enum eUniReturnCode).
        /// @ eUniApiReturnCode.Success						: Operation Success 
        /// @ eUniApiReturnCode.NotConnected				: Not connected to device
        /// @ eUniApiReturnCode.Disconnecting				: Currently disconnecting state
        /// @ eUniApiReturnCode.FailedCommunication			: Failed to communicate to device
        /// @ eUniApiReturnCode.InvalidMessage				: The communication message to be received from device is invalid data
        /// @ eUniApiReturnCode.InvalidSequenceNumber		: The communication message to be received from device is invalid sequence
        /// @ eUniApiReturnCode.InvalidCommunicationFormat	: The communication message to be received from device is invalid command format
        /// @ eUniApiReturnCode.ArgumentNullPointer			: The argument is null
        /// </returns>
        public virtual int GetMonitoringData(int dataIndex, int dataCount, int[] commandData, int[] actualData, int[] status)
        {
            int returnCode = (int)eUniApiReturnCode.Success;

            try
            {
                returnCode = eUniGetMonitoringData(this.NetID, dataIndex, dataCount, out commandData[0], out actualData[0], out status[0]);
                if (returnCode != (int)eUniApiReturnCode.Success)
                {
                    Debug.WriteLine("Failed to get monitoring data from device" +
                            "(Network ID: " + this.NetID.ToString() + "). " +
                            "error : " + returnCode,
                            "UniDevice.GetMonitoringData");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "UniDevice.GetMonitoringData");
                returnCode = -1;
            }

            return returnCode;
        }

        #endregion

    }
}
