using System;


namespace Acr.DeviceInfo {

    public static class DeviceInfo {

        private readonly static Lazy<IDeviceInfo> instanceInit = new Lazy<IDeviceInfo>(() => {
#if __PLATFORM__
            return new DeviceInfoImpl();
#else
            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
        }, false);


        private static IDeviceInfo customInstance;
        public static IDeviceInfo Instance {
            get { return customInstance ?? instanceInit.Value; }
            set { customInstance = value; }
        }
    }
}
