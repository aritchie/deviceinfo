using System;


namespace Plugin.DeviceInfo
{

    public static class CrossDevice
    {
#if NETSTANDARD1_0
        const string ERROR = "[deviceinfo] Platform implementation not found.  Have you added a nuget reference to your platform project?";
#endif


        static IAppInfo app;
        public static IAppInfo App
        {
            get
            {
#if NETSTANDARD1_0
                throw new Exception(ERROR);
#else
                app = app ?? new AppInfo();
                return app;
#endif
            }
            set { app = value; }
        }


        static IBatteryInfo batt;
        public static IBatteryInfo Battery
        {
            get
            {
#if NETSTANDARD1_0
                throw new Exception(ERROR);
#else
                batt = batt ?? new BatteryInfo();
                return batt;
#endif
            }
            set { batt = value; }
        }


        static INetworkInfo network;
        public static INetworkInfo Connectivity
        {
            get
            {
#if NETSTANDARD1_0
                throw new Exception(ERROR);
#else
                network = network ?? new NetworkInfo();
                return network;
#endif
            }
            set { network = value; }
        }


        static IHardwareInfo hardware;
        public static IHardwareInfo Hardware
        {
            get
            {
#if NETSTANDARD1_0
                throw new Exception(ERROR);
#else
                hardware = hardware ?? new HardwareInfo();
                return hardware;
#endif
            }
            set { hardware = value; }
        }
    }
}
