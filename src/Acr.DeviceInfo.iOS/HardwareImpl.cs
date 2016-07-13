using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using CoreBluetooth;
using CoreFoundation;
using ObjCRuntime;
using UIKit;


namespace Acr.DeviceInfo
{

    public class HardwareImpl : IHardware
    {
        public int ScreenHeight { get; } = (int)UIScreen.MainScreen.Bounds.Height;
        public int ScreenWidth { get; } = (int)UIScreen.MainScreen.Bounds.Width;
        public string DeviceId { get; } = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        public string Manufacturer { get; } = "Apple";
        public string Model { get; } = UIDevice.CurrentDevice.Model;
        public string OperatingSystem { get; } = $"{UIDevice.CurrentDevice.SystemName} {UIDevice.CurrentDevice.SystemVersion}";
        public bool IsSimulator { get; } = (Runtime.Arch == Arch.SIMULATOR);
        public bool IsTablet { get; } = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
        public OperatingSystemType OS { get; } = OperatingSystemType.iOS;


        public Task<bool> HasFeature(Feature feature)
        {
            return Task.FromResult(this.Has(feature));
        }


        bool Has(Feature feature)
        {
            switch (feature)
            {
                case Feature.Camera:
                    return UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);

                case Feature.CameraBack:
                    return UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);

                case Feature.CameraFront:
                    return UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear) ||
                           UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);

                case Feature.Bluetooth:
                    return false;

                case Feature.BluetoothLE:
                    using (var cb = new CBCentralManager(DispatchQueue.DefaultGlobalQueue))
                    {
                        switch (cb.State)
                        {
                            case CBCentralManagerState.Unknown:
                            case CBCentralManagerState.Unsupported:
                                return false;

                            default:
                                return true;
                        }
                    }

                default:
                    return false;
            }
        }
    }
}
