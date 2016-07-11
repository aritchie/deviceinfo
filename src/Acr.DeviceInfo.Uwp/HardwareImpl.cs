using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Xaml;
using Windows.Media.Capture;
using Windows.UI.ViewManagement;


namespace Acr.DeviceInfo
{
    public class HardwareImpl : IHardware
    {
        readonly EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();


        // TODO: camera detection
        async Task DetectCameras()
        {
            var list = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            foreach (var device in list)
            {
                if (MediaCapture.IsVideoProfileSupported(device.Id))
                {
                    if (device.EnclosureLocation.Panel == Panel.Front)
                        this.IsFrontCameraAvailable = true;
                    else if (device.EnclosureLocation.Panel == Panel.Back)
                        this.IsRearCameraAvailable = true;
                }
            }
        }


        public int ScreenHeight => (int)Window.Current.Bounds.Height;
        public int ScreenWidth => (int)Window.Current.Bounds.Width;
        public string DeviceId => this.deviceInfo.Id.ToString();
        public string Manufacturer => this.deviceInfo.SystemManufacturer;
        public string Model => this.deviceInfo.SystemSku;
        public string OperatingSystem => this.deviceInfo.OperatingSystem;
        public bool IsFrontCameraAvailable { get; private set; }
        public bool IsRearCameraAvailable { get; private set; }
        public bool IsSimulator { get; } = (Package.Current.Id.Architecture == ProcessorArchitecture.Unknown);
        public bool IsTablet => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch;
        public OperatingSystemType OS { get; } = OperatingSystemType.WindowUniversal;
    }
}