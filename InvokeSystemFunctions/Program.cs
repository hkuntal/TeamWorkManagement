using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace InvokeSystemFunctions
{
    internal class Program
    {
         static Timer _timer;
         static string fileName = Path.Combine(@"C:\Hariom", "SessionName_" + DateTime.Now.ToShortTimeString().Replace(":", "-").Replace(" ", "") + ".txt");

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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] bAddress;
        }

        //Structure for Terminal Service Session Info
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public int iSessionID;
            [MarshalAs(UnmanagedType.LPStr)] public string sWinsWorkstationName;
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

        private static void Main(string[] args)
        {
            // If you want to convert it into a CONSOLE APP without a no logon window
            // GetAllSessions();
            
            // GetCurrentSession();

            // Get current session name through the command process
            //GetCurrentSessionThroughCommandProcess();


            // GetcurrentSessionThroughEnvVariable();

            LogTheCurrentSessionReoccuringly();
            //Console.ReadLine();
            new ManualResetEvent(false).WaitOne();
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
                Console.WriteLine("Console App Started at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                sw.WriteLine("Console App Started at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));

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

        private static void GetcurrentSessionThroughEnvVariable()
        {
            Console.WriteLine("------------------FROM ENVIRONMENT VARIABLE--------------------");
            Console.WriteLine("Session Name = " + Environment.GetEnvironmentVariable("SessionName"));
            Console.WriteLine("User Name = " + Environment.UserName);
            Console.WriteLine("-----------------------------------------");
        }

        private static void GetCurrentSessionThroughCommandProcess()
        {
            //string cmd = "echo %sessionname%";
            //var proc = new Process
            //    {
            //        StartInfo =
            //            {
            //                FileName = "cmd.exe",
            //                Arguments = cmd,
            //                UseShellExecute = false,
            //                RedirectStandardOutput = true
            //            }
            //    };
            //proc.Start();

            ProcessStartInfo startInfo = new ProcessStartInfo("cmd")
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                Arguments = "echo %sessionname%"
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            //process.StandardInput.WriteLine("echo %sessionname%");
            //process.StandardInput.WriteLine("exit");
            var output = process.StandardOutput.ReadToEnd();
            process.Dispose();

            //var output = proc.StandardOutput.ReadLine();
            //proc.WaitForExit();

            Console.WriteLine("The session name is: " + output);
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

            if (WTSQuerySessionInformation(currentServer, WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSClientAddress,
                                                   out pAddress, out iReturned) == true)
            {
                oClientAddres = (WTS_CLIENT_ADDRESS)Marshal.PtrToStructure
                                                         (pAddress, oClientAddres.GetType());
                sIPAddress = oClientAddres.bAddress[2] + "." +
                             oClientAddres.bAddress[3] + "." + oClientAddres.bAddress[4]
                             + "." + oClientAddres.bAddress[5];

                Console.WriteLine("IpAddress: " + sIPAddress);
            }
            //Get the User Name of the Terminal Services User
            if (WTSQuerySessionInformation(currentServer,
                                           WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSUserName,
                                           out pAddress, out iReturned) == true)
            {
                sUserName = Marshal.PtrToStringAnsi(pAddress);
                Console.WriteLine("User name: " + sUserName);
            }

            //Get the Domain Name of the Terminal Services User
            if (WTSQuerySessionInformation(currentServer,
                                           WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSDomainName,
                                           out pAddress, out iReturned) == true)
            {
                sDomain = Marshal.PtrToStringAnsi(pAddress);
                Console.WriteLine("Domain Name: "+sDomain);
            }

            //Get the session Name of the Terminal Services User
            if (WTSQuerySessionInformation(currentServer,
                                           WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSWinStationName,
                                           out pAddress, out iReturned) == true)
            {
                sessionType = Marshal.PtrToStringAnsi(pAddress);
                Console.WriteLine("Session Type: " + sessionType);
            }

            //Get the session Name of the Terminal Services User
            if (WTSQuerySessionInformation(currentServer,
                                           WTS_CURRENT_SERVER_HANDLE, WTS_INFO_CLASS.WTSSessionId,
                                           out pAddress, out iReturned) == true)
            {
                var sessionId = Marshal.ReadIntPtr(pAddress);
                Console.WriteLine("Session Id: " + sessionId);
            }

            Console.WriteLine("------------------FROM WIN32 API--------------------");
            return sessionType;
        }
        private static void GetAllSessions()
        {
            IntPtr pServer = IntPtr.Zero;
            string sUserName = string.Empty;
            string sDomain = string.Empty;
            string sClientApplicationDirectory = string.Empty;
            string sIPAddress = string.Empty;

            WTS_CLIENT_ADDRESS oClientAddres = new WTS_CLIENT_ADDRESS();
            WTS_CLIENT_DISPLAY oClientDisplay = new WTS_CLIENT_DISPLAY();

            IntPtr pSessionInfo = IntPtr.Zero;

            int iCount = 0;
            int iReturnValue = WTSEnumerateSessions
                (pServer, 0, 1, ref pSessionInfo, ref iCount);
            int iDataSize = Marshal.SizeOf(typeof (WTS_SESSION_INFO));

            int iCurrent = (int) pSessionInfo;

            if (iReturnValue != 0)
            {
                //Go to all sessions
                for (int i = 0; i < iCount; i++)
                {
                    WTS_SESSION_INFO oSessionInfo =
                        (WTS_SESSION_INFO) Marshal.PtrToStructure((System.IntPtr) iCurrent,
                                                                  typeof (WTS_SESSION_INFO));
                    iCurrent += iDataSize;

                    uint iReturned = 0;

                    //Get the IP address of the Terminal Services User
                    IntPtr pAddress = IntPtr.Zero;
                    if (WTSQuerySessionInformation(pServer,
                                                   oSessionInfo.iSessionID, WTS_INFO_CLASS.WTSClientAddress,
                                                   out pAddress, out iReturned) == true)
                    {
                        oClientAddres = (WTS_CLIENT_ADDRESS) Marshal.PtrToStructure
                                                                 (pAddress, oClientAddres.GetType());
                        sIPAddress = oClientAddres.bAddress[2] + "." +
                                     oClientAddres.bAddress[3] + "." + oClientAddres.bAddress[4]
                                     + "." + oClientAddres.bAddress[5];
                    }
                    //Get the User Name of the Terminal Services User
                    if (WTSQuerySessionInformation(pServer,
                                                   oSessionInfo.iSessionID, WTS_INFO_CLASS.WTSUserName,
                                                   out pAddress, out iReturned) == true)
                    {
                        sUserName = Marshal.PtrToStringAnsi(pAddress);
                    }
                    //Get the Domain Name of the Terminal Services User
                    if (WTSQuerySessionInformation(pServer,
                                                   oSessionInfo.iSessionID, WTS_INFO_CLASS.WTSDomainName,
                                                   out pAddress, out iReturned) == true)
                    {
                        sDomain = Marshal.PtrToStringAnsi(pAddress);
                    }
                    //Get the Display Information  of the Terminal Services User
                    if (WTSQuerySessionInformation(pServer,
                                                   oSessionInfo.iSessionID, WTS_INFO_CLASS.WTSClientDisplay,
                                                   out pAddress, out iReturned) == true)
                    {
                        oClientDisplay = (WTS_CLIENT_DISPLAY) Marshal.PtrToStructure
                                                                  (pAddress, oClientDisplay.GetType());
                    }
                    //Get the Application Directory of the Terminal Services User
                    if (WTSQuerySessionInformation(pServer, oSessionInfo.iSessionID,
                                                   WTS_INFO_CLASS.WTSClientDirectory, out pAddress, out iReturned) ==
                        true)
                    {
                        sClientApplicationDirectory = Marshal.PtrToStringAnsi(pAddress);
                    }

                    Console.WriteLine("Session ID : " + oSessionInfo.iSessionID);
                    Console.WriteLine("Session State : " + oSessionInfo.oState);
                    Console.WriteLine("Workstation Name : " +
                                      oSessionInfo.sWinsWorkstationName);
                    Console.WriteLine("IP Address : " + sIPAddress);
                    Console.WriteLine("User Name : " + sDomain + @"\" + sUserName);
                    Console.WriteLine("Client Display Resolution: " +
                                      oClientDisplay.iHorizontalResolution + " x " +
                                      oClientDisplay.iVerticalResolution);
                    Console.WriteLine("Client Display Colour Depth: " +
                                      oClientDisplay.iColorDepth);
                    Console.WriteLine("Client Application Directory: " +
                                      sClientApplicationDirectory);

                    Console.WriteLine("-----------------------");
                }

                WTSFreeMemory(pSessionInfo);
            }
        }
    }

}

