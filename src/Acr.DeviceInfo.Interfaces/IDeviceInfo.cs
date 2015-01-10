using System;


namespace Acr.DeviceInfo {

    public interface IDeviceInfo {

        int ScreenHeight { get; }
        int ScreenWidth { get; }
        string AppVersion { get; }
        string DeviceId { get; }
        string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        bool IsFrontCameraAvailable { get; }
        bool IsRearCameraAvailable { get; }
        bool IsSimulator { get; }

        // shutdown, suspended, backgrounded, started, resumed
        // IsFlashAvailable
        // Camera resolution?


    //UIApplication.Notifications.ObserveWillResignActive((EventHandler<NSNotificationEventArgs>) ((sender, args) => this.EmitBackgroundChange(true)));
    //  UIApplication.Notifications.ObserveDidEnterBackground((EventHandler<NSNotificationEventArgs>) ((sender, args) => this.EmitBackgroundChange(true)));
    //  UIApplication.Notifications.ObserveWillEnterForeground((EventHandler<NSNotificationEventArgs>) ((sender, args) => this.EmitBackgroundChange(false)));
    //  UIApplication.Notifications.ObserveDidBecomeActive((EventHandler<NSNotificationEventArgs>) ((sender, args) => this.EmitBackgroundChange(false)));
    //  UIApplication.Notifications.ObserveWillTerminate((EventHandler<NSNotificationEventArgs>) ((sender, args) => this.WillTerminate()));
    }
}