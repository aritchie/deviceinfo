using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Xaml;
using Windows.Media.Capture;
using Windows.UI.ViewManagement;


namespace Acr.DeviceInfo {

    public class HardwareImpl : IHardware {

        public HardwareImpl() {
            var deviceInfo = new EasClientDeviceInformation();
            this.DeviceId = deviceInfo.Id.ToString();
            this.Manufacturer = deviceInfo.SystemManufacturer;
            this.Model = deviceInfo.SystemSku;
            this.OperatingSystem = deviceInfo.OperatingSystem;
            //PhoneCallManager.RequestStoreAccess();
            this.DetectCameras().Wait();

            // TODO: this will change
            this.IsTablet = (UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch);
        }


        async Task DetectCameras() {
            var list = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            foreach (var device in list) {
                if (MediaCapture.IsVideoProfileSupported(device.Id)) {
                    if (device.EnclosureLocation.Panel == Panel.Front)
                        this.IsFrontCameraAvailable = true;
                    else if (device.EnclosureLocation.Panel == Panel.Back)
                        this.IsRearCameraAvailable = true;
                }
            }
        }


        public int ScreenHeight { get; } = (int)Window.Current.Bounds.Height;
        public int ScreenWidth { get; } = (int)Window.Current.Bounds.Width;
        public string DeviceId { get; }
        public string Manufacturer { get; }
        public string Model { get; }
        public string OperatingSystem { get; }
        public bool IsFrontCameraAvailable { get; private set; }
        public bool IsRearCameraAvailable { get; private set; }
        public bool IsSimulator { get; } = (Package.Current.Id.Architecture == ProcessorArchitecture.Unknown);
        public bool IsTablet { get; } = false;
        public OperatingSystemType OS { get; } = OperatingSystemType.Windows; // TODO: winphone
    }
}