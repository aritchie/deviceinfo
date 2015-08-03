using System;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using Env = System.Environment;
using DevEnv = Microsoft.Devices.Environment;


namespace Acr.DeviceInfo {

    public class HardwareImpl : AbstractNpc, IHardware {
        readonly Lazy<string> deviceId;

        public HardwareImpl() {
            this.deviceId = new Lazy<string>(() => {
                try {
//var token = HardwareIdentification.GetPackageSpecificToken(null);
//        var hardwareId = token.Id;
//        var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

//        var bytes = new byte[hardwareId.Length];
//        dataReader.ReadBytes(bytes);

//        return Convert.ToBase64String(bytes);
                    var deviceIdBytes = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
                    return Convert.ToBase64String(deviceIdBytes);
                }
                catch (Exception ex) {
                    Console.WriteLine("Could not get DeviceId - did you enable ID_CAP_IDENTITY_DEVICE?  Error: {0}", ex);
                    return null;
                }
            });
            DeviceNetworkInformation.NetworkAvailabilityChanged += (sender, args) => { };
        }



        public int ScreenHeight { get; } = (int)Application.Current.Host.Content.ActualHeight;
        public int ScreenWidth { get; } = (int)Application.Current.Host.Content.ActualWidth;
        public string DeviceId => this.deviceId.Value;
        public string Manufacturer { get; } =  DeviceStatus.DeviceManufacturer;
        public string Model { get; } = DeviceStatus.DeviceName;
        public string OperatingSystem { get; } = Env.OSVersion.ToString();
        public bool IsFrontCameraAvailable { get; } = PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing);
        public bool IsRearCameraAvailable { get; } = PhotoCamera.IsCameraTypeSupported(CameraType.Primary);
        public bool IsSimulator { get; } = (DevEnv.DeviceType == DeviceType.Emulator);
        public bool IsTablet { get; } = false;
        public OperatingSystemType OS { get; } = OperatingSystemType.WindowsPhone;
    }
}
