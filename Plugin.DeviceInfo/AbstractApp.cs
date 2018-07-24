using System;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public abstract class AbstractApp : IApp
    {
        public abstract string BundleName { get; }
        public abstract string Version { get; }
        public abstract string ShortVersion { get; }
        public virtual CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public virtual IObservable<CultureInfo> WhenCultureChanged() => Observable.Return(CultureInfo.CurrentCulture);
        public virtual bool IsBackgrounded => false;
        public virtual IObservable<AppState> WhenStateChanged() => Observable.Empty<AppState>();
        public virtual IObservable<Unit> EnableIdleTimer(bool enabled) => Observable.Empty<Unit>();
        public virtual bool IsIdleTimerEnabled => true;
    }
}
