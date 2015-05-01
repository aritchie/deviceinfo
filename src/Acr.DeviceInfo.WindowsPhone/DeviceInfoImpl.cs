using System;
using System.Globalization;
using System.Windows;
using Windows.ApplicationModel;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;
using Env = System.Environment;
using DevEnv = Microsoft.Devices.Environment;


namespace Acr.DeviceInfo {

    public class DeviceInfoImpl : IDeviceInfo {
        private readonly Lazy<string> deviceId;


        public DeviceInfoImpl() {
            PhoneApplicationService.Current.Activated += (sender, args) => this.IsAppInBackground = false;
            PhoneApplicationService.Current.RunningInBackground += (sender, args) => this.IsAppInBackground = true;

            this.deviceId = new Lazy<string>(() => {
                var deviceIdBytes = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
                return Convert.ToBase64String(deviceIdBytes);
            });
        }


        public string AppVersion { get {  return Package.Current.Id.Version.ToString(); }}
        public int ScreenHeight { get { return (int)Application.Current.Host.Content.ActualHeight; }}
        public int ScreenWidth { get { return (int)Application.Current.Host.Content.ActualWidth; }}
        public string DeviceId { get { return this.deviceId.Value; }}
        public string Manufacturer { get { return DeviceStatus.DeviceManufacturer; }}
        public string Model { get { return DeviceStatus.DeviceName; }}
        public string OperatingSystem { get { return Env.OSVersion.ToString(); }}
        public bool IsFrontCameraAvailable { get { return PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing); }}
        public bool IsRearCameraAvailable { get { return PhotoCamera.IsCameraTypeSupported(CameraType.Primary); }}
        public bool IsSimulator { get { return (DevEnv.DeviceType == DeviceType.Emulator); }}
        public bool IsTablet { get { return false; }}
        public bool IsAppInBackground { get; private set; }

        public CultureInfo Locale { get { return CultureInfo.CurrentCulture; }}
        public OperatingSystemType OS { get { return OperatingSystemType.WindowsPhone; }}
    }
}