using System;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;


namespace Plugin.DeviceInfo
{
    public class HardwareInfo : IHardwareInfo
    {
        readonly EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();


        public int ScreenHeight => (int)Window.Current.Bounds.Height;
        public int ScreenWidth => (int)Window.Current.Bounds.Width;
        public string DeviceId => this.deviceInfo.Id.ToString();
        public string Manufacturer => this.deviceInfo.SystemManufacturer;
        public string Model => this.deviceInfo.SystemSku;
        public string OperatingSystem => this.deviceInfo.OperatingSystem;
        public bool IsSimulator { get; } = Package.Current.Id.Architecture == ProcessorArchitecture.Unknown;
        public bool IsTablet => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch;
    }
}