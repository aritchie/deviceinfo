using System;


namespace Acr.DeviceInfo {

    public static class DeviceInfo {

        readonly static Lazy<IApp> appInit = new Lazy<IApp>(() => {
#if __PLATFORM__
            return new AppImpl();
#else
            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
        }, false);


        readonly static Lazy<IBattery> batteryInit = new Lazy<IBattery>(() => {
#if __PLATFORM__
            return new BatteryImpl();
#else
            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
        }, false);


        readonly static Lazy<IConnectivity> connectInit = new Lazy<IConnectivity>(() => {
#if __PLATFORM__
            return new ConnectivityImpl();
#else
            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
        }, false);


        readonly static Lazy<IHardware> hwInit = new Lazy<IHardware>(() => {
#if __PLATFORM__
            return new HardwareImpl();
#else
            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
        }, false);


        static IApp appInstance;
        public static IApp App {
            get { return appInstance ?? appInit.Value; }
            set { appInstance = value; }
        }


        static IBattery batteryInstance;
        public static IBattery Battery {
            get { return batteryInstance ?? batteryInit.Value; }
            set { batteryInstance = value; }
        }


        static IConnectivity connectInstance;
        public static IConnectivity Connectivity {
            get { return connectInstance ?? connectInit.Value; }
            set { connectInstance = value; }
        }


        static IHardware hwInstance;
        public static IHardware Hardware {
            get { return hwInstance ?? hwInit.Value; }
            set { hwInstance = value; }
        }
    }
}
