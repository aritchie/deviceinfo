using System;


namespace Plugin.DeviceInfo
{

    public static partial class CrossDevice
    {
        static CrossDevice()
        {
            App = new AppImpl();
            Device = new DeviceImpl();
            Network = new NetworkImpl();
            PowerState = new PowerStateImpl();
        }
    }
}
