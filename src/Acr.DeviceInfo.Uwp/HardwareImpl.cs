using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Devices.Radios;
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


        public int ScreenHeight => (int)Window.Current.Bounds.Height;
        public int ScreenWidth => (int)Window.Current.Bounds.Width;
        public string DeviceId => this.deviceInfo.Id.ToString();
        public string Manufacturer => this.deviceInfo.SystemManufacturer;
        public string Model => this.deviceInfo.SystemSku;
        public string OperatingSystem => this.deviceInfo.OperatingSystem;
        public bool IsSimulator { get; } = Package.Current.Id.Architecture == ProcessorArchitecture.Unknown;
        public bool IsTablet => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch;
        public OperatingSystemType OS { get; } = OperatingSystemType.WindowUniversal;


        public Task<bool> HasFeature(Feature feature)
        {
            switch (feature)
            {
                case Feature.Camera:
                case Feature.CameraBack:
                case Feature.CameraFront:
                    return this.HasCamera(feature);

                case Feature.Bluetooth:
                case Feature.BluetoothLE:
                    return this.HasBluetooth();

                default:
                    return Task.FromResult(false);
            }
        }


        async Task<bool> HasCamera(Feature feature)
        {
            var list = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            foreach (var device in list)
            {
                if (MediaCapture.IsVideoProfileSupported(device.Id))
                {
                    if (feature == Feature.Camera)
                        return true;

                    switch (device.EnclosureLocation.Panel)
                    {
                        case Panel.Front:
                            if (feature == Feature.CameraFront)
                                return true;
                            break;

                        case Panel.Back:
                            if (feature == Feature.CameraBack)
                                return true;
                            break;
                    }
                }
            }
            return false;
        }


        async Task<bool> HasBluetooth()
        {
            var radios = await Radio.GetRadiosAsync();
            return radios.Any(x => x.Kind == RadioKind.Bluetooth);
        }
    }
}