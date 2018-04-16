using System;


namespace Plugin.DeviceInfo
{

    public static partial class CrossDevice
    {
        const string ERROR = "[Plugin.DeviceInfo] Platform implementation not found.  Have you added a nuget reference to your platform project?";


        static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new Exception(ERROR);

                return app;
            }
            set => app = value;
        }


        static IPowerState powerState;
        public static IPowerState PowerState
        {
            get
            {
                if (powerState == null)
                    throw new Exception(ERROR);

                return powerState;
            }
            set => powerState = value;
        }


        static INetwork network;
        public static INetwork Network
        {
            get
            {
                if (network == null)
                    throw new Exception(ERROR);

                return network;
            }
            set => network = value;
        }


        static IDevice device;
        public static IDevice Device
        {
            get
            {
                if (device == null)
                    throw new Exception(ERROR);

                return device;
            }
            set => device = value;
        }
    }
}
