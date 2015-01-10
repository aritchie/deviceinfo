using System;


namespace Acr.DeviceInfo {

    public static class DeviceInfo {
        private static IDeviceInfo instance;
        private static readonly object syncLock = new object();


        public static IDeviceInfo Instance {
            get {
                if (instance == null) {
                    lock (syncLock) {
                        if (instance == null) {
#if __PLATFORM__
                            instance = new DeviceInfoImpl();
#else
                            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
                        }
                    }
                }
                return instance;
            }
            set { instance = value; }
        }
    }
}
