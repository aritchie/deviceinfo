using System;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.UI.Xaml;


namespace Acr.DeviceInfo {

    public class DeviceInfoImpl : IDeviceInfo {
        private readonly EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();
        private readonly DeviceWatcher deviceWatcher;


        public DeviceInfoImpl() {
            this.deviceWatcher = DeviceInformation.CreateWatcher(DeviceClass.VideoCapture);
            this.deviceWatcher.Added += (sender, args) => {
                //this.videoDevices.Add(deviceInformation);
                //this.IsAvailable = this.videoDevices.Any(deviceInformation => deviceInformation.IsEnabled);
            };
            this.deviceWatcher.Removed += (sender, args) => {
                //this.videoDevices.RemoveAll(deviceInformation => deviceInformation.Id == deviceInformationUpdate.Id);
                //this.IsAvailable = this.videoDevices.Any(deviceInformation => deviceInformation.IsEnabled);
            };
            this.deviceWatcher.Updated += (sender, args) => {
                //foreach (DeviceInformation deviceInformation in this.videoDevices)
                //{
                //      if (deviceInformation.Id == deviceInformationUpdate.Id)
                //      {
                //             deviceInformation.Update(deviceInformationUpdate);
                //      }
                //}
            };
            this.deviceWatcher.Start();
        }


        public int ScreenHeight {
            get { return (int)Window.Current.Bounds.Height; }
        }


        public int ScreenWidth {
            get { return (int)Window.Current.Bounds.Width; }
        }


        public int ScreenDensity {
            get { return 0; }
        }


        public string AppVersion {
            get { return Package.Current.Id.Version.ToString(); }
        }


        public string DeviceId {
            get { return this.deviceInfo.Id.ToString(); }
        }


        public string Manufacturer {
            get { return this.deviceInfo.SystemManufacturer; }
        }


        public string Model {
            get { return this.deviceInfo.SystemSku; }
        }


        public string OperatingSystem {
            get { return this.deviceInfo.OperatingSystem; }
        }


        public bool IsFrontCameraAvailable {
            get { throw new NotImplementedException(); }
        }


        public bool IsRearCameraAvailable {
            get { throw new NotImplementedException(); }
        }


        public bool IsSimulator {
            get { return Package.Current.Id.Architecture == ProcessorArchitecture.Unknown; }
        }
    }
}