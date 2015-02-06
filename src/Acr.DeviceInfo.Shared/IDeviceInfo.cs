using System;


namespace Acr.DeviceInfo {

    public interface IDeviceInfo {

        int ScreenHeight { get; }
        int ScreenWidth { get; }
        int ScreenDensity { get; }
        string AppVersion { get; }
        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        bool IsFrontCameraAvailable { get; }
        bool IsRearCameraAvailable { get; }
        bool IsSimulator { get; }
    }
}