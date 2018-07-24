using System;
using System.Reactive;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public partial class AppImpl : IApp
    {
        public bool IsBackgrounded { get; } = false;
        public IObservable<AppState> WhenStateChanged() => Observable.Empty<AppState>();
        public IObservable<Unit> EnableIdleTimer(bool enabled) => Observable.Empty<Unit>();
        public bool IsIdleTimerEnabled => true;
    }
}