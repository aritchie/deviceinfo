using System;
using System.Reactive.Linq;
using UIKit;


namespace Plugin.DeviceInfo
{
    public class PowerStateImpl : IPowerState
    {
        readonly IObservable<int> levelOb;


        public PowerStateImpl()
        {
            this.levelOb = Observable
                .Create<int>(ob =>
                {
                    UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
                    var not = UIDevice
                        .Notifications
                        .ObserveBatteryLevelDidChange((sender, args) => ob.OnNext(this.Percentage));

                    return () =>
                    {
                        UIDevice.CurrentDevice.BatteryMonitoringEnabled = false;
                        not.Dispose();
                    };
                 })
                .Publish()
                .RefCount();
        }


        public int Percentage => Math.Abs((int)(UIDevice.CurrentDevice.BatteryLevel * 100F));


        public PowerStatus Status
        {
            get
            {
                switch (UIDevice.CurrentDevice.BatteryState)
                {
                    case UIDeviceBatteryState.Charging:
                        return PowerStatus.Charging;

                    case UIDeviceBatteryState.Full:
                        return PowerStatus.Charged;

                    case UIDeviceBatteryState.Unplugged:
                        return PowerStatus.Discharging;

                    case UIDeviceBatteryState.Unknown:
                    default:
                        return PowerStatus.Unknown;
                }
            }
        }


        public IObservable<int> WhenBatteryPercentageChanged() => this.levelOb;


        public IObservable<PowerStatus> WhenPowerStatusChanged() => Observable
            .Create<PowerStatus>(ob =>
                UIDevice
                    .Notifications
                    .ObserveBatteryStateDidChange((sender, args) => ob.OnNext(this.Status))
            );
    }
}