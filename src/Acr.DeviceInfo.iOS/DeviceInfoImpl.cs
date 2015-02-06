using System;
using Foundation;
using ObjCRuntime;
using UIKit;


namespace Acr.DeviceInfo {

    public class DeviceInfoImpl : IDeviceInfo {


		public DeviceInfoImpl() {
			this.AppVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
			this.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
			this.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;
            this.ScreenDensity = 0; //UIScreen.MainScreen.Scale;
			this.DeviceId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();
			this.Manufacturer = "Apple";
			this.Model = UIDevice.CurrentDevice.Model;
			this.OperatingSystem = String.Format("{0} {1}", UIDevice.CurrentDevice.SystemName, UIDevice.CurrentDevice.SystemVersion);
			this.IsFrontCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
			this.IsRearCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);
			this.IsSimulator = (Runtime.Arch == Arch.SIMULATOR);
		}


		public string AppVersion { get; private set; }
		public int ScreenHeight { get; private set; }
		public int ScreenWidth { get; private set; }
        public int ScreenDensity { get; private set; }
		public string DeviceId { get; private set; }
		public string Manufacturer { get; private set; }
		public string Model { get; private set; }
		public string OperatingSystem { get; private set; }
		public bool IsFrontCameraAvailable { get; private set; }
		public bool IsRearCameraAvailable { get; private set; }
		public bool IsSimulator { get; private set; }
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