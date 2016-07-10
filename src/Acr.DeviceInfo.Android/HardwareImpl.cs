using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Telephony;
using Android.Provider;
using B = Android.OS.Build;


namespace Acr.DeviceInfo
{

    public class HardwareImpl : IHardware
    {
        readonly Lazy<string> deviceId;


        public HardwareImpl()
        {
            var d = Resources.System.DisplayMetrics;
            this.ScreenHeight = (int)(d.HeightPixels / d.Density);
            this.ScreenWidth = (int)(d.WidthPixels / d.Density);

            var tel = Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
            this.IsTablet = (tel?.PhoneType == PhoneType.None);

            this.deviceId = new Lazy<string>(() =>
            {
                if (!Utils.CheckPermission(Manifest.Permission.ReadPhoneState))
                    return null;

                if (tel?.DeviceId == null)
                    return Settings.Secure.GetString(Application.Context.ApplicationContext.ContentResolver, Settings.Secure.AndroidId);

                return tel.DeviceId;
            });
        }


        public int ScreenHeight { get; }
        public int ScreenWidth { get; }
        public string DeviceId => this.deviceId.Value;
        public string Manufacturer { get; } = B.Manufacturer;
        public string Model { get; } = B.Model;
        public string OperatingSystem { get; } = $"{B.VERSION.Release} - SDK: {B.VERSION.SdkInt}";
        public bool IsFrontCameraAvailable { get; } = Application.Context.ApplicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureCameraFront);
        public bool IsRearCameraAvailable { get; } = Application.Context.ApplicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureCamera);
        public bool IsSimulator { get; } = B.Product.Equals("google_sdk");
        public bool IsTablet { get; }
        public OperatingSystemType OS { get; } = OperatingSystemType.Android;
    }
}