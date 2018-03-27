using System;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.System.Display;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;


namespace Plugin.DeviceInfo
{
    public class DeviceImpl : IDevice
    {
        readonly EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();


        public int ScreenHeight => (int)Window.Current.Bounds.Height;
        public int ScreenWidth => (int)Window.Current.Bounds.Width;
        public string DeviceId => this.deviceInfo.Id.ToString();
        public string Manufacturer => this.deviceInfo.SystemManufacturer;
        public string Model => this.deviceInfo.SystemSku;
        public string OperatingSystem => this.deviceInfo.OperatingSystem;

        string osVersion;
        public string OperatingSystemVersion
        {
            get
            {
                if (this.osVersion == null)
                {
                    var deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
                    var version = ulong.Parse(deviceFamilyVersion);
                    var major = (version & 0xFFFF000000000000L) >> 48;
                    var minor = (version & 0x0000FFFF00000000L) >> 32;
                    var build = (version & 0x00000000FFFF0000L) >> 16;
                    var revision = (version & 0x000000000000FFFFL);
                    this.osVersion = $"{major}.{minor}.{build}.{revision}";
                }
                return this.osVersion;
            }
        }
        public bool IsSimulator { get; } = Package.Current.Id.Architecture == ProcessorArchitecture.Unknown;
        public bool IsTablet => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch;


        DisplayRequest displayRequest;
        public bool IdleTimerDisabled
        {
            get => this.displayRequest != null;
            set
            {
                if (value)
                {
                    if (this.displayRequest != null)
                        return;

                    this.displayRequest = new DisplayRequest();
                    this.displayRequest.RequestActive();
                }
                else
                {
                    this.displayRequest?.RequestRelease();
                    this.displayRequest = null;
                }
            }
        }
    }
}