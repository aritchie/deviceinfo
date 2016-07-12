using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Devices.Enumeration;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Xaml;
using Windows.Media.Capture;
using Windows.UI.Core;
using Windows.UI.ViewManagement;


namespace Acr.DeviceInfo
{
    public class HardwareImpl : IHardware
    {
        readonly EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();
        readonly Lazy<CameraInfo> cameraLazy = new Lazy<CameraInfo>(() =>
        {
            var info = new CameraInfo();
            // TODO: I have no idea how to stop this from deadlocking
            //CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, async () =>
            //{
            //    var list = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            //    info.HasFront = list.Any(x => x.EnclosureLocation.Panel == Panel.Front);
            //    info.HasRear = list.Any(x => x.EnclosureLocation.Panel == Panel.Back);
            //}).AsTask().Wait();
            return info;
        });


        public int ScreenHeight => (int)Window.Current.Bounds.Height;
        public int ScreenWidth => (int)Window.Current.Bounds.Width;
        public string DeviceId => this.deviceInfo.Id.ToString();
        public string Manufacturer => this.deviceInfo.SystemManufacturer;
        public string Model => this.deviceInfo.SystemSku;
        public string OperatingSystem => this.deviceInfo.OperatingSystem;
        public bool IsFrontCameraAvailable => this.cameraLazy.Value.HasFront;
        public bool IsRearCameraAvailable => this.cameraLazy.Value.HasRear;
        public bool IsSimulator { get; } = (Package.Current.Id.Architecture == ProcessorArchitecture.Unknown);
        public bool IsTablet => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Touch;
        public OperatingSystemType OS { get; } = OperatingSystemType.WindowUniversal;
    }
}