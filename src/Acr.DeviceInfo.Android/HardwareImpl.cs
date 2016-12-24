using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Telephony;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Runtime;
using Java.Lang;
using B = Android.OS.Build;


namespace Acr.DeviceInfo
{

    public class HardwareImpl : IHardware
    {
        readonly TelephonyManager telManager;


        public HardwareImpl()
        {
            var windowManager = (IWindowManager)Application
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

            this.telManager = Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
        }


        public int ScreenHeight { get; }
        public int ScreenWidth { get; }

        //public string DeviceId { get; } = B.Serial;
        public string DeviceId => this.telManager?.DeviceId ?? Settings.Secure.GetString(Application.Context.ApplicationContext.ContentResolver, Settings.Secure.AndroidId);
        public string Manufacturer { get; } = B.Manufacturer;
        public string Model { get; } = B.Model;
        public string OperatingSystem { get; } = $"{B.VERSION.Release} - SDK: {B.VERSION.SdkInt}";
        public bool IsSimulator { get; } = B.Product.Equals("google_sdk");
        public bool IsTablet => this.telManager?.PhoneType == PhoneType.None; // best I can do
        public OperatingSystemType OS { get; } = OperatingSystemType.Android;
    }
}