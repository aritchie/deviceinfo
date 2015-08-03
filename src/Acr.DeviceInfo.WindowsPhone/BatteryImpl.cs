using System;
using Windows.Phone.Devices.Power;
using Microsoft.Phone.Info;


namespace Acr.DeviceInfo {

    public class BatteryImpl : AbstractBatteryImpl {
        readonly Battery defaultBattery;


        public BatteryImpl() {
            this.defaultBattery = Battery.GetDefault();
            this.defaultBattery.RemainingChargePercentChanged += (sender, args) => this.SetBattery();
            DeviceStatus.PowerSourceChanged += (sender, args) => this.SetBattery();
        }


        void SetBattery() {
            this.Percentage = this.defaultBattery.RemainingChargePercent;
            this.IsCharging = (DeviceStatus.PowerSource == PowerSource.External);
        }
    }
}