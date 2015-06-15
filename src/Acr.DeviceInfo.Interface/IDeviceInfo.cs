using System;
using System.Globalization;


namespace Acr.DeviceInfo {

    public interface IDeviceInfo {

        int ScreenHeight { get; }
        int ScreenWidth { get; }
        string AppVersion { get; }
        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        bool IsFrontCameraAvailable { get; }
        bool IsRearCameraAvailable { get; }
        bool IsSimulator { get; }
        bool IsTablet { get; }
        bool IsAppInBackground { get; }

        CultureInfo Locale { get; }
        OperatingSystemType OS { get; }
    }
}