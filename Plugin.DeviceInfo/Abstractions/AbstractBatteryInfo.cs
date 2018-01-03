using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Plugin.DeviceInfo
{
    public abstract class AbstractBatteryInfo : IBatteryInfo
    {
        public virtual int Percentage => -1;
        public virtual PowerStatus Status => PowerStatus.Unknown;
        public virtual IObservable<int> WhenBatteryPercentageChanged() => Observable.Empty<int>();
        public virtual IObservable<PowerStatus> WhenPowerStatusChanged() => Observable.Return(PowerStatus.Unknown);
    }
}
