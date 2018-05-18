using System;
using System.Threading.Tasks;
using AppKit;


namespace Plugin.DeviceInfo
{
    public partial class DeviceImpl : IDevice
    {
        bool init;
        void Init()
        {
            if (this.init)
                return;

            this.init = true;
            var tcs = new TaskCompletionSource<object>();
            NSApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                this.screenHeight = (int)NSScreen.MainScreen.Frame.Height;
                this.screenWidth = (int)NSScreen.MainScreen.Frame.Width;

                this.deviceId = null;
                this.operatingSystem = "MacOSX";
                this.operatingSystemVersion = "10";

                this.tablet = false;
                this.simulator = false;

                tcs.TrySetResult(null);
            });

            tcs.Task.Wait();
        }


        public bool IsJailBreakDetected { get; } = false;
    }
}