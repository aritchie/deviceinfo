using System;
using System.Reactive.Linq;
using UIKit;


namespace Acr.DeviceInfo
{

    public class BatteryImpl : IBattery
    {
        public int Percentage => (int)(UIDevice.CurrentDevice.BatteryLevel * 100F);

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


        public IObservable<int> WhenBatteryPercentageChanged()
        {
            return Observable.Create<int>(ob =>
                UIDevice
                    .Notifications
                    .ObserveBatteryLevelDidChange((sender, args) => ob.OnNext(this.Percentage))
            );
        }


        public IObservable<PowerStatus> WhenPowerStatusChanged()
        {
            return Observable.Create<PowerStatus>(ob =>
                UIDevice
                    .Notifications
                    .ObserveBatteryStateDidChange((sender, args) => ob.OnNext(this.Status))
            );
        }
    }
}