using System;
using System.Reactive.Linq;
using System.Windows.Forms;


namespace Plugin.DeviceInfo
{

    public class PowerStateImpl : IPowerState
    {
        int Percentage => Convert.ToInt32(SystemInformation.PowerStatus.BatteryLifePercent);


        public IObservable<int> WhenBatteryPercentageChanged() => Observable
            .Interval(TimeSpan.FromSeconds(30))
            .StartWith(this.Percentage)
            .Select(x => this.Percentage)
            .DistinctUntilChanged();


        public IObservable<PowerStatus> WhenPowerStatusChanged() => Observable
            .Interval(TimeSpan.FromSeconds(5))
            .Select(_ =>
            {
                switch (SystemInformation.PowerStatus.BatteryChargeStatus)
                {
                    case BatteryChargeStatus.Charging:
                        return PowerStatus.Charging;

                    case BatteryChargeStatus.Unknown:
                        return PowerStatus.Unknown;

                    case BatteryChargeStatus.NoSystemBattery:
                        return PowerStatus.NoBattery;

                    default:
                        return PowerStatus.Discharging;
                }
            })
            .DistinctUntilChanged();
    }
}
