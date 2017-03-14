using System;


namespace Plugin.DeviceInfo
{
    public class HardwareInfo : IHardwareInfo
    {
        public int ScreenHeight { get; }
        public int ScreenWidth { get; }
        public string DeviceId { get; }
        public string Manufacturer { get; }
        public string Model { get; }
        public string OperatingSystem { get; }
        public string OperatingSystemVersion { get; }
        public bool IsSimulator { get; }
        public bool IsTablet { get; }
    }
}
