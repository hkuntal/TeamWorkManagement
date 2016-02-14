using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;

namespace WindowsService1
{
    public partial class GetSessionName : ServiceBase
    {
        static Timer _timer;
        static string fileName = Path.Combine(@"C:\Hariom", "SessionName_" + DateTime.Now.ToShortTimeString().Replace(":", "-").Replace(" ", "") + ".txt");

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);


        #region Constants

        public const int WTS_CURRENT_SESSION = -1;

        #endregion

        #region Dll Imports

        [DllImport("wtsapi32.dll")]
        private static extern int WTSEnumerateSessions(
            IntPtr pServer,
            [MarshalAs(UnmanagedType.U4)] int iReserved,
            [MarshalAs(UnmanagedType.U4)] int iVersion,
            ref IntPtr pSessionInfo,
            [MarshalAs(UnmanagedType.U4)] ref int iCount);

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSQuerySessionInformation(
            System.IntPtr pServer,
            int iSessionID,
            WTS_INFO_CLASS oInfoClass,
            out System.IntPtr pBuffer,
            out uint iBytesReturned);

        [DllImport("wtsapi32.dll")]
        private static extern void WTSFreeMemory(
            IntPtr pMemory);

        #endregion

        #region Structures

        //Structure for Terminal Service Client IP Address
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_CLIENT_ADDRESS
        {
            public int iAddressFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] bAddress;
        }

        //Structure for Terminal Service Session Info
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public int iSessionID;
            [MarshalAs(UnmanagedType.LPStr)]
            public string sWinsWorkstationName;
            public WTS_CONNECTSTATE_CLASS oState;
        }

        //Structure for Terminal Service Session Client Display
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_CLIENT_DISPLAY
        {
            public int iHorizontalResolution;
            public int iVerticalResolution;
            //1 = The display uses 4 bits per pixel for a maximum of 16 colors.
            //2 = The display uses 8 bits per pixel for a maximum of 256 colors.
            //4 = The display uses 16 bits per pixel for a maximum of 2^16 colors.
            //8 = The display uses 3-byte RGB values for a maximum of 2^24 colors.
            //16 = The display uses 15 bits per pixel for a maximum of 2^15 colors.
            public int iColorDepth;
        }

        #endregion

        #region Enumurations

        public enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        public enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        }

        #endregion

        # region For service control manager
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };
        #endregion

        public GetSessionName()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 10000000000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        // http://stackoverflow.com/questions/3446507/the-service-is-not-responding-to-the-control-function-error-2186
        // https://msdn.microsoft.com/en-us/library/zt39148a.aspx
           
            LogTheCurrentSessionReoccuringly();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            using (var sw = new StreamWriter(@"c:\hariom\sessionName.txt", true))
            {
                sw.WriteLine("Service Started at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
            }
        }

        private static void LogTheCurrentSessionReoccuringly()
        {
            // From System.Timers

            _timer = new Timer(3000); // Set up the timer for 3 seconds
            //
            // Type "_timer.Elapsed += " and press tab twice.
            //
            // Create a file

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true; // Enable it
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GetAndLogCurrentSession();
        }

        private static void GetAndLogCurrentSession()
        {
            using (var sw = new StreamWriter(fileName, true))
            {
                Console.WriteLine("This method called at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                sw.WriteLine("This method called at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));

                Console.WriteLine("Session Name Env Var = " + Environment.GetEnvironmentVariable("SessionName"));
                sw.WriteLine("Session Name Env Var = " + Environment.GetEnvironmentVariable("SessionName"));

                // Console.WriteLine("------------------FROM WIN32 API--------------------");
                var session = GetCurrentSession();
                Console.WriteLine("Session Name Win32 App = " + session);
                sw.WriteLine("Session Name Win32 App = " + session);

                Console.WriteLine("------------------------------------------");
                sw.WriteLine("------------------------------------------");
            }
        }

        private static string GetCurrentSession()
        {
            Console.WriteLine("------------------FROM WIN32 API--------------------");

            const int WTS_CURRENT_SERVER_HANDLE = -1;
            IntPtr currentSession = IntPtr.Zero;
            IntPtr currentServer = IntPtr.Zero;
            string sUserName = string.Empty;
            string sDomain = string.Empty;
            string sessionType = string.Empty;

            IntPtr pAddress = IntPtr.Zero;
            uint iReturned = 0;
            WTS_CLIENT_ADDRESS oClientAddres = new WTS_CLIENT_ADDRESS();
            string sIPAddress = string.Empty;

            //WTS_SESSION_INFO oSessionInfo =
            //            (WTS_SESSION_INFO)Marshal.PtrToStructure((System.IntPtr)iCurrent,
            //                                                      typeof(WTS_SESSION_INFO));

           // if (WTSQuerySessionInformation(currentServer, WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSClientAddress,
           //                                        out pAddress, out iReturned) == true)
           // {
           //     oClientAddres = (WTS_CLIENT_ADDRESS)Marshal.PtrToStructure
           //                                              (pAddress, oClientAddres.GetType());
           //     sIPAddress = oClientAddres.bAddress[2] + "." +
           //                  oClientAddres.bAddress[3] + "." + oClientAddres.bAddress[4]
           //                  + "." + oClientAddres.bAddress[5];

           //     Console.WriteLine("IpAddress: " + sIPAddress);
           // }
           // //Get the User Name of the Terminal Services User
           // if (WTSQuerySessionInformation(currentServer,
           //                                WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSUserName,
           //                                out pAddress, out iReturned) == true)
           // {
           //     sUserName = Marshal.PtrToStringAnsi(pAddress);
           //     Console.WriteLine("User name: " + sUserName);
           // }

           // //Get the Domain Name of the Terminal Services User
           // if (WTSQuerySessionInformation(currentServer,
           //                                WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSDomainName,
           //                                out pAddress, out iReturned) == true)
           // {
           //     sDomain = Marshal.PtrToStringAnsi(pAddress);
           //     Console.WriteLine("Domain Name: " + sDomain);
           // }

           ////Get the session Name of the Terminal Services User
           // if (WTSQuerySessionInformation(currentServer,
           //                                WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSSessionId,
           //                                out pAddress, out iReturned) == true)
           // {
           //     var sessionId = Marshal.ReadIntPtr(pAddress);
           //     Console.WriteLine("Session Id: " + sessionId);
           // }

            //Get the session Name of the Terminal Services User
            if (WTSQuerySessionInformation(currentServer,
                                           WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSWinStationName,
                                           out pAddress, out iReturned) == true)
            {
                sessionType = Marshal.PtrToStringAnsi(pAddress);
                Console.WriteLine("Session Type: " + sessionType);
            }

            Console.WriteLine("------------------FROM WIN32 API--------------------");
            return sessionType;
        }
    }
}
