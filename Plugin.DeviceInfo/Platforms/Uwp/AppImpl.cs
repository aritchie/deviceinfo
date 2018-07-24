using System;
using System.Reactive;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.System.Display;
using Windows.UI.Xaml;


namespace Plugin.DeviceInfo
{
    public class AppImpl : AbstractApp
    {
        public override string BundleName => Package.Current.Id.Name;
        public override string Version { get; } = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public override string ShortVersion { get; } = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}";
        public override bool IsBackgrounded => !Window.Current.Visible;


        public override IObservable<AppState> WhenStateChanged() => Observable.Create<AppState>(ob =>
        {
            var handler = new WindowVisibilityChangedEventHandler((sender, args) =>
            {
                var state = args.Visible ? AppState.Foreground : AppState.Background;
                ob.OnNext(state);
            });
            Window.Current.VisibilityChanged += handler;
            return () => Window.Current.VisibilityChanged -= handler;
        });


        DisplayRequest displayRequest;
        public override bool IsIdleTimerEnabled => this.displayRequest == null;


        public override IObservable<Unit> EnableIdleTimer(bool enabled)
        {
            if (enabled)
            {
                if (this.displayRequest == null)
                {
                    this.displayRequest = new DisplayRequest();
                    this.displayRequest.RequestActive();
                }
            }
            else
            {
                this.displayRequest?.RequestRelease();
                this.displayRequest = null;
            }
            return Observable.Return(Unit.Default);
        }
    }
}
