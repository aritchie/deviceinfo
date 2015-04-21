using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Telephony;
using Android.Provider;
using B = Android.OS.Build;
using App = Android.App.Application;


namespace Acr.DeviceInfo {

    public class DeviceInfoImpl : IDeviceInfo {

        private readonly Lazy<string> appVersion;
        private readonly Lazy<string> deviceId;
        private readonly Lazy<int> screenHeight;
        private readonly Lazy<int> screenWidth;
        private readonly Lazy<CultureInfo> locale;
        private readonly Lazy<bool> isTablet;


        public DeviceInfoImpl() {
            this.appVersion = new Lazy<string>(() => App
                .Context
                .ApplicationContext
                .PackageManager
                .GetPackageInfo(App.Context.PackageName, 0)
                .VersionName
            );
            this.screenHeight = new Lazy<int>(() => {
                var d = Resources.System.DisplayMetrics;
                return (int)(d.HeightPixels / d.Density);
            });
            this.screenWidth = new Lazy<int>(() => {
                var d = Resources.System.DisplayMetrics;
                return (int)(d.WidthPixels / d.Density);
            });
            this.deviceId = new Lazy<string>(() => {
                var tel = App.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
                if (tel == null || tel.DeviceId == null)
                    return Settings.Secure.GetString(Application.Context.ApplicationContext.ContentResolver, Settings.Secure.AndroidId);

                return tel.DeviceId;
            });
            this.isTablet = new Lazy<bool>(() => {
                var tel = App.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
                return (tel != null && tel.PhoneType == PhoneType.None);
            });
            this.locale = new Lazy<CultureInfo>(() => {
                //			TODO: detect changes
                //			Android.App.Application.Context.Resources.Configuration.Locale
			    //			RegionInfo.CurrentRegion
			    var value = Java.Util.Locale.Default.ToString().Replace("_", "-");
			    return new CultureInfo(value);
            });
        }


        public string AppVersion {
            get { return this.appVersion.Value; }
        }


        public int ScreenHeight {
            get { return this.screenHeight.Value; }
        }


        public int ScreenWidth {
            get { return this.screenWidth.Value; }
        }


        public string DeviceId {
            get { return this.deviceId.Value; }
        }


        public string Manufacturer {
            get { return B.Manufacturer; }
        }


        public string Model {
            get { return B.Model; }
        }


        private string os;
        public string OperatingSystem {
            get {
                this.os = this.os ?? String.Format("{0} - SDK: {1}", B.VERSION.Release, B.VERSION.SdkInt);
                return this.os;
            }
        }


        public bool IsFrontCameraAvailable {
            get { return App.Context.ApplicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureCameraFront); }
        }


        public bool IsRearCameraAvailable {
            get { return App.Context.ApplicationContext.PackageManager.HasSystemFeature(PackageManager.FeatureCamera); }
        }


        public bool IsSimulator {
            get { return B.Product.Equals("google_sdk"); }
        }


        public bool IsTablet {
            get { return this.isTablet.Value; }
        }


        public CultureInfo Locale {
            get { return this.locale.Value; }
        }


        public OperatingSystemType OS {
            get { return OperatingSystemType.Android; }
        }
    }
}