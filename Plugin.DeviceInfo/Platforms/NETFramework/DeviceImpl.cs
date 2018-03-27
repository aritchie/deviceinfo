using System;
using System.Windows.Forms;


namespace Plugin.DeviceInfo
{

    public class DeviceImpl : IDevice
    {
        public bool EnableSleep { get; set; }
        public int ScreenHeight { get; } = SystemInformation.VirtualScreen.Height;
        public int ScreenWidth { get; } = SystemInformation.VirtualScreen.Width;
        public string DeviceId { get; }
        public string Manufacturer { get; }
        public string Model { get; }
        public string OperatingSystem => Environment.OSVersion.Platform.ToString();
        public string OperatingSystemVersion => Environment.OSVersion.VersionString;
        public bool IsSimulator { get; } = false;
        public bool IsTablet { get; } = false;
    }
}
