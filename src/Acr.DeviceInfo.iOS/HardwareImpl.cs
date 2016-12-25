using System;
using ObjCRuntime;
using UIKit;


namespace Acr.DeviceInfo
{
    public class HardwareImpl : IHardware
    {
        public int ScreenHeight { get; } = (int)UIScreen.MainScreen.Bounds.Height * (int)UIScreen.MainScreen.Scale;
        public int ScreenWidth { get; } = (int)UIScreen.MainScreen.Bounds.Width * (int)UIScreen.MainScreen.Scale;
        public string DeviceId { get; } = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        public string Manufacturer { get; } = "Apple";
        public string Model { get; } = UIDevice.CurrentDevice.Model;
        public string OperatingSystem { get; } = $"{UIDevice.CurrentDevice.SystemName} {UIDevice.CurrentDevice.SystemVersion}";
        public bool IsSimulator { get; } = Runtime.Arch == Arch.SIMULATOR;
        public bool IsTablet { get; } = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
        public OperatingSystemType OS { get; } = OperatingSystemType.iOS;
    }
}
