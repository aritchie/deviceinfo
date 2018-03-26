using System;
using System.Reactive.Linq;

namespace Plugin.DeviceInfo
{
    public abstract class AbstractPowerState : IPowerState
    {
        public virtual IObservable<int> WhenBatteryPercentageChanged() => Observable.Empty<int>();
        public virtual IObservable<PowerStatus> WhenPowerStatusChanged() => Observable.Return(PowerStatus.Unknown);
    }
}
