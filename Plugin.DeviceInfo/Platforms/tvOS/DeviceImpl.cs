using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UIKit;
using ObjCRuntime;


namespace Plugin.DeviceInfo
{
    public class DeviceImpl : IDevice
    {
        bool init;
        void Init()
        {
            if (this.init)
                return;

            this.init = true;
            var tcs = new TaskCompletionSource<object>();

            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                this.screenHeight = (int)UIScreen.MainScreen.Bounds.Height * (int)UIScreen.MainScreen.Scale;
                this.screenWidth = (int)UIScreen.MainScreen.Bounds.Width * (int)UIScreen.MainScreen.Scale;
                this.deviceId = UIDevice.CurrentDevice.IdentifierForVendor.AsString();

                this.model = UIDevice.CurrentDevice.Model;
                this.operatingSystem = UIDevice.CurrentDevice.SystemName;
                this.operatingSystemVersion = UIDevice.CurrentDevice.SystemVersion;
                this.tablet = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad;
                this.simulator = Runtime.Arch == Arch.SIMULATOR;

                tcs.TrySetResult(null);
            });
            tcs.Task.Wait();
        }


        int screenHeight;
        public int ScreenHeight
        {
            get
            {
                this.Init();
                return this.screenHeight;
            }
        }


        int screenWidth;
        public int ScreenWidth
        {
            get
            {
                this.Init();
                return this.screenWidth;
            }
        }


        string deviceId;
        public string DeviceId
        {
            get
            {
                this.Init();
                return this.deviceId;
            }
        }

        public string Manufacturer { get; } = "Apple";


        string model;
        public string Model
        {
            get
            {
                this.Init();
                return this.model;
            }
        }


        string operatingSystem;
        public string OperatingSystem
        {
            get
            {
                this.Init();
                return this.operatingSystem;
            }
        }


        string operatingSystemVersion;
        public string OperatingSystemVersion
        {
            get
            {
                this.Init();
                return this.operatingSystemVersion;
            }
        }


        bool simulator;
        public bool IsSimulator
        {
            get
            {
                this.Init();
                return this.simulator;
            }
        }


        bool tablet;
        public bool IsTablet
        {
            get
            {
                this.Init();
                return this.tablet;
            }
        }


        public bool IdleTimerDisabled
        {
            get => false;
            set {}
        }
    }
}