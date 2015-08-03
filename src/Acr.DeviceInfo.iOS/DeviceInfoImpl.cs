using System;
using CoreTelephony;
using Foundation;
using ObjCRuntime;
using UIKit;


namespace Acr.DeviceInfo {

    public class DeviceInfoImpl : AbstractNpc, IDeviceInfo {

        public DeviceInfoImpl() {
            using (var info = new CTTelephonyNetworkInfo())
                this.CellularNetworkCarrier = info.SubscriberCellularProvider.CarrierName;
        }

        public int ScreenHeight { get; } = (int)UIScreen.MainScreen.Bounds.Height;
        public int ScreenWidth { get; } = (int)UIScreen.MainScreen.Bounds.Width;
        public string DeviceId { get; } = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        public string Manufacturer { get; } = "Apple";
        public string Model { get; } = UIDevice.CurrentDevice.Model;
        public string OperatingSystem { get; } = $"{UIDevice.CurrentDevice.SystemName} {UIDevice.CurrentDevice.SystemVersion}";
        public string CellularNetworkCarrier { get; }
        public bool IsFrontCameraAvailable { get; } = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
        public bool IsRearCameraAvailable { get; } = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);
        public bool IsSimulator { get; } = (Runtime.Arch == Arch.SIMULATOR);
        public bool IsTablet { get; } = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;

        public OperatingSystemType OS { get; } = OperatingSystemType.iOS;
    }
}
/*
  float scale = 1;
  if ([[UIScreen mainScreen] respondsToSelector:@selector(scale)]) {
    scale = [[UIScreen mainScreen] scale];
  }
  float dpi;
  if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad) {
    dpi = 132 * scale;
  } else if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone) {
    dpi = 163 * scale;
  } else {
    dpi = 160 * scale;
  }
*/
