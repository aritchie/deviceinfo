using System;
using System.Reactive;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public partial class AppImpl : IApp
    {
        public bool IsBackgrounded { get; } = false;
        public IObservable<Unit> WhenEnteringBackground() => Observable.Empty<Unit>();
        public IObservable<Unit> WhenEnteringForeground() => Observable.Empty<Unit>();
        public IObservable<Unit> EnableIdleTimer(bool enabled) => Observable.Empty<Unit>();
        public bool IsIdleTimerEnabled => true;
    }
}