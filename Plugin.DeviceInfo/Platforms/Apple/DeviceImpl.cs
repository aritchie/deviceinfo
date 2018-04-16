using System;
using System.Threading.Tasks;


namespace Plugin.DeviceInfo
{
    public partial class DeviceImpl : IDevice
    {
        int screenHeight;
        public int ScreenHeight
        {
            get
            {
                this.Init();
                return this.screenHeight;
            }
        }


        int screenWidth;
        public int ScreenWidth
        {
            get
            {
                this.Init();
                return this.screenWidth;
            }
        }


        string deviceId;
        public string DeviceId
        {
            get
            {
                this.Init();
                return this.deviceId;
            }
        }

        public string Manufacturer { get; } = "Apple";


        string model;
        public string Model
        {
            get
            {
                this.Init();
                return this.model;
            }
        }


        string operatingSystem;
        public string OperatingSystem
        {
            get
            {
                this.Init();
                return this.operatingSystem;
            }
        }


        string operatingSystemVersion;
        public string OperatingSystemVersion
        {
            get
            {
                this.Init();
                return this.operatingSystemVersion;
            }
        }


        bool simulator;
        public bool IsSimulator
        {
            get
            {
                this.Init();
                return this.simulator;
            }
        }


        bool tablet;
        public bool IsTablet
        {
            get
            {
                this.Init();
                return this.tablet;
            }
        }
    }
}