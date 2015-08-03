using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Telephony;
using Android.Provider;
using B = Android.OS.Build;


namespace Acr.DeviceInfo {

    public class DeviceInfoImpl : IDeviceInfo {
        readonly Lazy<string> deviceId;


        public DeviceInfoImpl() {
            var d = Resources.System.DisplayMetrics;
            this.ScreenHeight = (int)(d.HeightPixels / d.Density);
            this.ScreenWidth = (int)(d.WidthPixels / d.Density);

            var tel = Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
            this.IsTablet = (tel?.PhoneType == PhoneType.None);
            this.CellularNetworkCarrier = tel?.NetworkOperatorName;

            this.deviceId = new Lazy<string>(() => {
                try {
                    if (tel?.DeviceId == null)
                        return Settings.Secure.GetString(Application.Context.ApplicationContext.ContentResolver, Settings.Secure.AndroidId);

                    return tel.DeviceId;
                }
                catch (Exception ex) {
                    Console.WriteLine("Could not read DeviceId - Did you grant READ_PHONE_STATE permissions in your android manifest? " + ex);
                    return null;
                }
            });
        }


        public int ScreenHeight { get; }
        public int ScreenWidth { get; }
        public string DeviceId => this.deviceId.Value;
        public string Manufacturer { get; } = B.Manufacturer;
        public string Model { get; } = B.Model;
        public string OperatingSystem { get; } = $"{B.VERSION.Release} - SDK: {B.VERSION.SdkInt}";
        public string CellularNetworkCarrier { get; }
        public bool IsFrontCameraAvailable { get; } = Application.Context.ApplicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureCameraFront);
        public bool IsRearCameraAvailable { get; } = Application.Context.ApplicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureCamera);
        public bool IsSimulator { get; } = B.Product.Equals("google_sdk");
        public bool IsTablet { get; }
        public OperatingSystemType OS { get; } = OperatingSystemType.Android;
    }
}