using System;


namespace Plugin.DeviceInfo
{
    public interface IDevice
    {
        /// <summary>
        /// Setting this to true disables the screen from turning off
        /// </summary>
        bool IdleTimerDisabled { get; set; }

        int ScreenHeight { get; }
        int ScreenWidth { get; }

        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        string OperatingSystemVersion { get; }
        bool IsSimulator { get; }
        bool IsTablet { get; }
    }
}