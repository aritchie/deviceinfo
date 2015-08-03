using System;


namespace Acr.DeviceInfo {

    public interface IDeviceInfo {

        int ScreenHeight { get; }
        int ScreenWidth { get; }

        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        string CellularNetworkCarrier { get; }
        bool IsFrontCameraAvailable { get; }
        bool IsRearCameraAvailable { get; }
        bool IsSimulator { get; }
        bool IsTablet { get; }
        OperatingSystemType OS { get; }
    }
}