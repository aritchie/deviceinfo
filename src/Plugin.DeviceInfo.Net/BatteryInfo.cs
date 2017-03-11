using System;
using System.Reactive.Linq;
using System.Windows.Forms;


namespace Plugin.DeviceInfo
{

    public class BatteryInfo : IBatteryInfo
    {
        public PowerStatus Status
        {
            get
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
            }
        }


        public int Percentage => Convert.ToInt32(SystemInformation.PowerStatus.BatteryLifePercent);
        public IObservable<int> WhenBatteryPercentageChanged() => Observable.Create<int>(ob =>
        {
            var last = this.Percentage;
            ob.OnNext(last);

            return Observable
                .Interval(TimeSpan.FromSeconds(30))
                .Subscribe(x =>
                {
                    var now = this.Percentage;
                    if (now != last)
                    {
                        last = now;
                        ob.OnNext(last);
                    }
                });
        });


        public IObservable<PowerStatus> WhenPowerStatusChanged() => Observable.Create<PowerStatus>(ob =>
        {
            var last = this.Status;
            ob.OnNext(last);

            return Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Subscribe(x =>
                {
                    var now = this.Status;
                    if (now != last)
                    {
                        last = now;
                        ob.OnNext(last);
                    }
                });
        });
    }
}
