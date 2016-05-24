using System;
using UIKit;


namespace Acr.DeviceInfo
{

    public class BatteryImpl : IBattery
    {
        public BatteryImpl()
        {
            UIDevice.Notifications.ObserveBatteryLevelDidChange((sender, args) => this.stateChanged?.Invoke(this, EventArgs.Empty));
            UIDevice.Notifications.ObserveBatteryStateDidChange((sender, args) => this.stateChanged?.Invoke(this, EventArgs.Empty));
        }


        public int Percentage => (int)(UIDevice.CurrentDevice.BatteryLevel * 100F);
        public bool IsCharging => UIDevice.CurrentDevice.BatteryState == UIDeviceBatteryState.Charging;


        EventHandler stateChanged;
        public event EventHandler StateChanged
        {
            add
            {
                if (this.stateChanged == null)
                    UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;

                this.stateChanged += value;
            }
            remove
            {
                this.stateChanged -= value;
                if (this.stateChanged == null)
                    UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
            }
        }
    }
}