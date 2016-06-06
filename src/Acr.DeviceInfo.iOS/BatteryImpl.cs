using System;
using Foundation;
using UIKit;


namespace Acr.DeviceInfo
{

    public class BatteryImpl : AbstractBatteryImpl
    {
        NSObject stateCallback;
        NSObject levelCallback;


        public int Percentage => (int)(UIDevice.CurrentDevice.BatteryLevel * 100F);
        public bool IsCharging => UIDevice.CurrentDevice.BatteryState == UIDeviceBatteryState.Charging;


        protected override void StartMonitoringState(Action<int, bool> callback)
        {
            this.stateCallback = UIDevice.Notifications.ObserveBatteryLevelDidChange((sender, args) => { });
            this.levelCallback = UIDevice.Notifications.ObserveBatteryStateDidChange((sender, args) => { });
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
        }


        protected override void StopMonitoringState()
        {
            this.stateCallback?.Dispose();
            this.stateCallback = null;
            this.levelCallback?.Dispose();
            this.levelCallback = null;
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = false;
        }
    }
}