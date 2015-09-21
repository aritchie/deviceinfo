using System;


namespace Acr.DeviceInfo {

    public class HardwareImpl : IHardware {
        public int ScreenHeight { get; }
        public int ScreenWidth { get; }
        public string DeviceId { get; }
        public string Manufacturer { get; }
        public string Model { get; }
        public string OperatingSystem { get; }
        public bool IsFrontCameraAvailable { get; }
        public bool IsRearCameraAvailable { get; }
        public bool IsSimulator { get; } = false;
        public bool IsTablet { get; } = false;
        public OperatingSystemType OS { get; } = OperatingSystemType.NetCore;
    }
}
