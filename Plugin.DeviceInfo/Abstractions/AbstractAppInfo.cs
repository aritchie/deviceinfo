using System;
using System.Globalization;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public abstract class AbstractAppInfo : IAppInfo
    {
        public abstract string Version { get; }
        public abstract string ShortVersion { get; }
        public virtual CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public virtual IObservable<CultureInfo> WhenCultureChanged() => Observable.Return(CultureInfo.CurrentCulture);
        public virtual bool IsBackgrounded => false;
        public virtual IObservable<object> WhenEnteringForeground() => Observable.Empty<object>();
        public virtual IObservable<object> WhenEnteringBackground() => Observable.Empty<object>();
    }
}
