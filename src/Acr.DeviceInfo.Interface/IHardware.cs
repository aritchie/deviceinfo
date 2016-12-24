using System;


namespace Acr.DeviceInfo
{
    public interface IHardware
    {
        int ScreenHeight { get; }
        int ScreenWidth { get; }

        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        bool IsSimulator { get; }
        bool IsTablet { get; }
        OperatingSystemType OS { get; }
    }
}