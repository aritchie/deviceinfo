using System;


namespace Plugin.DeviceInfo
{

    public static class CrossDevice
    {
#if PORTABLE
        const string ERROR = "[deviceinfo] Platform implementation not found.  Have you added a nuget reference to your platform project?";
#endif


        static IApp app;
        public static IApp App
        {
            get
            {
#if PORTABLE
                throw new Exception(ERROR);
#else
                app = app ?? new AppImpl();
                return app;
#endif
            }
            set { app = value; }
        }


        static IBattery batt;
        public static IBattery Battery
        {
            get
            {
#if PORTABLE
                throw new Exception(ERROR);
#else
                batt = this.batt ?? new BatteryImpl();
                return batt;
#endif
            }
            set { batt = value; }
        }


        static IConnectivity connectivity;
        public static IConnectivity Connectivity
        {
            get
            {
#if PORTABLE
                throw new Exception(ERROR);
#else
                connectivity = connectivity ?? new ConnectivityImpl();
                return connectivity;
#endif
            }
            set { connectivity = value; }
        }


        static IHardware hardware;
        public static IHardware Hardware
        {
            get
            {
#if PORTABLE
                throw new Exception(ERROR);
#else
                hardware = hardware ?? new HardwareImpl();
                return hardware;
#endif
            }
            set { hardware = value; }
        }
    }
}
