using System;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public abstract class AbstractApp : IApp
    {
        public abstract bool EnableSleepMode { get; set; }
        public abstract string Version { get; }
        public abstract string ShortVersion { get; }
        public virtual CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public virtual IObservable<CultureInfo> WhenCultureChanged() => Observable.Return(CultureInfo.CurrentCulture);
        public virtual bool IsBackgrounded => false;
        public virtual IObservable<Unit> WhenEnteringForeground() => Observable.Empty<Unit>();
        public virtual IObservable<Unit> WhenEnteringBackground() => Observable.Empty<Unit>();
    }
}
