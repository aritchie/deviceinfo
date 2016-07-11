using System;
using Windows.Devices.Power;
using Windows.System.Power;


namespace Acr.DeviceInfo
{
    public class BatteryImpl : IBattery
    {
        //public BatteryImpl() {
        //    this.UpdateStats();
        //    Battery.AggregateBattery.ReportUpdated += (sender, args) => this.UpdateStats();
        //}


        //void UpdateStats() {
        //    var stats = Battery.AggregateBattery.GetReport();
        //    this.IsCharging = stats.Status != BatteryStatus.Discharging;

        //    if (stats.RemainingCapacityInMilliwattHours != null && stats.FullChargeCapacityInMilliwattHours != null)
        //        this.Percentage = stats.RemainingCapacityInMilliwattHours.Value / stats.FullChargeCapacityInMilliwattHours.Value;
        //}

        public int Percentage { get; }
        public PowerStatus Status { get; }
        public IObservable<int> WhenBatteryPercentageChanged()
        {
            throw new NotImplementedException();
        }

        public IObservable<PowerStatus> WhenPowerStatusChanged()
        {
            throw new NotImplementedException();
        }
    }
}
