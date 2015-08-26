using System;
using UIKit;


namespace Acr.DeviceInfo {

    public class BatteryImpl : AbstractBatteryImpl {

        public BatteryImpl() {
            UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
            UIDevice.Notifications.ObserveBatteryLevelDidChange((sender, args) => this.SetBatteryState());
            UIDevice.Notifications.ObserveBatteryStateDidChange((sender, args) => this.SetBatteryState());
            this.SetBatteryState(); // set initial state
        }


        void SetBatteryState() {
            this.Percentage = (int)(UIDevice.CurrentDevice.BatteryLevel * 100F);
            this.IsCharging = (UIDevice.CurrentDevice.BatteryState == UIDeviceBatteryState.Charging);
        }
    }
}