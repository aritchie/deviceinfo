using System;


namespace Acr.DeviceInfo {

    public class BatteryImpl : AbstractBatteryImpl {

        public BatteryImpl() {
            BatteryBroadcastReceiver.StatusChanged += (sender, args) => this.SetState();
            BatteryBroadcastReceiver.Register();
        }


        void SetState() {
            this.Percentage = BatteryBroadcastReceiver.Percentage;
            this.IsCharging = BatteryBroadcastReceiver.IsCharging;
        }
    }
}
