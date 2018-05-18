using System;
using System.Linq;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Telephony;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Runtime;
using Java.IO;
using Java.Lang;
using B = Android.OS.Build;
using Process = System.Diagnostics.Process;


namespace Plugin.DeviceInfo
{
    public class DeviceImpl : IDevice
    {
        public DeviceImpl()
        {
            var windowManager = (IWindowManager) Application
                .Context
                .GetSystemService(Context.WindowService)
                .JavaCast<IWindowManager>();

            if (B.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                var size = new Point();
                try
                {
                    windowManager.DefaultDisplay.GetRealSize(size);
                    this.ScreenHeight = size.Y;
                    this.ScreenWidth = size.X;
                }
                catch (NoSuchMethodError)
                {
                    this.ScreenHeight = windowManager.DefaultDisplay.Height;
                    this.ScreenWidth = windowManager.DefaultDisplay.Width;
                }
            }
            else
            {
                var metrics = new DisplayMetrics();
                windowManager.DefaultDisplay.GetMetrics(metrics);
                this.ScreenHeight = metrics.HeightPixels;
                this.ScreenWidth = metrics.WidthPixels;
            }
        }


        bool? jailbreak;
        public bool IsJailBreakDetected
        {
            get
            {
                this.jailbreak = this.jailbreak ?? this.IsJailBroken();
                return this.jailbreak.Value;
            }
        }


        static readonly string[] checks = new[]
        {
            "/system/app/Superuser.apk",
            "/sbin/su",
            "/system/bin/su",
            "/system/xbin/su",
            "/data/local/xbin/su",
            "/data/local/bin/su",
            "/system/sd/xbin/su",
            "/system/bin/failsafe/su",
            "/data/local/su",
            "/su/bin/su"
        };
        protected virtual bool IsJailBroken()
        {
            if (checks.Any(System.IO.File.Exists))
                return true;

            if (B.Tags?.Contains("test-keys") ?? false)
                return true;

            if (this.CheckJailBreakProcess())
                return true;

            return false;
        }


        protected virtual bool CheckJailBreakProcess()
        {
            try
            {
                using (var process = Runtime.GetRuntime().Exec("/system/xbin/which", new[] {"su"}))
                {
                    using (var reader = new BufferedReader(new InputStreamReader(process.InputStream)))
                    {
                        if (reader.ReadLine() != null)
                            return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }


        public int ScreenHeight { get; }
        public int ScreenWidth { get; }

        //public string DeviceId { get; } = B.Serial;
        public string DeviceId => Settings.Secure.GetString(Application.Context.ApplicationContext.ContentResolver,
            Settings.Secure.AndroidId);

        public string Manufacturer { get; } = B.Manufacturer;
        public string Model { get; } = B.Model;
        public string OperatingSystem { get; } = B.VERSION.Release;
        public string OperatingSystemVersion { get; } = B.VERSION.SdkInt.ToString();

        public bool IsSimulator { get; } =
            B.Product.Equals("google_sdk", StringComparison.InvariantCultureIgnoreCase)
            || B.Model.Contains("Emulator")
            || B.Model.Contains("Android SDK built for x86");


        public bool IsTablet => ((TelephonyManager)Application
            .Context
            .ApplicationContext
            .GetSystemService(Context.TelephonyService))
            .PhoneType == PhoneType.None; // best I can do
    }
}