using System;


namespace Acr.DeviceInfo {

    public class BatteryImpl : AbstractBatteryImpl {

        public BatteryImpl() {
            if (!BatteryBroadcastReceiver.Register())
                return;

            BatteryBroadcastReceiver.StatusChanged += (sender, args) => this.SetState();
            this.SetState();
        }


        void SetState() {
            this.Percentage = BatteryBroadcastReceiver.Percentage;
            this.IsCharging = BatteryBroadcastReceiver.IsCharging;
        }
    }
}
