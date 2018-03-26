using System;


namespace Plugin.DeviceInfo
{

    public static class CrossDevice
    {
        const string ERROR = "[Plugin.DeviceInfo] Platform implementation not found.  Have you added a nuget reference to your platform project?";


        static IAppInfo app;
        public static IAppInfo App
        {
            get
            {
#if NETSTANDARD
                throw new Exception(ERROR);
#else
                app = app ?? new AppInfo();
                return app;
#endif
            }
            set => app = value;
        }


        static IBatteryInfo batt;
        public static IBatteryInfo Battery
        {
            get
            {
#if NETSTANDARD
                throw new Exception(ERROR);
#else
                batt = batt ?? new PowerStateImpl();
                return batt;
#endif
            }
            set => batt = value;
        }


        static INetwork network;
        public static INetwork Network
        {
            get
            {
#if NETSTANDARD
                throw new Exception(ERROR);
#else
                network = network ?? new NetworkInfo();
                return network;
#endif
            }
            set => network = value;
        }


        static IHardwareInfo hardware;
        public static IHardwareInfo Hardware
        {
            get
            {
#if NETSTANDARD
                throw new Exception(ERROR);
#else
                hardware = hardware ?? new HardwareInfo();
                return hardware;
#endif
            }
            set => hardware = value;
        }
    }
}
